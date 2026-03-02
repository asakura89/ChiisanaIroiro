export enum ProcessStatus {
    UNEXECUTED = 0,
    EXECUTING = 1,
    DONE = 2,
    DISMISSED = 3
}

export type Process = {
    id: number;
    name: string;
    order: number;
    waitFrom?: number;
    waitTo?: number;
    startTime?: Date;
    endTime?: Date;
    status: ProcessStatus;
    param: string;
    result: string;

    execute(): void;
}

export class ProcessManager {
    generateProcessId(): number;
    getAllUnexecutedProcesses(ids?: number[]): Process[];
    getAllExecutedProcesses(ids?: number[]): Process[];
}


/* export class Task {
    isExecuting: boolean = false;
    readonly id: string;
    readonly processId: number;
    processes: Process[];


    execute
} */