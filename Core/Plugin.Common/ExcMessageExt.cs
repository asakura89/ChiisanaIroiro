namespace Plugin.Common {
    public static class ExcMessageExt {
        public static InvalidOperationException AsNaiseuErrorMessage(this String message) => new($"{message} ヽ(ﾟДﾟ; )ﾉ");
    }
}
