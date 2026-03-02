export enum Schedule {
    IMMEDIATELY = 0,
    EVERY_MINUTE = 1,
    EVERY_15_MINUTES = 15,
    HOURLY = 60,
    DAILY = 24,
    WEEKLY = 7,
    BIWEEKLY = 72,
    MONTHLY = 30,
    YEARLY = 365
}

export enum Status {
    UNEXECUTED = 0,
    EXECUTING = 1,
    DONE = 2,
    DISMISSED = 3
}

export type Task = {
    name: string;
    schedule: Schedule;
    registerDate: Date;
    nextRun: Date;
    status: Status;
    order: number;
    startDate: Date;
    endDate: Date;
    action: () => void;
}