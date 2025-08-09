export class Duration {
    readonly millis: number;

    private static readonly MillisPerSecond = 1000;
    private static readonly MillisPerMinute = 60 * Duration.MillisPerSecond;
    private static readonly MillisPerHour = 60 * Duration.MillisPerMinute;
    private static readonly MillisPerDay = 24 * Duration.MillisPerHour;

    constructor();
    constructor(days: number);
    constructor(days: number, hours: number);
    constructor(days: number, hours: number, minutes: number);
    constructor(days: number, hours: number, minutes: number, seconds: number);
    constructor(days: number, hours: number, minutes: number, seconds: number, milliseconds: number);
    constructor(days: number = 0, hours: number = 0, minutes: number = 0, seconds: number = 0, milliseconds: number = 0) {
        this.millis = days * Duration.MillisPerDay +
            hours * Duration.MillisPerHour +
            minutes * Duration.MillisPerMinute +
            seconds * Duration.MillisPerSecond +
            milliseconds;
    }

    static fromMilliseconds(ms: number): Duration {
        return new Duration(0, 0, 0, 0, ms);
    }

    static fromMinutes(minutes: number): Duration {
        return new Duration(0, 0, minutes);
    }

    static fromHours(hours: number): Duration {
        return new Duration(0, hours);
    }

    private static tryParseByRegex(duration: string): { success: boolean, result: Duration } {
        let ms = 0;

        // Accepts "1d 2h 3m 4s 5ms"
        const regex = /(?:(\d+)d)?\s*(?:(\d+)h)?\s*(?:(\d+)(?!ms)m)?\s*(?:(\d+)s)?\s*(?:(\d+)ms)?/i;
        const match = duration.match(regex);
        if (match && (match[1] || match[2] || match[3] || match[4] || match[5])) {
            ms += parseInt(match[1] || "0") * Duration.MillisPerDay;
            ms += parseInt(match[2] || "0") * Duration.MillisPerHour;
            ms += parseInt(match[3] || "0") * Duration.MillisPerMinute;
            ms += parseInt(match[4] || "0") * Duration.MillisPerSecond;
            ms += parseInt(match[5] || "0");

            return {
                success: true,
                result: Duration.fromMilliseconds(ms)
            };
        }

        return {
            success: false,
            result: new Duration(0)
        };
    }

    private static tryParseByDelimitedString(duration: string): { success: boolean, result: Duration } {
        let ms = 0;

        // Accepts "dd:hh:mm:ss:ms"
        const timeParts = duration.split(":").map(part => parseInt(part, 10));
        if (timeParts.length === 5) {
            ms += timeParts[0] * Duration.MillisPerDay;
            ms += timeParts[1] * Duration.MillisPerHour;
            ms += timeParts[2] * Duration.MillisPerMinute;
            ms += timeParts[3] * Duration.MillisPerSecond;
            ms += timeParts[4];

            return {
                success: true,
                result: Duration.fromMilliseconds(ms)
            };
        }

        if (timeParts.length === 4) {
            ms += timeParts[0] * Duration.MillisPerHour;
            ms += timeParts[1] * Duration.MillisPerMinute;
            ms += timeParts[2] * Duration.MillisPerSecond;
            ms += timeParts[3];

            return {
                success: true,
                result: Duration.fromMilliseconds(ms)
            };
        }

        if (timeParts.length === 3) {
            ms += timeParts[0] * Duration.MillisPerMinute;
            ms += timeParts[1] * Duration.MillisPerSecond;
            ms += timeParts[2];

            return {
                success: true,
                result: Duration.fromMilliseconds(ms)
            };
        }

        if (timeParts.length === 2) {
            ms += timeParts[0] * Duration.MillisPerSecond;
            ms += timeParts[1];

            return {
                success: true,
                result: Duration.fromMilliseconds(ms)
            };
        }

        if (timeParts.length === 1 && !isNaN(timeParts[0])) {
            ms += timeParts[0];

            return {
                success: true,
                result: Duration.fromMilliseconds(ms)
            };
        }

        return {
            success: false,
            result: new Duration(0)
        };
    }

    static fromString(duration: string): Duration {
        let { success, result } = this.tryParseByRegex(duration);
        if (success) {
            return result;
        }

        ({ success, result } = this.tryParseByDelimitedString(duration));
        if (success) {
            return result;
        }

        throw new Error(`Unrecognized duration format: ${duration}`);
    }

    get days() {
        return Math.floor(this.millis / Duration.MillisPerDay);
    }

    get hours() {
        return Math.floor((this.millis / Duration.MillisPerHour) % 24);
    }

    get minutes() {
        return Math.floor((this.millis / Duration.MillisPerMinute) % 60);
    }

    get seconds() {
        return Math.floor((this.millis / Duration.MillisPerSecond) % 60);
    }

    get milliseconds() {
        return Math.floor(this.millis % 1000);
    }

    get totalDays() {
        return this.millis / Duration.MillisPerDay;
    }

    get totalHours() {
        return this.millis / Duration.MillisPerHour;
    }

    get totalMinutes() {
        return this.millis / Duration.MillisPerMinute;
    }

    get totalSeconds() {
        return this.millis / Duration.MillisPerSecond;
    }

    get totalMilliseconds() {
        return this.millis;
    }

    add(other: Duration): Duration {
        return Duration.fromMilliseconds(this.totalMilliseconds + other.totalMilliseconds);
    }

    subtract(other: Duration): Duration {
        return Duration.fromMilliseconds(this.totalMilliseconds - other.totalMilliseconds);
    }

    toString(): string {
        let result = [];

        if (this.days)
            result.push(`${this.days}d`);

        if (this.hours)
            result.push(`${this.hours}h`);

        if (this.minutes)
            result.push(`${this.minutes}m`);

        if (this.seconds)
            result.push(`${this.seconds}s`);

        if (this.milliseconds)
            result.push(`${this.milliseconds}ms`);

        if (result.length === 0)
            result.push("0ms");

        return result.join(" ");
    }

    toDelimitedString(): string {
        let result: number[] = [];

        if (this.days)
            result.push(this.days);

        if (this.hours)
            result.push(this.hours);

        if (this.minutes)
            result.push(this.minutes);

        if (this.seconds)
            result.push(this.seconds);

        if (this.milliseconds)
            result.push(this.milliseconds);

        if (result.length === 0)
            result.push(0);

        return result.join(":");
    }
}