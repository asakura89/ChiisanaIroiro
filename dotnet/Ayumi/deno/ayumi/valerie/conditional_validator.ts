import {isNullOrEmpty} from "../kayo-core/string_helper.ts";

export enum Condition {
    // Numbers
    NumberBetween = "between",
    NumberNotBetween = "not between",
    NumberEqualTo = "equal to",
    NumberNotEqualTo = "not equal to",
    NumberGreaterThan = "greater than",
    NumberLessThan = "less than",
    NumberGreaterThanOrEqualTo = "greater than or equal to",
    NumberLessThanOrEqualTo = "less than or equal to",

    // Texts
    TextIsExactly = "is exactly",
    TextNotExactly = "not exactly",
    TextContains = "contains",
    TextDoesntContains = "doesnt contains",
    TextBeginsWith = "begins with",
    TextEndsWith = "ends with",
    TextIsBlank = "is blank",
    TextNotBlank = "not blank",

    // Dates
    DateYesterday = "yesterday",
    DateToday = "today",
    DateTomorrow = "tomorrow",
    DateInTheLast7Days = "in the last 7 days",
    DateLastWeek = "last week",
    DateThisWeek = "this week",
    DateNextWeek = "next week",
    DateLastMonth = "last month",
    DateThisMonth = "this month",
    DateNextMonth = "next month",
    DateLastYear = "last year",
    DateThisYear = "this year",
    DateNextYear = "next year"
}

export function validateNumber(value: string, lowered: string, compare1: string, compare2?: string): boolean {
    if (isNullOrEmpty(value))
        return false;

    const numOfValue: number = parseFloat(value);
    if (isNaN(numOfValue))
        return false;

    if (isNullOrEmpty(compare1))
        return false;

    const  numOfCompare1: number = parseFloat(compare1);
    if (isNaN(numOfCompare1))
        return false;

    switch (lowered) {
        case Condition.NumberBetween:
        case Condition.NumberNotBetween: {
            if (isNullOrEmpty(compare2))
                return false;

            const numOfCompare2: number = compare2 ? parseFloat(compare2) : NaN;
            if (isNaN(numOfCompare2))
                return false;

            const result: boolean = numOfValue >= numOfCompare1 && numOfValue <= numOfCompare2;
            return lowered === Condition.NumberBetween ? result : !result;
        }
        case Condition.NumberEqualTo:
            return numOfValue === numOfCompare1;
        case Condition.NumberNotEqualTo:
            return numOfValue !== numOfCompare1;
        case Condition.NumberGreaterThan:
            return numOfValue > numOfCompare1;
        case Condition.NumberLessThan:
            return numOfValue < numOfCompare1;
        case Condition.NumberGreaterThanOrEqualTo:
            return numOfValue >= numOfCompare1;
        case Condition.NumberLessThanOrEqualTo:
            return numOfValue <= numOfCompare1;
        default:
            return false;
    }
}

export function validateText(value: string, lowered: string, compare1: string): boolean {
    if (!value)
        return false;

    if (!compare1)
        return false;

    switch (lowered) {
        case Condition.TextIsExactly:
            return value === compare1;
        case Condition.TextNotExactly:
            return value !== compare1;
        case Condition.TextContains:
            return value.includes(compare1);
        case Condition.TextDoesntContains:
            return !value.includes(compare1);
        case Condition.TextBeginsWith:
            return value.startsWith(compare1);
        case Condition.TextEndsWith:
            return value.endsWith(compare1);
        case Condition.TextIsBlank:
            return value.trim() === "";
        case Condition.TextNotBlank:
            return value.trim() !== "";
        default:
            return false;
    }
}

export function validateDate(value: string, lowered: string): boolean {
    const dateValue: Date = new Date(value);
    if (isNaN(dateValue.getTime()))
        return false;

    const stripTime = (d: Date): Date => new Date(
        d.getFullYear(),
        d.getMonth(),
        d.getDate());

    const isSameDay = (d1: Date, d2: Date): boolean =>
        d1.getFullYear() === d2.getFullYear() &&
        d1.getMonth() === d2.getMonth() &&
        d1.getDate() === d2.getDate();

    const today: Date = stripTime(new Date());

    switch (lowered) {
        case Condition.DateYesterday: {
            const yesterday = new Date(today);
            yesterday.setDate(today.getDate() -1);
            return isSameDay(dateValue, yesterday);
        }
        case Condition.DateToday:
            return isSameDay(dateValue, today);
        case Condition.DateTomorrow: {
            const tomorrow = new Date(today);
            tomorrow.setDate(today.getDate() +1);
            return isSameDay(dateValue, tomorrow);
        }
        case Condition.DateInTheLast7Days: {
            // Last 7 days: today and the 6 days before today.
            const startDate = new Date(today);
            startDate.setDate(today.getDate() -6);
            return dateValue >= startDate && dateValue <= today;
        }
        case Condition.DateLastWeek: {
            // Week starts on Sunday
            const currentWeekStart = new Date(today);
            currentWeekStart.setDate(today.getDate() - today.getDay());
            const lastWeekStart = new Date(currentWeekStart);
            lastWeekStart.setDate(currentWeekStart.getDate() - 7);
            const lastWeekEnd = new Date(lastWeekStart);
            lastWeekEnd.setDate(lastWeekStart.getDate() +6);
            return dateValue >= lastWeekStart && dateValue <= lastWeekEnd;
        }
        case Condition.DateThisWeek: {
            const weekStart = new Date(today);
            weekStart.setDate(today.getDate() - today.getDay());
            const weekEnd = new Date(weekStart);
            weekEnd.setDate(weekStart.getDate() +6);
            return dateValue >= weekStart && dateValue <= weekEnd;
        }
        case Condition.DateNextWeek: {
            const currentWeekStart = new Date(today);
            currentWeekStart.setDate(today.getDate() - today.getDay());
            const nextWeekStart = new Date(currentWeekStart);
            nextWeekStart.setDate(currentWeekStart.getDate() +7);
            const nextWeekEnd = new Date(nextWeekStart);
            nextWeekEnd.setDate(nextWeekStart.getDate() +6);
            return dateValue >= nextWeekStart && dateValue <= nextWeekEnd;
        }
        case Condition.DateLastMonth: {
            const year: number = today.getFullYear();
            const month: number = today.getMonth();
            let lastMonth: number, lastYear: number;
            if (month === 0) {
                lastMonth = 11;
                lastYear = year -1;
            }
            else {
                lastMonth = month -1;
                lastYear = year;
            }
            const start = new Date(lastYear, lastMonth, 1);
            const end = new Date(lastYear, lastMonth +1, 0);
            return dateValue >= start && dateValue <= end;
        }
        case Condition.DateThisMonth: {
            const start = new Date(today.getFullYear(), today.getMonth(), 1);
            const end = new Date(today.getFullYear(), today.getMonth() +1, 0);
            return dateValue >= start && dateValue <= end;
        }
        case Condition.DateNextMonth: {
            const year = today.getFullYear();
            const month = today.getMonth();
            let nextMonth: number, nextYear: number;
            if (month === 11) {
                nextMonth = 0;
                nextYear = year +1;
            }
            else {
                nextMonth = month +1;
                nextYear = year;
            }
            const start = new Date(nextYear, nextMonth, 1);
            const end = new Date(nextYear, nextMonth +1, 0);
            return dateValue >= start && dateValue <= end;
        }
        case Condition.DateLastYear: {
            const lastYear = today.getFullYear() -1;
            const start = new Date(lastYear, 0, 1);
            const end = new Date(lastYear, 11, 31);
            return dateValue >= start && dateValue <= end;
        }
        case Condition.DateThisYear: {
            const year = today.getFullYear();
            const start = new Date(year, 0, 1);
            const end = new Date(year, 11, 31);
            return dateValue >= start && dateValue <= end;
        }
        case Condition.DateNextYear: {
            const nextYear = today.getFullYear() +1;
            const start = new Date(nextYear, 0, 1);
            const end = new Date(nextYear, 11, 31);
            return dateValue >= start && dateValue <= end;
        }
        default:
            return false;
    }
}

export function validateConditionally(value: string, condition: Condition, compare1: string, compare2?: string): boolean {
    const lowered: string = condition.toLowerCase().trim();

    const isNumber: boolean = lowered === Condition.NumberBetween ||
        lowered === Condition.NumberNotBetween ||
        lowered === Condition.NumberEqualTo ||
        lowered === Condition.NumberNotEqualTo ||
        lowered === Condition.NumberGreaterThan ||
        lowered === Condition.NumberLessThan ||
        lowered === Condition.NumberGreaterThanOrEqualTo ||
        lowered === Condition.NumberLessThanOrEqualTo;

    if (isNumber)
        return validateNumber(value, lowered, compare1, compare2);

    const isText: boolean = lowered === Condition.TextIsExactly ||
        lowered === Condition.TextNotExactly ||
        lowered === Condition.TextContains ||
        lowered === Condition.TextDoesntContains ||
        lowered === Condition.TextBeginsWith ||
        lowered === Condition.TextEndsWith ||
        lowered === Condition.TextIsBlank ||
        lowered === Condition.TextNotBlank;

    if (isText)
        return validateText(value, lowered, compare1);

    return validateDate(value, lowered);
}
