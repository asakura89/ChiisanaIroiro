import { join } from "@std/path";
import { init } from "../../ayumi/app/app_initializer.ts";
import { existsSync, WalkEntry, walkSync } from "@std/fs";
import BadConfigurationException from "../../kayo/core/bad_config_exc.ts";
import InvalidOperationException from "../../kayo/core/invalid_operation_exc.ts";
import { isNullOrEmpty as arrayIsNullOrEmpty } from "../../kayo/core/array_helper.ts";
import SchedulerException from "./scheduler_exc.ts";
import { getType } from "../../kayo/core/core.ts";
import type { Task } from "./task.ts";
import { Schedule, Status } from "./task.ts";

function initApp() {
    init([
        setAppName,
        initTasks
    ]);
}

function setAppName() {
    globalThis.Context.appInfo.name = "Kacchun";
}

function initTasks() {
    try {
        let tasks = getGlobalTasks();
        if (arrayIsNullOrEmpty(tasks)) {
            tasks = readConfig(Configs.SCHEDULE) as Task[];
            setGlobalTasks(tasks);
        }

        attachActions(tasks!);
        setImportantTaskProperties(tasks!);
    }
    catch (error) {
        console.error("Failed to initialize tasks from config:", error);
    }
}

function attachActions(tasks: Task[]) {
    const tasksDir = join(globalThis.Context.appInfo.directory, "app_plugins");
    const isExists = existsSync(tasksDir);
    if (!isExists)
        throw new InvalidOperationException("Tasks folder supposed to be exist.");

    const files: WalkEntry[] = Array.from(
        walkSync(tasksDir, {
            maxDepth: 1,
            includeFiles: true,
            includeDirs: false,
            includeSymlinks: false,
            exts: [".ts"]
        })
    );

    // NOTE: can be optimized more
    const importedTasks: Record<string, Task> = {};
    for (const file of files) {
        //const modulePath = normalize(file.path);
        //const moduleURI = new URL(modulePath).href;
        const moduleURI = "file:///" + file.path.replaceAll("\\", "/");
        try {
            import(moduleURI)
                .then(module => {
                    if (!module) {
                        console.log("Module cannot be loaded.");
                        return;
                    }

                    const task: Task = module.default;
                    console.log("Module was loaded.");

                    const isValidImport = task &&
                        getType(task.name) === "String" &&
                        getType(task.action) === "Function";

                    if (isValidImport)
                        importedTasks[task.name] = task;
                    else
                        console.warn(`Invalid task in file: ${file.name}`);

                    tasks.map(task => {
                        if (importedTasks[task.name])
                            task.action = importedTasks[task.name].action;
                        else
                            console.warn(`No matching action found for task: ${task.name}`);

                        return task;
                    });

                    setGlobalTasks(tasks);
                })
                .catch(error => console.error(`Error loading module ${file.name}, URI: ${moduleURI}`, error));
        }
        catch (error) {
            console.error(`Error importing file: ${file.name}`, error);
        }
    }
}

function setImportantTaskProperties(tasks: Task[]) {
    for (const task of tasks) {
        if (!task.registerDate) {
            task.registerDate = new Date();
            console.log(`Set now as registerDate for task ${task.name}`);
        }

        if (getType(task.registerDate) === "String") {
            task.registerDate = new Date(task.registerDate);
            console.log(`Convert back registerDate from JSON for task ${task.name}`);
        }

        if (!task.nextRun) {
            task.nextRun = new Date(
                task.registerDate.getTime() + task.schedule * 60 * 1000
            );

            console.log(`Calculated nextRun for task ${task.name}: ${task.nextRun.toString()}`);
        }

        if (!task.status) {
            task.status = Status.UNEXECUTED;
            console.log(`Set empty status for task ${task.name} as UNEXECUTED`);
        }

        updateSingleTaskToGlobal(task);
    }
}

function getGlobalTasks(): Task[] | null {
    const appData = globalThis.Context.data.get("AppData") as {Tasks: Task[] | undefined} | undefined;
    if (!appData)
        return null;

    const tasks = appData["Tasks"];
    if (!tasks)
        return null;

    return tasks;
}

function setGlobalTasks(tasks: Task[]): void {
    let appData = globalThis.Context.data.get("AppData") as {Tasks: Task[] | undefined} | undefined;
    if (!appData) {
        appData = {Tasks: []};
    }

    appData.Tasks = tasks; //(appData.Tasks ?? []).concat(tasks);
    globalThis.Context.data.set("AppData", appData);
}

function updateSingleTaskToGlobal(task: Task): void {
    let tasks = getGlobalTasks()!;
    if (arrayIsNullOrEmpty(tasks)) {
        tasks = [task];
    }
    else {
        const index = tasks.findIndex(t => t.name === task.name);
        if (index !== -1)
            tasks[index] = task;
        else
            tasks.push(task);
    }

    setGlobalTasks(tasks);
    console.log(`Persisted task state for: ${task.name}`);
}

export const Configs = {
    SCHEDULE: "schedule"
}

export function readConfig(configName: string): Record<string, unknown>[] {
    const configPath = join(globalThis.Context.appInfo.directory, "app_data", `${configName}.config.json`);
    const isExists = existsSync(configPath);
    if (!isExists)
        throw new BadConfigurationException("Config file is invalid.");

    return readJson(configPath);
}

export function readJson(filePath: string): Record<string, unknown>[] {
    const data = Deno.readTextFileSync(filePath);
    return JSON.parse(data);
}

export function writeJson(filePath: string, data: Record<string, unknown>[]): void {
    const json = JSON.stringify(data, null, 4);
    Deno.writeTextFileSync(filePath, json);
}

function task() {
    console.log("Task executed: Sending an email...");
}

const runnable = () => {
    console.log("Preparing to execute the task...");
    task();
};

const trigger = (delay: number) => {
    console.log(`Trigger set for ${delay}ms`);
    setTimeout(() => {
        console.log("Trigger activated!");
        runnable();
    }, delay);
};

/* function executeTasks() {
    const tasks: Tasks = [];
} */

function _scheduler() {
    const schedule = 2000;
    console.log("Scheduler initialized.");
    trigger(schedule);


}

function checkAndGetTasksWithinSchedules(): Task[] {
    const tasks = getGlobalTasks()!;
    if (arrayIsNullOrEmpty(tasks)) {
        console.log("No tasks to process.");
        return [];
    }

    tasks.sort((a, b) => a.order - b.order);
    const withinSchedules: Task[] = [];
    for (const task of tasks) {
        try {
            console.log(`Checking task: ${task.name}, Status: ${task.status}`);

            if (task.status === Status.DONE || task.status === Status.DISMISSED) {
                continue;
            }

            if (task.schedule === Schedule.IMMEDIATELY) {
                withinSchedules.push(task);
                continue;
            }

            const isWithinSchedule: boolean = isInScheduleTimeFrame(task.nextRun);
            if (isWithinSchedule)
                withinSchedules.push(task);
            else
                console.log(`Task ${task.name} is not within the timeframe.`);
        }
        catch (error) {
            console.error(`Critical error while processing task: ${task.name}`, error);
        }
    }

    if (arrayIsNullOrEmpty(withinSchedules))
        console.log("There is no tasks within schedules");

    return withinSchedules;
}

function executeRunnable() {
    const withinSchedules: Task[] = checkAndGetTasksWithinSchedules();
    // NOTE: tasks executed still squential not parallel
    for (const task of withinSchedules) {
        console.log(`Executing task: ${task.name}`);

        task.startDate = new Date();
        task.status = Status.EXECUTING;

        let retries = 3;
        while (retries > 0) {
            try {
                if (!task.action || task.action === null)
                    throw new SchedulerException("Action method of a task haven't set");

                task.action();
                task.endDate = new Date();
                task.status = Status.DONE;

                console.log(`Task ${task.name} executed successfully.`);
                break;
            }
            catch (error) {
                console.error(`Error executing task: ${task.name}, Retries left: ${retries - 1}`, error);
                retries--;

                if (retries === 0) {
                    task.status = Status.DISMISSED; // Mark as dismissed on failure
                    console.error(`Task ${task.name} failed after multiple attempts.`);
                }
            }
        }

        task.nextRun = new Date(
            task.nextRun.getTime() + task.schedule * 60 * 1000
        );

        updateSingleTaskToGlobal(task);
        console.log(`Updated nextRun for task ${task.name}: ${task.nextRun}`);
    }
}

let intervalId: number = 0;
let stopScheduler = false;

function start() {
    stopScheduler = false;
    intervalId = setInterval(() => {
        if (stopScheduler) {
            clearInterval(intervalId);
            return;
        }
        executeRunnable();
    }, 1000);
}

function _stop() {
    stopScheduler = true;
}

initApp();
start();

console.log({
    Context: globalThis.Context
});

function getMillisecondSpan(scheduledTime: Date): number {
    const now = new Date();

    /* console.log({
        sc: scheduledTime,
        sct: scheduledTime.getTime(),
        n: now,
        nt: now.getTime(),
        sub: scheduledTime.getTime() - now.getTime()
    }); */
    return scheduledTime.getTime() - now.getTime();
}

function isInScheduleTimeFrame(scheduledTime: Date): boolean {
    const oneMinuteSpan = 60 * 1000;
    const timespan = getMillisecondSpan(scheduledTime);

    // NOTE: this is to make sure even though the schedule late 1 min, it still be ran
    // the grace period is 1 min late
    return timespan >= -oneMinuteSpan;
}

/* function isInRunningTimeFrame(scheduledTime: Date): boolean {
    const now = new Date();
    const compareNow = new Date(
        now.getFullYear(),
        now.getMonth(),
        now.getDate(),
        now.getHours(),
        now.getMinutes(),
        0);

    console.log({
        cn: compareNow.getTime(),
        sc: scheduledTime.getTime(),
        c: compareNow.getTime() === scheduledTime.getTime()
    });
    return isInScheduleTimeFrame(scheduledTime) &&
        compareNow.getTime() === scheduledTime.getTime();
} */

/* const a: Date = new Date(2024, 12 -1, 28, 15, 42, 20, 0);
console.log({
    span: getMillisecondSpan(a),
    timeframe: isInScheduleTimeFrame(a),
    //running: isInRunningTimeFrame(a)
}); */