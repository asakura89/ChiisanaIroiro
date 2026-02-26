export type CalendarView = "day" | "week" | "month" | "agenda";

export type RecurrenceFrequency = "daily" | "weekly" | "monthly" | "yearly";

export interface RecurrenceRule {
    frequency: RecurrenceFrequency;
    interval: number;
    untilIso: string | null;
    count: number | null;
    byWeekday: number[];
}

export interface CalendarProfile {
    id: string;
    name: string;
    color: string;
    tag: string;
    createdAtIso: string;
    updatedAtIso: string;
}

export interface CalendarEvent {
    id: string;
    calendarId: string;
    title: string;
    description: string;
    location: string;
    startIso: string;
    endIso: string;
    allDay: boolean;
    recurrence: RecurrenceRule | null;
    createdAtIso: string;
    updatedAtIso: string;
}

export interface CalendarEventOccurrence {
    occurrenceId: string;
    eventId: string;
    calendarId: string;
    title: string;
    description: string;
    location: string;
    startIso: string;
    endIso: string;
    allDay: boolean;
    sourceStartIso: string;
    sourceEndIso: string;
}

export interface CalendarEventsResponse {
    calendars: CalendarProfile[];
    events: CalendarEvent[];
    occurrences: CalendarEventOccurrence[];
}
