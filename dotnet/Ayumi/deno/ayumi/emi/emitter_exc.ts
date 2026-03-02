export default class EmitterException extends Error {
    constructor(message?: string) {
        super(message ? message : "EmitterException");
        this.name = "EmitterException";

        if (Error.captureStackTrace) {
            Error.captureStackTrace(this, EmitterException);
        }
    }
}