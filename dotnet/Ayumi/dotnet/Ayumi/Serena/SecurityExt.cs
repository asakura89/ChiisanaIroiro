using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using ConfigurationManager = AppSea.ConfigurationManager;

namespace Serena;

public static class SecurityExt {
    const int Pbkdf2IterationCount = 210000;
    const int EncryptionKeySizeInBytes = 32;
    const int NonceSizeInBytes = 12;
    const int TagSizeInBytes = 16;
    const byte PayloadVersion = 1;

    public static string Encrypt(this string plainText) => Encrypt(plainText, GetKey(), GetSalt());

    public static string Encrypt(this string plainText, string securityKey, string securitySalt) {
        ArgumentNullException.ThrowIfNull(plainText);

        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] encryptionKey = CreateEncryptionKey(securityKey, securitySalt);
        byte[] nonce = RandomNumberGenerator.GetBytes(NonceSizeInBytes);
        byte[] cipherBytes = new byte[plainBytes.Length];
        byte[] authenticationTag = new byte[TagSizeInBytes];

        using (var algorithm = new AesGcm(encryptionKey, TagSizeInBytes))
            algorithm.Encrypt(nonce, plainBytes, cipherBytes, authenticationTag);

        byte[] payload = new byte[1 + NonceSizeInBytes + TagSizeInBytes + cipherBytes.Length];
        payload[0] = PayloadVersion;
        Buffer.BlockCopy(nonce, 0, payload, 1, NonceSizeInBytes);
        Buffer.BlockCopy(authenticationTag, 0, payload, 1 + NonceSizeInBytes, TagSizeInBytes);
        Buffer.BlockCopy(cipherBytes, 0, payload, 1 + NonceSizeInBytes + TagSizeInBytes, cipherBytes.Length);

        CryptographicOperations.ZeroMemory(encryptionKey);

        return EncodeBase64UrlFromBytes(payload);
    }

    public static string Decrypt(this string chiperText) => Decrypt(chiperText, GetKey(), GetSalt());

    public static string Decrypt(this string chiperText, string securityKey, string securitySalt) {
        ArgumentNullException.ThrowIfNull(chiperText);

        byte[] payload = DecodeBase64UrlToBytes(chiperText);
        if (payload.Length < 1 + NonceSizeInBytes + TagSizeInBytes)
            throw new CryptographicException("Invalid encrypted payload.");
        if (payload[0] != PayloadVersion)
            throw new CryptographicException("Unsupported encrypted payload version.");

        byte[] nonce = new byte[NonceSizeInBytes];
        byte[] authenticationTag = new byte[TagSizeInBytes];
        int cipherOffset = 1 + NonceSizeInBytes + TagSizeInBytes;
        byte[] cipherBytes = new byte[payload.Length - cipherOffset];
        byte[] plainBytes = new byte[cipherBytes.Length];
        byte[] encryptionKey = CreateEncryptionKey(securityKey, securitySalt);

        Buffer.BlockCopy(payload, 1, nonce, 0, NonceSizeInBytes);
        Buffer.BlockCopy(payload, 1 + NonceSizeInBytes, authenticationTag, 0, TagSizeInBytes);
        Buffer.BlockCopy(payload, cipherOffset, cipherBytes, 0, cipherBytes.Length);

        using (var algorithm = new AesGcm(encryptionKey, TagSizeInBytes))
            algorithm.Decrypt(nonce, cipherBytes, authenticationTag, plainBytes);

        CryptographicOperations.ZeroMemory(encryptionKey);

        return Encoding.UTF8.GetString(plainBytes);
    }

    static byte[] CreateEncryptionKey(string securityKey, string securitySalt) {
        string password = securityKey ?? string.Empty;
        string saltSource = string.IsNullOrEmpty(securitySalt) ? password : securitySalt;
        byte[] saltBytes = Encoding.UTF8.GetBytes(saltSource);

        if (string.IsNullOrEmpty(password))
            throw new CryptographicException("Security key is required.");
        if (saltBytes.Length == 0)
            throw new CryptographicException("Security salt is required.");

        return Rfc2898DeriveBytes.Pbkdf2(
            password,
            saltBytes,
            Pbkdf2IterationCount,
            HashAlgorithmName.SHA512,
            EncryptionKeySizeInBytes
        );
    }

    public const string SecurityKeyAppSettingsKey = "Security.Key";

    static string GetKey() {
        string key = ConfigurationManager.GetAppSetting(SecurityKeyAppSettingsKey)?.Value;
        return key ?? string.Empty;
    }

    public const string SecuritySaltAppSettingsKey = "Security.Salt";

    static string GetSalt() {
        string salt = ConfigurationManager.GetAppSetting(SecuritySaltAppSettingsKey)?.Value;
        return salt ?? string.Empty;
    }

    const string Base64Plus = "+";
    const string Base64Slash = "/";
    const string Base64Underscore = "_";
    const string Base64Minus = "-";
    const string Base64Equal = "=";
    const string Base64DoubleEqual = "==";
    const char Base64EqualChar = '=';

    public static string EncodeBase64Url(string plain) =>
        EncodeBase64UrlFromBytes(
            Encoding.UTF8.GetBytes(plain)
        );

    static string EncodeBase64UrlFromBytes(byte[] bytes) =>
        Convert
            .ToBase64String(bytes)
            .TrimEnd(Base64EqualChar)
            .Replace(Base64Plus, Base64Minus)
            .Replace(Base64Slash, Base64Underscore);

    public static string DecodeBase64Url(string base64Url) =>
        Encoding.UTF8.GetString(
            DecodeBase64UrlToBytes(base64Url)
        );

    static byte[] DecodeBase64UrlToBytes(string base64Url) {
        string halfProcessed = base64Url
            .Replace(Base64Minus, Base64Plus)
            .Replace(Base64Underscore, Base64Slash);

        string base64 = halfProcessed;
        if (halfProcessed.Length % 4 == 2)
            base64 = halfProcessed + Base64DoubleEqual;
        else if (halfProcessed.Length % 4 == 3)
            base64 = halfProcessed + Base64Equal;

        return Convert.FromBase64String(base64);
    }

    public static SecureString AsSecureString(this string plain) {
        if (string.IsNullOrWhiteSpace(plain))
            throw new ArgumentNullException(nameof(plain));

        return new NetworkCredential(string.Empty, plain).SecurePassword;
    }

    public static string AsPlainString(this SecureString secure) {
        if (secure == null)
            throw new ArgumentNullException(nameof(secure));

        return new NetworkCredential(string.Empty, secure).Password;
    }

    public static string GenerateKey() => EncodeBase64UrlFromBytes(RandomNumberGenerator.GetBytes(EncryptionKeySizeInBytes));

    public static string GenerateSalt() => EncodeBase64UrlFromBytes(RandomNumberGenerator.GetBytes(EncryptionKeySizeInBytes));
}
