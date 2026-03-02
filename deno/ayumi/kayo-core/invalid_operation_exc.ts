export default class InvalidOperationException extends Error {
    constructor(message?: string) {
        super(message ? message : "InvalidOperationException");
        this.name = "InvalidOperationException";

        if (Error.captureStackTrace) {
            Error.captureStackTrace(this, InvalidOperationException);
        }
    }
}