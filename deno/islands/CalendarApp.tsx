import { useMemo, useState } from "preact/hooks";
import {
    addDays,
    endOfDay,
    endOfMonth,
    endOfWeek,
    startOfDay,
    startOfMonth,
    startOfWeek,
    toIsoFromLocalInput,
    toLocalInputValue
} from "../app/calendar/datetime.ts";
import {
    CalendarEvent,
    CalendarEventOccurrence,
    CalendarProfile,
    CalendarView,
    RecurrenceRule
} from "../app/calendar/types.ts";

interface CalendarPageData {
    calendars: CalendarProfile[];
    events: CalendarEvent[];
    occurrences: CalendarEventOccurrence[];
    rangeStartIso: string;
    rangeEndIso: string;
}

interface CalendarAppProps {
    initialData: CalendarPageData;
}

interface EventFormState {
    id: string;
    calendarId: string;
    title: string;
    description: string;
    location: string;
    startLocal: string;
    endLocal: string;
    allDay: boolean;
    recurrenceEnabled: boolean;
    recurrenceFrequency: RecurrenceRule["frequency"];
    recurrenceInterval: number;
    recurrenceUntilLocal: string;
    recurrenceCount: string;
    recurrenceByWeekday: number[];
}

interface CalendarFormState {
    id: string;
    name: string;
    color: string;
    tag: string;
}

function formatWeekdayShort(day: number): string {
    return ["S", "M", "T", "W", "T", "F", "S"][day];
}

function formatMonthTitle(date: Date): string {
    return new Intl.DateTimeFormat("en-US", {month: "long", year: "numeric"}).format(date);
}

function formatDayTitle(date: Date): string {
    return new Intl.DateTimeFormat("en-US", {weekday: "long", day: "numeric", month: "short"}).format(date);
}

function getRangeForView(view: CalendarView, selectedDate: Date): { fromIso: string; toIso: string } {
    if (view === "day") {
        return {
            fromIso: startOfDay(addDays(selectedDate, -3)).toISOString(),
            toIso: endOfDay(addDays(selectedDate, 4)).toISOString()
        };
    }
    if (view === "week") {
        return {
            fromIso: startOfWeek(addDays(selectedDate, -14)).toISOString(),
            toIso: endOfWeek(addDays(selectedDate, 21)).toISOString()
        };
    }
    if (view === "agenda") {
        return {
            fromIso: startOfDay(addDays(selectedDate, -1)).toISOString(),
            toIso: endOfDay(addDays(selectedDate, 90)).toISOString()
        };
    }
    return {
        fromIso: startOfWeek(addDays(startOfMonth(selectedDate), -14)).toISOString(),
        toIso: endOfWeek(addDays(endOfMonth(selectedDate), 21)).toISOString()
    };
}

function buildMonthMatrix(anchorDate: Date): Date[][] {
    const monthStart = startOfMonth(anchorDate);
    const matrixStart = startOfWeek(monthStart);
    const matrix: Date[][] = [];
    for (let row = 0; row < 6; row += 1) {
        const rowValues: Date[] = [];
        for (let col = 0; col < 7; col += 1) {
            rowValues.push(addDays(matrixStart, row * 7 + col));
        }
        matrix.push(rowValues);
    }
    return matrix;
}

function sameDate(left: Date, right: Date): boolean {
    return left.getFullYear() === right.getFullYear() &&
        left.getMonth() === right.getMonth() &&
        left.getDate() === right.getDate();
}

function defaultEventForm(calendarId: string, baseDate: Date): EventFormState {
    const start = new Date(baseDate);
    start.setMinutes(0, 0, 0);
    const end = new Date(start);
    end.setHours(end.getHours() + 1);
    return {
        id: "",
        calendarId,
        title: "",
        description: "",
        location: "",
        startLocal: toLocalInputValue(start.toISOString()),
        endLocal: toLocalInputValue(end.toISOString()),
        allDay: false,
        recurrenceEnabled: false,
        recurrenceFrequency: "weekly",
        recurrenceInterval: 1,
        recurrenceUntilLocal: "",
        recurrenceCount: "",
        recurrenceByWeekday: [start.getDay()]
    };
}

function fromEventToForm(event: CalendarEvent): EventFormState {
    const recurrence = event.recurrence;
    return {
        id: event.id,
        calendarId: event.calendarId,
        title: event.title,
        description: event.description,
        location: event.location,
        startLocal: toLocalInputValue(event.startIso),
        endLocal: toLocalInputValue(event.endIso),
        allDay: event.allDay,
        recurrenceEnabled: recurrence !== null,
        recurrenceFrequency: recurrence?.frequency || "weekly",
        recurrenceInterval: recurrence?.interval || 1,
        recurrenceUntilLocal: recurrence?.untilIso ? toLocalInputValue(recurrence.untilIso) : "",
        recurrenceCount: recurrence?.count ? String(recurrence.count) : "",
        recurrenceByWeekday: recurrence?.byWeekday || [new Date(event.startIso).getDay()]
    };
}

function getHourLabel(hour: number): string {
    const suffix = hour >= 12 ? "pm" : "am";
    const normalized = hour % 12 === 0 ? 12 : hour % 12;
    return `${normalized} ${suffix}`;
}

export default function CalendarApp({initialData}: CalendarAppProps) {
    const [view, setView] = useState<CalendarView>("day");
    const [selectedDate, setSelectedDate] = useState(new Date());
    const [panelMonthDate, setPanelMonthDate] = useState(startOfMonth(new Date()));
    const [searchTerm, setSearchTerm] = useState("");
    const [calendars, setCalendars] = useState<CalendarProfile[]>(initialData.calendars);
    const [events, setEvents] = useState<CalendarEvent[]>(initialData.events);
    const [occurrences, setOccurrences] = useState<CalendarEventOccurrence[]>(initialData.occurrences);
    const [activeCalendarIds, setActiveCalendarIds] = useState<string[]>(initialData.calendars.map((item) => item.id));
    const [loading, setLoading] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const [eventDialogOpen, setEventDialogOpen] = useState(false);
    const [calendarDialogOpen, setCalendarDialogOpen] = useState(false);
    const [eventForm, setEventForm] = useState<EventFormState>(
        defaultEventForm(initialData.calendars[0]?.id || "", new Date())
    );
    const [calendarForm, setCalendarForm] = useState<CalendarFormState>({
        id: "",
        name: "",
        color: "#1f6feb",
        tag: ""
    });

    const range = useMemo(() => getRangeForView(view, selectedDate), [view, selectedDate]);

    const calendarMap = useMemo(() => {
        const map = new Map<string, CalendarProfile>();
        for (const calendar of calendars) {
            map.set(calendar.id, calendar);
        }
        return map;
    }, [calendars]);

    const filteredOccurrences = useMemo(() => {
        const query = searchTerm.trim().toLowerCase();
        return occurrences.filter((occurrence) => {
            if (!activeCalendarIds.includes(occurrence.calendarId)) {
                return false;
            }
            if (!query) {
                return true;
            }
            const combined = `${occurrence.title} ${occurrence.location} ${occurrence.description}`.toLowerCase();
            return combined.includes(query);
        });
    }, [occurrences, activeCalendarIds, searchTerm]);

    const selectedDateOccurrences = useMemo(() => {
        const dayStart = startOfDay(selectedDate).getTime();
        const dayEnd = endOfDay(selectedDate).getTime();
        return filteredOccurrences.filter((item) => {
            const itemStart = new Date(item.startIso).getTime();
            return itemStart >= dayStart && itemStart < dayEnd;
        });
    }, [selectedDate, filteredOccurrences]);

    async function reloadInRange(fromIso: string, toIso: string) {
        const params = new URLSearchParams();
        params.set("kind", "events");
        params.set("from", fromIso);
        params.set("to", toIso);
        for (const calendarId of activeCalendarIds) {
            params.append("calendarId", calendarId);
        }
        setLoading(true);
        setErrorMessage("");
        try {
            const response = await fetch(`/api/calendar/events?${params.toString()}`);
            if (!response.ok) {
                throw new Error(`Failed to load events: ${response.status}`);
            }
            const payload = await response.json();
            setCalendars(payload.calendars || []);
            setEvents(payload.events || []);
            setOccurrences(payload.occurrences || []);
        } catch (error) {
            setErrorMessage(error instanceof Error ? error.message : "Failed to load data");
        } finally {
            setLoading(false);
        }
    }

    async function loadCurrentRange() {
        await reloadInRange(range.fromIso, range.toIso);
    }

    function onSelectView(nextView: CalendarView) {
        setView(nextView);
        const nextRange = getRangeForView(nextView, selectedDate);
        reloadInRange(nextRange.fromIso, nextRange.toIso);
    }

    function onMoveDate(offset: number) {
        const nextDate = new Date(selectedDate);
        if (view === "month") {
            nextDate.setMonth(nextDate.getMonth() + offset);
        } else if (view === "week") {
            nextDate.setDate(nextDate.getDate() + (offset * 7));
        } else {
            nextDate.setDate(nextDate.getDate() + offset);
        }
        setSelectedDate(nextDate);
        setPanelMonthDate(startOfMonth(nextDate));
        const nextRange = getRangeForView(view, nextDate);
        reloadInRange(nextRange.fromIso, nextRange.toIso);
    }

    function onJumpToday() {
        const today = new Date();
        setSelectedDate(today);
        setPanelMonthDate(startOfMonth(today));
        const nextRange = getRangeForView(view, today);
        reloadInRange(nextRange.fromIso, nextRange.toIso);
    }

    function onToggleCalendar(calendarId: string) {
        const next = activeCalendarIds.includes(calendarId)
            ? activeCalendarIds.filter((id) => id !== calendarId)
            : [...activeCalendarIds, calendarId];
        setActiveCalendarIds(next);
    }

    function openCreateEventDialog(baseDate: Date) {
        setEventForm(defaultEventForm(activeCalendarIds[0] || calendars[0]?.id || "", baseDate));
        setEventDialogOpen(true);
    }

    function openEditEventDialog(eventId: string) {
        const event = events.find((item) => item.id === eventId);
        if (!event) {
            return;
        }
        setEventForm(fromEventToForm(event));
        setEventDialogOpen(true);
    }

    async function submitEventForm(event: Event) {
        event.preventDefault();
        const recurrence = eventForm.recurrenceEnabled
            ? {
                frequency: eventForm.recurrenceFrequency,
                interval: Number(eventForm.recurrenceInterval) || 1,
                untilIso: eventForm.recurrenceUntilLocal ? toIsoFromLocalInput(eventForm.recurrenceUntilLocal) : null,
                count: eventForm.recurrenceCount ? Number(eventForm.recurrenceCount) : null,
                byWeekday: eventForm.recurrenceByWeekday
            }
            : null;
        const payload = {
            action: eventForm.id ? "updateEvent" : "createEvent",
            id: eventForm.id,
            calendarId: eventForm.calendarId,
            title: eventForm.title,
            description: eventForm.description,
            location: eventForm.location,
            startIso: toIsoFromLocalInput(eventForm.startLocal),
            endIso: toIsoFromLocalInput(eventForm.endLocal),
            allDay: eventForm.allDay,
            recurrence
        };
        setLoading(true);
        setErrorMessage("");
        try {
            const response = await fetch("/api/calendar/events", {
                method: "POST",
                headers: {"content-type": "application/json"},
                body: JSON.stringify(payload)
            });
            if (!response.ok) {
                const body = await response.json();
                throw new Error(body.error || "Failed to save event");
            }
            setEventDialogOpen(false);
            await loadCurrentRange();
        } catch (error) {
            setErrorMessage(error instanceof Error ? error.message : "Failed to save event");
        } finally {
            setLoading(false);
        }
    }

    async function deleteEvent(eventId: string) {
        if (!confirm("Delete this event?")) {
            return;
        }
        setLoading(true);
        setErrorMessage("");
        try {
            const response = await fetch("/api/calendar/events", {
                method: "POST",
                headers: {"content-type": "application/json"},
                body: JSON.stringify({action: "deleteEvent", id: eventId})
            });
            if (!response.ok) {
                const body = await response.json();
                throw new Error(body.error || "Failed to delete event");
            }
            if (eventDialogOpen && eventForm.id === eventId) {
                setEventDialogOpen(false);
            }
            await loadCurrentRange();
        } catch (error) {
            setErrorMessage(error instanceof Error ? error.message : "Failed to delete event");
        } finally {
            setLoading(false);
        }
    }

    function openCreateCalendarDialog() {
        setCalendarForm({
            id: "",
            name: "",
            color: "#1f6feb",
            tag: ""
        });
        setCalendarDialogOpen(true);
    }

    function openEditCalendarDialog(calendar: CalendarProfile) {
        setCalendarForm({
            id: calendar.id,
            name: calendar.name,
            color: calendar.color,
            tag: calendar.tag
        });
        setCalendarDialogOpen(true);
    }

    async function submitCalendarForm(event: Event) {
        event.preventDefault();
        const payload = {
            action: calendarForm.id ? "updateCalendar" : "createCalendar",
            id: calendarForm.id,
            name: calendarForm.name,
            color: calendarForm.color,
            tag: calendarForm.tag
        };
        setLoading(true);
        setErrorMessage("");
        try {
            const response = await fetch("/api/calendar/events", {
                method: "POST",
                headers: {"content-type": "application/json"},
                body: JSON.stringify(payload)
            });
            if (!response.ok) {
                const body = await response.json();
                throw new Error(body.error || "Failed to save calendar");
            }
            setCalendarDialogOpen(false);
            await loadCurrentRange();
        } catch (error) {
            setErrorMessage(error instanceof Error ? error.message : "Failed to save calendar");
        } finally {
            setLoading(false);
        }
    }

    async function deleteCalendar(calendarId: string) {
        if (!confirm("Delete this calendar and all its events?")) {
            return;
        }
        setLoading(true);
        setErrorMessage("");
        try {
            const response = await fetch("/api/calendar/events", {
                method: "POST",
                headers: {"content-type": "application/json"},
                body: JSON.stringify({action: "deleteCalendar", id: calendarId})
            });
            if (!response.ok) {
                const body = await response.json();
                throw new Error(body.error || "Failed to delete calendar");
            }
            setActiveCalendarIds((prev) => prev.filter((id) => id !== calendarId));
            if (eventForm.calendarId === calendarId) {
                setEventForm((prev) => ({
                    ...prev,
                    calendarId: ""
                }));
            }
            await loadCurrentRange();
        } catch (error) {
            setErrorMessage(error instanceof Error ? error.message : "Failed to delete calendar");
        } finally {
            setLoading(false);
        }
    }

    const miniMonthMatrix = useMemo(() => buildMonthMatrix(panelMonthDate), [panelMonthDate]);
    const monthMatrix = useMemo(() => buildMonthMatrix(selectedDate), [selectedDate]);

    function renderDayEventsList(date: Date, compact = false) {
        const dayStart = startOfDay(date).getTime();
        const dayEnd = endOfDay(date).getTime();
        const dayEvents = filteredOccurrences.filter((item) => {
            const start = new Date(item.startIso).getTime();
            return start >= dayStart && start < dayEnd;
        });
        if (dayEvents.length === 0) {
            return compact ? <span class="calendar-empty-dot"></span> : <div class="calendar-empty-state">No events</div>;
        }
        if (compact) {
            return (
                <div class="calendar-day-chip-list">
                    {dayEvents.slice(0, 2).map((item) => {
                        const calendar = calendarMap.get(item.calendarId);
                        return (
                            <button
                                class="calendar-day-chip"
                                style={{borderColor: calendar?.color || "#5f6368"}}
                                onClick={() => openEditEventDialog(item.eventId)}
                            >
                                {item.title}
                            </button>
                        );
                    })}
                    {dayEvents.length > 2 && <span class="calendar-more-indicator">+{dayEvents.length - 2}</span>}
                </div>
            );
        }
        return (
            <div class="calendar-day-events-stack">
                {dayEvents.map((item) => {
                    const calendar = calendarMap.get(item.calendarId);
                    const timeLabel = new Intl.DateTimeFormat("en-US", {
                        hour: "numeric",
                        minute: "2-digit"
                    }).format(new Date(item.startIso));
                    return (
                        <button
                            class="calendar-day-event-card"
                            style={{borderLeftColor: calendar?.color || "#5f6368"}}
                            onClick={() => openEditEventDialog(item.eventId)}
                        >
                            <div class="calendar-day-event-title">{item.title}</div>
                            <div class="calendar-day-event-meta">{timeLabel}</div>
                        </button>
                    );
                })}
            </div>
        );
    }

    return (
        <div class="calendar-shell">
            <div class="calendar-header-row">
                <h1 class="calendar-title">Calendar</h1>
                <div class="calendar-header-actions">
                    <button class="calendar-button-secondary" onClick={() => onMoveDate(-1)}>&lt;</button>
                    <button class="calendar-button-secondary" onClick={() => onMoveDate(1)}>&gt;</button>
                    <button class="calendar-button-secondary" onClick={onJumpToday}>Today</button>
                    <select
                        class="calendar-view-select"
                        value={view}
                        onChange={(event) => onSelectView((event.currentTarget.value as CalendarView))}
                    >
                        <option value="day">Day</option>
                        <option value="week">Week</option>
                        <option value="month">Month</option>
                        <option value="agenda">Agenda</option>
                    </select>
                    <button class="calendar-button-primary" onClick={() => openCreateEventDialog(selectedDate)}>New event</button>
                </div>
            </div>

            <div class="calendar-search-row">
                <input
                    class="calendar-search-input"
                    placeholder="Search for a calendar"
                    value={searchTerm}
                    onInput={(event) => setSearchTerm(event.currentTarget.value)}
                />
                <button class="calendar-button-secondary" onClick={openCreateCalendarDialog}>New calendar</button>
                <button class="calendar-button-secondary" onClick={loadCurrentRange}>Refresh</button>
            </div>

            <div class="calendar-date-title">{formatDayTitle(selectedDate)}</div>

            {errorMessage && <div class="calendar-error-banner">{errorMessage}</div>}
            {loading && <div class="calendar-loading-banner">Loading...</div>}

            <div class="calendar-main-layout">
                <aside class="calendar-left-panel">
                    <div class="calendar-month-panel">
                        <div class="calendar-month-panel-header">
                            <button
                                class="calendar-nav-inline"
                                onClick={() => setPanelMonthDate(addDays(startOfMonth(panelMonthDate), -1))}
                            >
                                &lt;
                            </button>
                            <strong>{formatMonthTitle(panelMonthDate)}</strong>
                            <button
                                class="calendar-nav-inline"
                                onClick={() => setPanelMonthDate(addDays(endOfMonth(panelMonthDate), 1))}
                            >
                                &gt;
                            </button>
                        </div>
                        <div class="calendar-month-weekdays">
                            {Array.from({length: 7}, (_, day) => <span>{formatWeekdayShort(day)}</span>)}
                        </div>
                        <div class="calendar-month-grid">
                            {miniMonthMatrix.flat().map((date) => {
                                const inCurrentMonth = date.getMonth() === panelMonthDate.getMonth();
                                const selected = sameDate(date, selectedDate);
                                return (
                                    <button
                                        class={`calendar-month-cell ${inCurrentMonth ? "" : "calendar-month-cell-muted"} ${selected ? "calendar-month-cell-selected" : ""}`}
                                        onClick={() => {
                                            setSelectedDate(date);
                                            setPanelMonthDate(startOfMonth(date));
                                            const nextRange = getRangeForView(view, date);
                                            reloadInRange(nextRange.fromIso, nextRange.toIso);
                                        }}
                                    >
                                        {date.getDate()}
                                    </button>
                                );
                            })}
                        </div>
                    </div>

                    <div class="calendar-list-panel">
                        <div class="calendar-list-header">Calendars</div>
                        <div class="calendar-list-items">
                            {calendars.map((calendar) => (
                                <div class="calendar-list-item">
                                    <label class="calendar-checkbox-label">
                                        <input
                                            type="checkbox"
                                            checked={activeCalendarIds.includes(calendar.id)}
                                            onChange={() => onToggleCalendar(calendar.id)}
                                        />
                                        <span class="calendar-color-dot" style={{backgroundColor: calendar.color}}></span>
                                        <span>{calendar.name}</span>
                                        {calendar.tag && <small class="calendar-tag-pill">{calendar.tag}</small>}
                                    </label>
                                    <button class="calendar-text-button" onClick={() => openEditCalendarDialog(calendar)}>Edit</button>
                                    <button class="calendar-text-button danger" onClick={() => deleteCalendar(calendar.id)}>Delete</button>
                                </div>
                            ))}
                        </div>
                    </div>
                </aside>

                <section class="calendar-content-panel">
                    {view === "day" && (
                        <div class="calendar-day-view">
                            <div class="calendar-time-grid">
                                {Array.from({length: 24}, (_, hour) => {
                                    const hourEvents = selectedDateOccurrences.filter((item) => new Date(item.startIso).getHours() === hour);
                                    return (
                                        <div class="calendar-time-row">
                                            <div class="calendar-time-label">{getHourLabel(hour)}</div>
                                            <div class="calendar-time-slot" onDblClick={() => {
                                                const next = new Date(selectedDate);
                                                next.setHours(hour, 0, 0, 0);
                                                openCreateEventDialog(next);
                                            }}>
                                                {hourEvents.map((item) => {
                                                    const calendar = calendarMap.get(item.calendarId);
                                                    return (
                                                        <button
                                                            class="calendar-time-event"
                                                            style={{backgroundColor: calendar?.color || "#1f6feb"}}
                                                            onClick={() => openEditEventDialog(item.eventId)}
                                                        >
                                                            <span>{item.title}</span>
                                                            <small>{new Intl.DateTimeFormat("en-US", {
                                                                hour: "numeric",
                                                                minute: "2-digit"
                                                            }).format(new Date(item.startIso))}</small>
                                                        </button>
                                                    );
                                                })}
                                            </div>
                                        </div>
                                    );
                                })}
                            </div>
                        </div>
                    )}

                    {view === "week" && (
                        <div class="calendar-week-view">
                            <div class="calendar-week-grid">
                                {Array.from({length: 7}, (_, offset) => {
                                    const date = addDays(startOfWeek(selectedDate), offset);
                                    return (
                                        <div class="calendar-week-column">
                                            <button class="calendar-week-column-header" onClick={() => {
                                                setSelectedDate(date);
                                                onSelectView("day");
                                            }}>
                                                {new Intl.DateTimeFormat("en-US", {weekday: "short", day: "numeric"}).format(date)}
                                            </button>
                                            <div class="calendar-week-column-events">
                                                {renderDayEventsList(date, false)}
                                            </div>
                                        </div>
                                    );
                                })}
                            </div>
                        </div>
                    )}

                    {view === "month" && (
                        <div class="calendar-month-view">
                            <div class="calendar-month-weekdays full">
                                {Array.from({length: 7}, (_, day) => <span>{formatWeekdayShort(day)}</span>)}
                            </div>
                            <div class="calendar-month-view-grid">
                                {monthMatrix.flat().map((date) => {
                                    const inCurrentMonth = date.getMonth() === selectedDate.getMonth();
                                    const selected = sameDate(date, selectedDate);
                                    return (
                                        <div class={`calendar-month-view-cell ${inCurrentMonth ? "" : "muted"} ${selected ? "selected" : ""}`}>
                                            <button class="calendar-month-view-date" onClick={() => {
                                                setSelectedDate(date);
                                                onSelectView("day");
                                            }}>
                                                {date.getDate()}
                                            </button>
                                            {renderDayEventsList(date, true)}
                                        </div>
                                    );
                                })}
                            </div>
                        </div>
                    )}

                    {view === "agenda" && (
                        <div class="calendar-agenda-view">
                            {filteredOccurrences
                                .filter((item) => new Date(item.startIso).getTime() >= startOfDay(selectedDate).getTime())
                                .slice(0, 150)
                                .map((item) => {
                                    const calendar = calendarMap.get(item.calendarId);
                                    return (
                                        <button class="calendar-agenda-item" onClick={() => openEditEventDialog(item.eventId)}>
                                            <div class="calendar-agenda-color" style={{backgroundColor: calendar?.color || "#1f6feb"}}></div>
                                            <div class="calendar-agenda-content">
                                                <div class="calendar-agenda-title">{item.title}</div>
                                                <div class="calendar-agenda-meta">
                                                    {new Intl.DateTimeFormat("en-US", {
                                                        weekday: "short",
                                                        month: "short",
                                                        day: "numeric",
                                                        hour: "numeric",
                                                        minute: "2-digit"
                                                    }).format(new Date(item.startIso))}
                                                </div>
                                            </div>
                                        </button>
                                    );
                                })}
                        </div>
                    )}
                </section>
            </div>

            {eventDialogOpen && (
                <div class="calendar-dialog-backdrop">
                    <form class="calendar-dialog" onSubmit={submitEventForm}>
                        <div class="calendar-dialog-header">
                            <h2>{eventForm.id ? "Edit event" : "Create event"}</h2>
                            <button type="button" class="calendar-nav-inline" onClick={() => setEventDialogOpen(false)}>X</button>
                        </div>
                        <label class="calendar-form-row">
                            <span>Title</span>
                            <input
                                required
                                value={eventForm.title}
                                onInput={(event) => setEventForm((prev) => ({...prev, title: event.currentTarget.value}))}
                            />
                        </label>
                        <label class="calendar-form-row">
                            <span>Calendar</span>
                            <select
                                required
                                value={eventForm.calendarId}
                                onChange={(event) => setEventForm((prev) => ({...prev, calendarId: event.currentTarget.value}))}
                            >
                                <option value="">Select calendar</option>
                                {calendars.map((calendar) => <option value={calendar.id}>{calendar.name}</option>)}
                            </select>
                        </label>
                        <div class="calendar-form-columns">
                            <label class="calendar-form-row">
                                <span>Start</span>
                                <input
                                    type="datetime-local"
                                    required
                                    value={eventForm.startLocal}
                                    onInput={(event) => setEventForm((prev) => ({...prev, startLocal: event.currentTarget.value}))}
                                />
                            </label>
                            <label class="calendar-form-row">
                                <span>End</span>
                                <input
                                    type="datetime-local"
                                    required
                                    value={eventForm.endLocal}
                                    onInput={(event) => setEventForm((prev) => ({...prev, endLocal: event.currentTarget.value}))}
                                />
                            </label>
                        </div>
                        <label class="calendar-inline-check">
                            <input
                                type="checkbox"
                                checked={eventForm.allDay}
                                onChange={(event) => setEventForm((prev) => ({...prev, allDay: event.currentTarget.checked}))}
                            />
                            <span>All day</span>
                        </label>
                        <label class="calendar-form-row">
                            <span>Location</span>
                            <input
                                value={eventForm.location}
                                onInput={(event) => setEventForm((prev) => ({...prev, location: event.currentTarget.value}))}
                            />
                        </label>
                        <label class="calendar-form-row">
                            <span>Description</span>
                            <textarea
                                rows={3}
                                value={eventForm.description}
                                onInput={(event) => setEventForm((prev) => ({...prev, description: event.currentTarget.value}))}
                            />
                        </label>
                        <label class="calendar-inline-check">
                            <input
                                type="checkbox"
                                checked={eventForm.recurrenceEnabled}
                                onChange={(event) => setEventForm((prev) => ({...prev, recurrenceEnabled: event.currentTarget.checked}))}
                            />
                            <span>Recurring</span>
                        </label>
                        {eventForm.recurrenceEnabled && (
                            <div class="calendar-recurrence-box">
                                <div class="calendar-form-columns">
                                    <label class="calendar-form-row">
                                        <span>Frequency</span>
                                        <select
                                            value={eventForm.recurrenceFrequency}
                                            onChange={(event) => setEventForm((prev) => ({
                                                ...prev,
                                                recurrenceFrequency: event.currentTarget.value as RecurrenceRule["frequency"]
                                            }))}
                                        >
                                            <option value="daily">Daily</option>
                                            <option value="weekly">Weekly</option>
                                            <option value="monthly">Monthly</option>
                                            <option value="yearly">Yearly</option>
                                        </select>
                                    </label>
                                    <label class="calendar-form-row">
                                        <span>Interval</span>
                                        <input
                                            type="number"
                                            min={1}
                                            value={eventForm.recurrenceInterval}
                                            onInput={(event) => setEventForm((prev) => ({
                                                ...prev,
                                                recurrenceInterval: Number(event.currentTarget.value) || 1
                                            }))}
                                        />
                                    </label>
                                </div>
                                {eventForm.recurrenceFrequency === "weekly" && (
                                    <div class="calendar-weekday-selector">
                                        {Array.from({length: 7}, (_, weekday) => (
                                            <label class="calendar-inline-check">
                                                <input
                                                    type="checkbox"
                                                    checked={eventForm.recurrenceByWeekday.includes(weekday)}
                                                    onChange={(event) => {
                                                        const checked = event.currentTarget.checked;
                                                        setEventForm((prev) => ({
                                                            ...prev,
                                                            recurrenceByWeekday: checked
                                                                ? [...prev.recurrenceByWeekday, weekday].sort()
                                                                : prev.recurrenceByWeekday.filter((value) => value !== weekday)
                                                        }));
                                                    }}
                                                />
                                                <span>{formatWeekdayShort(weekday)}</span>
                                            </label>
                                        ))}
                                    </div>
                                )}
                                <div class="calendar-form-columns">
                                    <label class="calendar-form-row">
                                        <span>Until</span>
                                        <input
                                            type="datetime-local"
                                            value={eventForm.recurrenceUntilLocal}
                                            onInput={(event) => setEventForm((prev) => ({...prev, recurrenceUntilLocal: event.currentTarget.value}))}
                                        />
                                    </label>
                                    <label class="calendar-form-row">
                                        <span>Count</span>
                                        <input
                                            type="number"
                                            min={1}
                                            value={eventForm.recurrenceCount}
                                            onInput={(event) => setEventForm((prev) => ({...prev, recurrenceCount: event.currentTarget.value}))}
                                        />
                                    </label>
                                </div>
                            </div>
                        )}
                        <div class="calendar-dialog-actions">
                            {eventForm.id && (
                                <button
                                    type="button"
                                    class="calendar-button-danger"
                                    onClick={() => deleteEvent(eventForm.id)}
                                >
                                    Delete
                                </button>
                            )}
                            <button type="button" class="calendar-button-secondary" onClick={() => setEventDialogOpen(false)}>Cancel</button>
                            <button type="submit" class="calendar-button-primary">Save</button>
                        </div>
                    </form>
                </div>
            )}

            {calendarDialogOpen && (
                <div class="calendar-dialog-backdrop">
                    <form class="calendar-dialog small" onSubmit={submitCalendarForm}>
                        <div class="calendar-dialog-header">
                            <h2>{calendarForm.id ? "Edit calendar" : "Create calendar"}</h2>
                            <button type="button" class="calendar-nav-inline" onClick={() => setCalendarDialogOpen(false)}>X</button>
                        </div>
                        <label class="calendar-form-row">
                            <span>Name</span>
                            <input
                                required
                                value={calendarForm.name}
                                onInput={(event) => setCalendarForm((prev) => ({...prev, name: event.currentTarget.value}))}
                            />
                        </label>
                        <label class="calendar-form-row">
                            <span>Color</span>
                            <input
                                type="color"
                                required
                                value={calendarForm.color}
                                onInput={(event) => setCalendarForm((prev) => ({...prev, color: event.currentTarget.value}))}
                            />
                        </label>
                        <label class="calendar-form-row">
                            <span>Tag</span>
                            <input
                                value={calendarForm.tag}
                                onInput={(event) => setCalendarForm((prev) => ({...prev, tag: event.currentTarget.value}))}
                            />
                        </label>
                        <div class="calendar-dialog-actions">
                            {calendarForm.id && (
                                <button
                                    type="button"
                                    class="calendar-button-danger"
                                    onClick={() => deleteCalendar(calendarForm.id)}
                                >
                                    Delete
                                </button>
                            )}
                            <button type="button" class="calendar-button-secondary" onClick={() => setCalendarDialogOpen(false)}>Cancel</button>
                            <button type="submit" class="calendar-button-primary">Save</button>
                        </div>
                    </form>
                </div>
            )}
        </div>
    );
}