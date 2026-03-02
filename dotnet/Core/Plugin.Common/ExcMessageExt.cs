namespace Plugin.Common;

public static class ExcMessageExt {
    public static InvalidOperationException AsNaiseuErrorMessage(this string message) {
        return new InvalidOperationException($"{message} ヽ(ﾟДﾟ; )ﾉ");
    }
}