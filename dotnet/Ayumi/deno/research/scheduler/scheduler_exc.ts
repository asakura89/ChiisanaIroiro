export default class Scheduler extends Error {
    constructor(message?: string) {
        super(message ? message : "Scheduler");
        this.name = "Scheduler";

        if (Error.captureStackTrace)
            Error.captureStackTrace(this, Scheduler);
    }
}