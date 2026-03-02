import { Schedule, Status } from "../task.ts";

export default {
    name: "task:simple",
    schedule: Schedule.IMMEDIATELY,
    registerDate: new Date(),
    nextRun: new Date(),
    status: Status.UNEXECUTED,
    order: 1,
    startDate: new Date(),
    endDate: new Date(),
    action: () => {
        console.log("Executing Task.");
    }
};
