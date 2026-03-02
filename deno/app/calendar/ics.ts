import { CalendarEvent, CalendarProfile, RecurrenceRule } from "./types.ts";

function padNumber(value: number): string {
    return String(value).padStart(2, "0");
}

function toUtcDateTime(iso: string): string {
    const date = new Date(iso);
    return `${date.getUTCFullYear()}${padNumber(date.getUTCMonth() + 1)}${padNumber(date.getUTCDate())}T${padNumber(date.getUTCHours())}${padNumber(date.getUTCMinutes())}${padNumber(date.getUTCSeconds())}Z`;
}

function toDateValue(iso: string): string {
    const date = new Date(iso);
    return `${date.getUTCFullYear()}${padNumber(date.getUTCMonth() + 1)}${padNumber(date.getUTCDate())}`;
}

function escapeText(value: string): string {
    return value
        .replaceAll("\\", "\\\\")
        .replaceAll(";", "\\;")
        .replaceAll(",", "\\,")
        .replaceAll("\n", "\\n");
}

function recurrenceToRRule(recurrence: RecurrenceRule | null): string | null {
    if (!recurrence) {
        return null;
    }
    const byDayMap = ["SU", "MO", "TU", "WE", "TH", "FR", "SA"];
    const freq = recurrence.frequency.toUpperCase();
    const parts = [`FREQ=${freq}`, `INTERVAL=${Math.max(1, recurrence.interval || 1)}`];
    if (recurrence.count && recurrence.count > 0) {
        parts.push(`COUNT=${Math.trunc(recurrence.count)}`);
    }
    if (recurrence.untilIso) {
        parts.push(`UNTIL=${toUtcDateTime(recurrence.untilIso)}`);
    }
    if (recurrence.frequency === "weekly" && recurrence.byWeekday.length > 0) {
        const byDay = recurrence.byWeekday
            .filter((value) => value >= 0 && value <= 6)
            .map((value) => byDayMap[value])
            .join(",");
        if (byDay) {
            parts.push(`BYDAY=${byDay}`);
        }
    }
    return parts.join(";");
}

function eventToIcsLines(event: CalendarEvent, calendar: CalendarProfile | undefined, stampUtc: string): string[] {
    const lines: string[] = [
        "BEGIN:VEVENT",
        `UID:${event.id}@chiisana-iroiro`,
        `DTSTAMP:${stampUtc}`
    ];
    if (event.allDay) {
        lines.push(`DTSTART;VALUE=DATE:${toDateValue(event.startIso)}`);
        lines.push(`DTEND;VALUE=DATE:${toDateValue(event.endIso)}`);
    } else {
        lines.push(`DTSTART:${toUtcDateTime(event.startIso)}`);
        lines.push(`DTEND:${toUtcDateTime(event.endIso)}`);
    }
    lines.push(`SUMMARY:${escapeText(event.title)}`);
    if (event.description) {
        lines.push(`DESCRIPTION:${escapeText(event.description)}`);
    }
    if (event.location) {
        lines.push(`LOCATION:${escapeText(event.location)}`);
    }
    if (calendar?.name) {
        lines.push(`CATEGORIES:${escapeText(calendar.name)}`);
    }
    const rrule = recurrenceToRRule(event.recurrence);
    if (rrule) {
        lines.push(`RRULE:${rrule}`);
    }
    lines.push("END:VEVENT");
    return lines;
}

export function buildCalendarIcs(
    events: CalendarEvent[],
    calendars: CalendarProfile[],
    calendarName: string
): string {
    const stampUtc = toUtcDateTime(new Date().toISOString());
    const profileMap = new Map(calendars.map((item) => [item.id, item]));
    const lines: string[] = [
        "BEGIN:VCALENDAR",
        "VERSION:2.0",
        "PRODID:-//ChiisanaIroiro//Calendar//EN",
        "CALSCALE:GREGORIAN",
        "METHOD:PUBLISH",
        `X-WR-CALNAME:${escapeText(calendarName)}`
    ];
    for (const event of events) {
        lines.push(...eventToIcsLines(event, profileMap.get(event.calendarId), stampUtc));
    }
    lines.push("END:VCALENDAR");
    return `${lines.join("\r\n")}\r\n`;
}
