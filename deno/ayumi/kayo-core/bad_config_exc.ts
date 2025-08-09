export default class BadConfigurationException extends Error {
    constructor(message?: string) {
        super(message ? message : "BadConfigurationException");
        this.name = "BadConfigurationException";

        if (Error.captureStackTrace)
            Error.captureStackTrace(this, BadConfigurationException);
    }
}