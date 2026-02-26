import {
    addDays,
    addMonths,
    addYears,
    areIntervalsOverlapping
} from "./datetime.ts";
import {
    CalendarEvent,
    CalendarEventOccurrence
} from "./types.ts";

function buildOccurrence(
    event: CalendarEvent,
    startDate: Date,
    durationMs: number,
    index: number
): CalendarEventOccurrence {
    const endDate = new Date(startDate.getTime() + durationMs);
    return {
        occurrenceId: `${event.id}:${index}:${startDate.toISOString()}`,
        eventId: event.id,
        calendarId: event.calendarId,
        title: event.title,
        description: event.description,
        location: event.location,
        startIso: startDate.toISOString(),
        endIso: endDate.toISOString(),
        allDay: event.allDay,
        sourceStartIso: event.startIso,
        sourceEndIso: event.endIso
    };
}

export function expandEventOccurrences(
    event: CalendarEvent,
    rangeStartIso: string,
    rangeEndIso: string
): CalendarEventOccurrence[] {
    if (!event.recurrence) {
        if (areIntervalsOverlapping(event.startIso, event.endIso, rangeStartIso, rangeEndIso)) {
            return [buildOccurrence(
                event,
                new Date(event.startIso),
                new Date(event.endIso).getTime() - new Date(event.startIso).getTime(),
                0
            )];
        }
        return [];
    }

    const occurrences: CalendarEventOccurrence[] = [];
    const rule = event.recurrence;
    const interval = Math.max(1, rule.interval || 1);
    const eventStartDate = new Date(event.startIso);
    const eventEndDate = new Date(event.endIso);
    const durationMs = eventEndDate.getTime() - eventStartDate.getTime();
    const rangeStartDate = new Date(rangeStartIso);
    const rangeEndDate = new Date(rangeEndIso);
    const untilDate = rule.untilIso ? new Date(rule.untilIso) : null;
    const maxOccurrences = rule.count && rule.count > 0 ? rule.count : Number.POSITIVE_INFINITY;
    let emittedCount = 0;
    let cursor = new Date(eventStartDate);
    let sequence = 0;

    while (emittedCount < maxOccurrences) {
        if (untilDate && cursor > untilDate) {
            break;
        }
        if (cursor >= rangeEndDate) {
            break;
        }

        if (rule.frequency === "weekly") {
            const weekdays = rule.byWeekday.length > 0 ? [...rule.byWeekday].sort() : [eventStartDate.getDay()];
            const weekStart = addDays(cursor, -cursor.getDay());
            for (const weekday of weekdays) {
                const candidate = addDays(weekStart, weekday);
                candidate.setHours(
                    eventStartDate.getHours(),
                    eventStartDate.getMinutes(),
                    eventStartDate.getSeconds(),
                    eventStartDate.getMilliseconds()
                );
                if (candidate < eventStartDate) {
                    continue;
                }
                if (untilDate && candidate > untilDate) {
                    continue;
                }
                const candidateEnd = new Date(candidate.getTime() + durationMs);
                if (areIntervalsOverlapping(
                    candidate.toISOString(),
                    candidateEnd.toISOString(),
                    rangeStartDate.toISOString(),
                    rangeEndDate.toISOString()
                )) {
                    occurrences.push(buildOccurrence(event, candidate, durationMs, sequence));
                }
                emittedCount += 1;
                sequence += 1;
                if (emittedCount >= maxOccurrences) {
                    break;
                }
            }
            cursor = addDays(cursor, interval * 7);
            continue;
        }

        const cursorEnd = new Date(cursor.getTime() + durationMs);
        if (areIntervalsOverlapping(
            cursor.toISOString(),
            cursorEnd.toISOString(),
            rangeStartDate.toISOString(),
            rangeEndDate.toISOString()
        )) {
            occurrences.push(buildOccurrence(event, cursor, durationMs, sequence));
        }
        emittedCount += 1;
        sequence += 1;

        if (rule.frequency === "daily") {
            cursor = addDays(cursor, interval);
            continue;
        }

        if (rule.frequency === "monthly") {
            cursor = addMonths(cursor, interval);
            continue;
        }

        cursor = addYears(cursor, interval);
    }

    return occurrences.sort((a, b) => {
        const diff = new Date(a.startIso).getTime() - new Date(b.startIso).getTime();
        if (diff !== 0) {
            return diff;
        }
        return a.title.localeCompare(b.title);
    });
}
