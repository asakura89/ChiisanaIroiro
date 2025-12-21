import type { Signal } from "@preact/signals";
import { Duration } from "../ayumi/itsu/duration.ts";

export function isValidTime(time: string): boolean {
    const testByRegex: boolean = /^\d{1,2}:\d{2}$/.test(time);
    const testBySplit: boolean = time
        .split(":")
        .every((val: string, idx: number): boolean =>
            idx === 0 ?
                +val >= 0 && +val < 24 :
                +val >= 0 && +val < 60);

    return testByRegex && testBySplit;
}

interface DyanaWindowProps {
    startAt: Signal<string>;
    endAt: Signal<string>;
    workHour: Signal<string>;
    breakHour: Signal<string>;
    total: Signal<string>;
    responsibility: Signal<string>;
    message: Signal<string>;
}

export const DyanaWindow = (props: DyanaWindowProps) => {
    const handleCalculate = () => {
        try {
            if (!isValidTime(props.startAt.value))
                throw new Error("Arrive hours is not valid.");
            if (!isValidTime(props.endAt.value))
                throw new Error("Depart hours is not valid.");

            const [arrHour, arrMinute] = props.startAt.value.split(":").map(Number);
            const [depHour, depMinute] = props.endAt.value.split(":").map(Number);

            const workDuration: Duration = Duration.fromString(props.workHour.value);
            const breakDuration: Duration = Duration.fromString(props.breakHour.value);
            const responsibilityHours: Duration = workDuration.add(breakDuration);

            const now = new Date();
            const start = new Date(now.getFullYear(), now.getMonth(), now.getDate(), arrHour, arrMinute);
            const end = new Date(now.getFullYear(), now.getMonth(), now.getDate(), depHour, depMinute);

            const totalTime: number = (end.getTime() - start.getTime()) / (1000 * 60); // in minutes
            const totalDuration: Duration = new Duration(0, Math.floor(totalTime / 60), totalTime % 60, 0, 0);
            const endOfResponsibility: Date = new Date(start.getTime() + responsibilityHours.totalMilliseconds);

            const message: string[] = [];

            if (totalDuration.totalMilliseconds > responsibilityHours.totalMilliseconds &&
                totalDuration.totalHours > responsibilityHours.totalHours + 1)
                message.push(`Overtimeeee by ${totalDuration.subtract(responsibilityHours).toString()} Waaaaaaattttt...`);

            else if (totalDuration.totalMilliseconds < responsibilityHours.totalMilliseconds) {
                message.push(`Oy! Work more! should be at ${endOfResponsibility.toTimeString().slice(0, 5)}.`);
                message.push(`Less by ${responsibilityHours.subtract(totalDuration).toString()}`);
            }

            else if (Math.abs(Math.floor(totalDuration.totalHours)) === Math.abs(Math.floor(responsibilityHours.totalHours)))
                message.push("It's time to go home :)");

            else
                message.push(`Just go home already!`);

            props.message.value = message.join("\n");
            props.total.value = totalDuration.toString();
            props.responsibility.value = responsibilityHours.toString();
        }
        catch (err: any) {
            props.message.value = err.message;
        }
    };

    const handleReset = () => {
        props.startAt.value = "09:00";
        props.endAt.value = "18:00";
        props.workHour.value = "8h";
        props.breakHour.value = "1h";
        props.total.value = "0";
        props.responsibility.value = "0";
        props.message.value = "";
    };

    return (
        <div class="card bg-base-100 shadow-sm border border-base-300">
            <div class="card-body">
                <div class="flex flex-wrap items-center justify-between gap-3">
                    <div>
                        <div class="text-xs uppercase tracking-widest text-base-content/60">Calculator</div>
                        <h2 class="text-2xl font-semibold">Work hours calculator</h2>
                    </div>
                    <div class="flex gap-2">
                        <button class="btn btn-outline btn-sm" onClick={handleReset}>Reset</button>
                        <button class="btn btn-primary btn-sm" onClick={handleCalculate}>Calculate</button>
                    </div>
                </div>

                <div class="mt-6 grid gap-4 md:grid-cols-2">
                    <label class="form-control">
                        <div class="label">
                            <span class="label-text">Start at</span>
                        </div>
                        <input
                            class="input input-bordered"
                            type="text"
                            value={props.startAt}
                            onInput={(e) => {
                                props.startAt.value = (e.target as HTMLInputElement).value;
                                handleCalculate();
                            }}
                            placeholder="HH:mm (24 h, e.g. 09:00)"
                        />
                    </label>

                    <label class="form-control">
                        <div class="label">
                            <span class="label-text">End at</span>
                        </div>
                        <input
                            class="input input-bordered"
                            type="text"
                            value={props.endAt}
                            onInput={(e) => {
                                props.endAt.value = (e.target as HTMLInputElement).value;
                                handleCalculate();
                            }}
                            placeholder="HH:mm (24 h, e.g. 18:00)"
                        />
                    </label>

                    <label class="form-control">
                        <div class="label">
                            <span class="label-text">Work hours</span>
                        </div>
                        <input
                            class="input input-bordered"
                            type="text"
                            value={props.workHour}
                            onInput={(e) => {
                                props.workHour.value = (e.target as HTMLInputElement).value;
                                handleCalculate();
                            }}
                            placeholder="Hours, e.g. 8h"
                        />
                    </label>

                    <label class="form-control">
                        <div class="label">
                            <span class="label-text">Break hours</span>
                        </div>
                        <input
                            class="input input-bordered"
                            type="text"
                            value={props.breakHour}
                            onInput={(e) => {
                                props.breakHour.value = (e.target as HTMLInputElement).value;
                                handleCalculate();
                            }}
                            placeholder="Hours, e.g. 1h"
                        />
                    </label>
                </div>

                <div class="mt-6 grid gap-4 md:grid-cols-2">
                    <div class="stat bg-base-200 rounded-box">
                        <div class="stat-title">Worked for</div>
                        <div class="stat-value text-primary">{props.total}</div>
                    </div>
                    <div class="stat bg-base-200 rounded-box">
                        <div class="stat-title">Responsibility hours</div>
                        <div class="stat-value text-accent">{props.responsibility}</div>
                    </div>
                </div>

                {props.message.value ? (
                    <div class="mt-6 alert alert-info">
                        <span class="whitespace-pre-line">{props.message}</span>
                    </div>
                ) : null}
            </div>
        </div>
    );
};
