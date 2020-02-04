using System;

namespace DefaultPlugin {
    public static class ExcMessageExt {
        public static InvalidOperationException AsNaiseuErrorMessage(this String message) =>
            new InvalidOperationException($"{message} ヽ(ﾟДﾟ; )ﾉ");
    }
}
