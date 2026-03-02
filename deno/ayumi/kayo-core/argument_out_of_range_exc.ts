export default class ArgumentOutOfRangeException extends Error {
    constructor(message?: string) {
        super(message ? message : "ArgumentOutOfRangeException");
        this.name = "ArgumentOutOfRangeException";

        if (Error.captureStackTrace) {
            Error.captureStackTrace(this, ArgumentOutOfRangeException);
        }
    }
}