import * as React from "@preact/compat";
import { Handlers, PageProps } from "$fresh/server.ts";
import HtmlHead from "../../components/server/HtmlHead.tsx";
import CalendarAppIsland from "../../islands/CalendarApp.tsx";
import {
    listCalendarEvents,
    listCalendarProfiles,
    listOccurrencesInRange
} from "../../app/calendar/kv_repository.ts";
import {
    addDays,
    startOfDay
} from "../../app/calendar/datetime.ts";
import { CalendarEvent, CalendarEventOccurrence, CalendarProfile } from "../../app/calendar/types.ts";

interface CalendarPageData {
    calendars: CalendarProfile[];
    events: CalendarEvent[];
    occurrences: CalendarEventOccurrence[];
    rangeStartIso: string;
    rangeEndIso: string;
}

export const handler: Handlers<CalendarPageData> = {
    async GET(_req, ctx) {
        const now = new Date();
        const rangeStart = startOfDay(addDays(now, -35));
        const rangeEnd = startOfDay(addDays(now, 65));
        const [calendars, events, occurrences] = await Promise.all([
            listCalendarProfiles(),
            listCalendarEvents(),
            listOccurrencesInRange(rangeStart.toISOString(), rangeEnd.toISOString(), null)
        ]);
        return ctx.render({
            calendars,
            events,
            occurrences,
            rangeStartIso: rangeStart.toISOString(),
            rangeEndIso: rangeEnd.toISOString()
        });
    }
};

export default function CalendarPage(props: PageProps<CalendarPageData>) {
    const componentName = "Calendar";
    props.Component.displayName = componentName;

    return (
        <>
            <HtmlHead componentName={componentName} />
            <section class="calendar-page-shell">
                <CalendarAppIsland initialData={props.data} />
            </section>
        </>
    );
}
