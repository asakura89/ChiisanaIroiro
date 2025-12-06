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
        <div style={{ backgroundColor: "#1a1a1a", color: "#fff", padding: "20px", borderRadius: "8px" }}>
            <h1>Dyana Work Hours Calculator</h1>
            <div style={{ marginBottom: "10px" }}>
                <label>Start At:</label>
                <input
                    type="text"
                    value={props.startAt}
                    onInput={(e) => {
                        props.startAt.value = (e.target as HTMLInputElement).value;
                        handleCalculate();
                    }}
                    placeholder="HH:mm"
                    style={{ marginLeft: "10px" }}
                />
            </div>
            <div style={{ marginBottom: "10px" }}>
                <label>End At:</label>
                <input
                    type="text"
                    value={props.endAt}
                    onInput={(e) => {
                        props.endAt.value = (e.target as HTMLInputElement).value;
                        handleCalculate();
                    }}
                    placeholder="HH:mm"
                    style={{ marginLeft: "10px" }}
                />
            </div>
            <div style={{ marginBottom: "10px" }}>
                <label>Work Hours:</label>
                <input
                    type="text"
                    value={props.workHour}
                    onInput={(e) => {
                        props.workHour.value = (e.target as HTMLInputElement).value;
                        handleCalculate();
                    }}
                    placeholder="Hours"
                    style={{ marginLeft: "10px" }}
                />
            </div>
            <div style={{ marginBottom: "10px" }}>
                <label>Break Hours:</label>
                <input
                    type="text"
                    value={props.breakHour}
                    onInput={(e) => {
                        props.breakHour.value = (e.target as HTMLInputElement).value;
                        handleCalculate();
                    }}
                    placeholder="Hours"
                    style={{ marginLeft: "10px" }}
                />
            </div>
            <div style={{ marginTop: "20px" }}>
                <button onClick={handleCalculate} style={{ marginRight: "10px" }}>Calculate</button>
                <button onClick={handleReset}>Reset</button>
            </div>
            {props.total.value && (
                <div style={{ marginTop: "20px" }}>
                    <h3>Worked for: {props.total}</h3>
                    <h3>Responsibility hours: {props.responsibility}</h3>
                    <h3>{props.message}</h3>
                </div>
            )}
        </div>
    );
};
