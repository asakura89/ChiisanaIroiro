export default class ArgumentNullException extends Error {
    constructor(message?: string) {
        super(message ? message : "ArgumentNullException");
        this.name = "ArgumentNullException";

        if (Error.captureStackTrace) {
            Error.captureStackTrace(this, ArgumentNullException);
        }
    }
}