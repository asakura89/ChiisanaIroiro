import { Duration } from "./duration.ts";

export class TimeOfDay {
    readonly hours: number;
    readonly minutes: number;

    constructor(hours: number, minutes: number = 0) {
        if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
            throw new Error("Invalid time of day");
        this.hours = hours;
        this.minutes = minutes;
    }

    static fromString(time: string): TimeOfDay {
        const [h, m = "0"] = time.split(":");
        return new TimeOfDay(parseInt(h, 10), parseInt(m, 10));
    }

    static isValid(time: string): boolean {
        const testByRegex = /^\d{1,2}(:\d{2})?$/.test(time);
        if (!testByRegex) return false;
        const [h, m = "0"] = time.split(":");
        const hours = +h, minutes = +m;
        return hours >= 0 && hours < 24 && minutes >= 0 && minutes < 60;
    }

    toMinutes(): number {
        return this.hours * 60 + this.minutes;
    }

    static fromMinutes(totalMinutes: number): TimeOfDay {
        // Wraps around 24h
        const mins = ((totalMinutes % 1440) + 1440) % 1440; // handle negative
        const hours = Math.floor(mins / 60);
        const minutes = mins % 60;
        return new TimeOfDay(hours, minutes);
    }

    addDuration(duration: Duration): TimeOfDay {
        return TimeOfDay.fromMinutes(this.toMinutes() + duration.minutes);
    }

    subtractDuration(duration: Duration): TimeOfDay {
        return TimeOfDay.fromMinutes(this.toMinutes() - duration.minutes);
    }

    diff(other: TimeOfDay): Duration {
        // Returns the difference as a Duration (can be negative)
        return Duration.fromMinutes(this.toMinutes() - other.toMinutes());
    }

    toString(): string {
        return `${this.hours}:${String(this.minutes).padStart(2, "0")}`;
    }
}
