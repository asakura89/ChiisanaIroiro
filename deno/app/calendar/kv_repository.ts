import {
    CalendarEvent,
    CalendarEventOccurrence,
    CalendarProfile
} from "./types.ts";
import { expandEventOccurrences } from "./recurrence.ts";

const CALENDAR_PROFILE_PREFIX = ["calendar", "profile"] as const;
const CALENDAR_EVENT_PREFIX = ["calendar", "event"] as const;

function nowIso(): string {
    return new Date().toISOString();
}

async function openCalendarKv(): Promise<any> {
    return await (Deno as typeof Deno & { openKv: () => Promise<any> }).openKv();
}

export async function listCalendarProfiles(): Promise<CalendarProfile[]> {
    const kv = await openCalendarKv();
    const profiles: CalendarProfile[] = [];
    for await (const entry of kv.list({prefix: CALENDAR_PROFILE_PREFIX})) {
        profiles.push(entry.value as CalendarProfile);
    }
    if (profiles.length === 0) {
        const defaults = await ensureDefaultProfiles(kv);
        return defaults;
    }
    return profiles.sort((a, b) => a.name.localeCompare(b.name));
}

async function ensureDefaultProfiles(kv: any): Promise<CalendarProfile[]> {
    const now = nowIso();
    const defaults: CalendarProfile[] = [{
            id: crypto.randomUUID(),
            name: "Work",
            color: "#1f6feb",
            tag: "work",
            createdAtIso: now,
            updatedAtIso: now
        }, {
            id: crypto.randomUUID(),
            name: "Personal",
            color: "#0f9d58",
            tag: "personal",
            createdAtIso: now,
            updatedAtIso: now
        }, {
            id: crypto.randomUUID(),
            name: "Public Holiday",
            color: "#d93025",
            tag: "holiday",
            createdAtIso: now,
            updatedAtIso: now
        }];
    for (const profile of defaults) {
        await kv.set([...CALENDAR_PROFILE_PREFIX, profile.id], profile);
    }
    return defaults;
}

export async function createCalendarProfile(
    payload: Pick<CalendarProfile, "name" | "color" | "tag">
): Promise<CalendarProfile> {
    const kv = await openCalendarKv();
    const now = nowIso();
    const profile: CalendarProfile = {
        id: crypto.randomUUID(),
        name: payload.name.trim(),
        color: payload.color.trim(),
        tag: payload.tag.trim(),
        createdAtIso: now,
        updatedAtIso: now
    };
    await kv.set([...CALENDAR_PROFILE_PREFIX, profile.id], profile);
    return profile;
}

export async function updateCalendarProfile(
    payload: Pick<CalendarProfile, "id" | "name" | "color" | "tag">
): Promise<CalendarProfile | null> {
    const kv = await openCalendarKv();
    const existing = await kv.get([...CALENDAR_PROFILE_PREFIX, payload.id]);
    if (!existing.value) {
        return null;
    }
    const updated: CalendarProfile = {
        ...(existing.value as CalendarProfile),
        name: payload.name.trim(),
        color: payload.color.trim(),
        tag: payload.tag.trim(),
        updatedAtIso: nowIso()
    };
    await kv.set([...CALENDAR_PROFILE_PREFIX, updated.id], updated);
    return updated;
}

export async function deleteCalendarProfile(profileId: string): Promise<boolean> {
    const kv = await openCalendarKv();
    const profileKey = [...CALENDAR_PROFILE_PREFIX, profileId];
    const existing = await kv.get(profileKey);
    if (!existing.value) {
        return false;
    }
    for await (const entry of kv.list({prefix: CALENDAR_EVENT_PREFIX})) {
        if ((entry.value as CalendarEvent).calendarId === profileId) {
            await kv.delete(entry.key);
        }
    }
    await kv.delete(profileKey);
    return true;
}

export async function listCalendarEvents(): Promise<CalendarEvent[]> {
    const kv = await openCalendarKv();
    const events: CalendarEvent[] = [];
    for await (const entry of kv.list({prefix: CALENDAR_EVENT_PREFIX})) {
        events.push(entry.value as CalendarEvent);
    }
    return events.sort((a, b) => new Date(a.startIso).getTime() - new Date(b.startIso).getTime());
}

export async function createCalendarEvent(
    payload: Omit<CalendarEvent, "id" | "createdAtIso" | "updatedAtIso">
): Promise<CalendarEvent> {
    const kv = await openCalendarKv();
    const now = nowIso();
    const event: CalendarEvent = {
        ...payload,
        id: crypto.randomUUID(),
        createdAtIso: now,
        updatedAtIso: now
    };
    await kv.set([...CALENDAR_EVENT_PREFIX, event.id], event);
    return event;
}

export async function updateCalendarEvent(
    payload: Pick<CalendarEvent, "id"> & Partial<Omit<CalendarEvent, "id" | "createdAtIso" | "updatedAtIso">>
): Promise<CalendarEvent | null> {
    const kv = await openCalendarKv();
    const key = [...CALENDAR_EVENT_PREFIX, payload.id];
    const existing = await kv.get(key);
    if (!existing.value) {
        return null;
    }
    const updated: CalendarEvent = {
        ...(existing.value as CalendarEvent),
        ...payload,
        updatedAtIso: nowIso()
    };
    await kv.set(key, updated);
    return updated;
}

export async function deleteCalendarEvent(eventId: string): Promise<boolean> {
    const kv = await openCalendarKv();
    const key = [...CALENDAR_EVENT_PREFIX, eventId];
    const existing = await kv.get(key);
    if (!existing.value) {
        return false;
    }
    await kv.delete(key);
    return true;
}

export async function listOccurrencesInRange(
    rangeStartIso: string,
    rangeEndIso: string,
    calendarIds: string[] | null
): Promise<CalendarEventOccurrence[]> {
    const events = await listCalendarEvents();
    const filteredEvents = calendarIds && calendarIds.length > 0
        ? events.filter((event) => calendarIds.includes(event.calendarId))
        : events;
    const occurrences = filteredEvents.flatMap((event) =>
        expandEventOccurrences(event, rangeStartIso, rangeEndIso)
    );
    return occurrences.sort((a, b) => {
        const diff = new Date(a.startIso).getTime() - new Date(b.startIso).getTime();
        if (diff !== 0) {
            return diff;
        }
        return a.title.localeCompare(b.title);
    });
}
