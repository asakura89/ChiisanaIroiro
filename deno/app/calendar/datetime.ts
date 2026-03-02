export function toIsoFromLocalInput(localValue: string): string {
    return new Date(localValue).toISOString();
}

export function toLocalInputValue(isoValue: string): string {
    const date = new Date(isoValue);
    const local = new Date(date.getTime() - (date.getTimezoneOffset() * 60000));
    return local.toISOString().slice(0, 16);
}

export function startOfDay(date: Date): Date {
    const result = new Date(date);
    result.setHours(0, 0, 0, 0);
    return result;
}

export function endOfDay(date: Date): Date {
    const result = startOfDay(date);
    result.setDate(result.getDate() + 1);
    return result;
}

export function startOfWeek(date: Date): Date {
    const result = startOfDay(date);
    const day = result.getDay();
    result.setDate(result.getDate() - day);
    return result;
}

export function endOfWeek(date: Date): Date {
    const result = startOfWeek(date);
    result.setDate(result.getDate() + 7);
    return result;
}

export function startOfMonth(date: Date): Date {
    const result = startOfDay(date);
    result.setDate(1);
    return result;
}

export function endOfMonth(date: Date): Date {
    const result = startOfMonth(date);
    result.setMonth(result.getMonth() + 1);
    return result;
}

export function addDays(date: Date, days: number): Date {
    const result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
}

export function addMonths(date: Date, months: number): Date {
    const result = new Date(date);
    result.setMonth(result.getMonth() + months);
    return result;
}

export function addYears(date: Date, years: number): Date {
    const result = new Date(date);
    result.setFullYear(result.getFullYear() + years);
    return result;
}

export function areIntervalsOverlapping(
    aStartIso: string,
    aEndIso: string,
    bStartIso: string,
    bEndIso: string
): boolean {
    const aStart = new Date(aStartIso).getTime();
    const aEnd = new Date(aEndIso).getTime();
    const bStart = new Date(bStartIso).getTime();
    const bEnd = new Date(bEndIso).getTime();
    return aStart < bEnd && bStart < aEnd;
}
