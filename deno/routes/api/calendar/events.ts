import { Handlers } from "$fresh/server.ts";
import {
    createCalendarEvent,
    createCalendarProfile,
    deleteCalendarEvent,
    deleteCalendarProfile,
    listCalendarEvents,
    listCalendarProfiles,
    listOccurrencesInRange,
    updateCalendarEvent,
    updateCalendarProfile
} from "../../../app/calendar/kv_repository.ts";
import { CalendarEvent, RecurrenceRule } from "../../../app/calendar/types.ts";

function jsonResponse(payload: unknown, status = 200): Response {
    return new Response(JSON.stringify(payload), {
        status,
        headers: {"content-type": "application/json"}
    });
}

function parseCalendarIds(params: URLSearchParams): string[] | null {
    const ids = params.getAll("calendarId").flatMap((item) => item.split(",")).map((item) =>
        item.trim()
    ).filter((item) => item.length > 0);
    if (ids.length === 0) {
        return null;
    }
    return ids;
}

function normalizeRecurrence(input: unknown): RecurrenceRule | null {
    if (!input || typeof input !== "object") {
        return null;
    }
    const value = input as Record<string, unknown>;
    const frequency = String(value.frequency || "").toLowerCase();
    if (!["daily", "weekly", "monthly", "yearly"].includes(frequency)) {
        return null;
    }
    const interval = Number(value.interval || 1);
    const count = value.count == null || value.count === "" ? null : Number(value.count);
    const untilIso = value.untilIso == null || value.untilIso === "" ? null : String(value.untilIso);
    const byWeekday = Array.isArray(value.byWeekday) ? value.byWeekday.map((item) => Number(item)).filter((item) => item >= 0 && item <= 6) : [];
    return {
        frequency: frequency as RecurrenceRule["frequency"],
        interval: Number.isFinite(interval) && interval > 0 ? Math.trunc(interval) : 1,
        count: count != null && Number.isFinite(count) && count > 0 ? Math.trunc(count) : null,
        untilIso,
        byWeekday
    };
}

function validateEventInput(payload: Record<string, unknown>): { ok: true; value: Omit<CalendarEvent, "id" | "createdAtIso" | "updatedAtIso"> } | { ok: false; error: string } {
    const title = String(payload.title || "").trim();
    const calendarId = String(payload.calendarId || "").trim();
    const startIso = String(payload.startIso || "").trim();
    const endIso = String(payload.endIso || "").trim();
    if (!title) {
        return {ok: false, error: "Event title is required"};
    }
    if (!calendarId) {
        return {ok: false, error: "calendarId is required"};
    }
    if (!startIso || Number.isNaN(new Date(startIso).getTime())) {
        return {ok: false, error: "startIso is invalid"};
    }
    if (!endIso || Number.isNaN(new Date(endIso).getTime())) {
        return {ok: false, error: "endIso is invalid"};
    }
    if (new Date(endIso).getTime() <= new Date(startIso).getTime()) {
        return {ok: false, error: "endIso must be after startIso"};
    }
    const recurrence = normalizeRecurrence(payload.recurrence);
    return {
        ok: true,
        value: {
            calendarId,
            title,
            description: String(payload.description || ""),
            location: String(payload.location || ""),
            startIso,
            endIso,
            allDay: Boolean(payload.allDay),
            recurrence
        }
    };
}

export const handler: Handlers = {
    async GET(req) {
        const url = new URL(req.url);
        const kind = url.searchParams.get("kind") || "events";
        if (kind === "calendars") {
            const calendars = await listCalendarProfiles();
            return jsonResponse({calendars});
        }
        const fromIso = url.searchParams.get("from");
        const toIso = url.searchParams.get("to");
        if (!fromIso || !toIso) {
            return jsonResponse({error: "from and to are required for events query"}, 400);
        }
        const calendarIds = parseCalendarIds(url.searchParams);
        const [calendars, events, occurrences] = await Promise.all([
            listCalendarProfiles(),
            listCalendarEvents(),
            listOccurrencesInRange(fromIso, toIso, calendarIds)
        ]);
        return jsonResponse({
            calendars,
            events,
            occurrences
        });
    },
    async POST(req) {
        const body = await req.json();
        const action = String(body.action || "");
        if (!action) {
            return jsonResponse({error: "action is required"}, 400);
        }

        if (action === "createCalendar") {
            const name = String(body.name || "").trim();
            const color = String(body.color || "").trim();
            const tag = String(body.tag || "").trim();
            if (!name || !color) {
                return jsonResponse({error: "name and color are required"}, 400);
            }
            const created = await createCalendarProfile({name, color, tag});
            return jsonResponse({calendar: created}, 201);
        }

        if (action === "updateCalendar") {
            const id = String(body.id || "");
            const name = String(body.name || "").trim();
            const color = String(body.color || "").trim();
            const tag = String(body.tag || "").trim();
            if (!id || !name || !color) {
                return jsonResponse({error: "id, name and color are required"}, 400);
            }
            const updated = await updateCalendarProfile({id, name, color, tag});
            if (!updated) {
                return jsonResponse({error: "calendar not found"}, 404);
            }
            return jsonResponse({calendar: updated});
        }

        if (action === "deleteCalendar") {
            const id = String(body.id || "");
            if (!id) {
                return jsonResponse({error: "id is required"}, 400);
            }
            const deleted = await deleteCalendarProfile(id);
            if (!deleted) {
                return jsonResponse({error: "calendar not found"}, 404);
            }
            return jsonResponse({ok: true});
        }

        if (action === "createEvent") {
            const validation = validateEventInput(body);
            if (!validation.ok) {
                return jsonResponse({error: validation.error}, 400);
            }
            const created = await createCalendarEvent(validation.value);
            return jsonResponse({event: created}, 201);
        }

        if (action === "updateEvent") {
            const id = String(body.id || "");
            if (!id) {
                return jsonResponse({error: "id is required"}, 400);
            }
            const validation = validateEventInput(body);
            if (!validation.ok) {
                return jsonResponse({error: validation.error}, 400);
            }
            const updated = await updateCalendarEvent({id, ...validation.value});
            if (!updated) {
                return jsonResponse({error: "event not found"}, 404);
            }
            return jsonResponse({event: updated});
        }

        if (action === "deleteEvent") {
            const id = String(body.id || "");
            if (!id) {
                return jsonResponse({error: "id is required"}, 400);
            }
            const deleted = await deleteCalendarEvent(id);
            if (!deleted) {
                return jsonResponse({error: "event not found"}, 404);
            }
            return jsonResponse({ok: true});
        }

        return jsonResponse({error: "unsupported action"}, 400);
    }
};
