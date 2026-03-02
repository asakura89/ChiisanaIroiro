/* const dateValue: Date = new Date(value);
        if (isNaN(dateValue.getTime()))
            return false; */

const stripTime = (d: Date): Date => new Date(
    d.getFullYear(),
    d.getMonth(),
    d.getDate());

/* const isSameDay = (d1: Date, d2: Date): boolean =>
    d1.getFullYear() === d2.getFullYear() &&
    d1.getMonth() === d2.getMonth() &&
    d1.getDate() === d2.getDate(); */

const today: Date = stripTime(new Date());

// Week starts on Monday.
const firstDay = 1; // Monday is represented as 1.

// If today is Sunday (0), we treat it as 7.
const currentDay = today.getDay() === 0 ? 7 : today.getDay();
const adjustedDay = currentDay < firstDay ? currentDay +7 : currentDay;
const offset = adjustedDay - firstDay; // Days since Monday.
const currentWeekStart = new Date(today.getDate() - offset);
//currentWeekStart.setDate(today.getDate() - offset);
const lastWeekStart = new Date(currentWeekStart.getDate() -7);
//lastWeekStart.setDate(currentWeekStart.getDate() - 7);
const lastWeekEnd = new Date(lastWeekStart.getDate() +6);
//lastWeekEnd.setDate(lastWeekStart.getDate() +6);
//return dateValue >= lastWeekStart && dateValue <= lastWeekEnd;

console.table({
    currentDay: currentDay,
    adjustedDay: adjustedDay,
    offset: offset,
    currentWeekStart: currentWeekStart,
    lastWeekStart: lastWeekStart,
    lastWeekEnd: lastWeekEnd
});

//assert(year.value === 1447);
//assert(globalThis.NumberPrimitiveReaction === "Value changed → 1447");

//assert();