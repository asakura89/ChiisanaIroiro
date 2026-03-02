import { assert, assertEquals } from "jsr:@std/assert";
import { describe, it } from "jsr:@std/testing/bdd";
import { FakeTime } from "@std/testing/time";
import {
    Condition,
    validateConditionally,
    validateDate,
    validateNumber,
    validateText,
} from "../valerie/conditional_validator.ts";

const BASE_DATE = new Date("2024-05-15T00:00:00Z");

function withBaseDate(fn: () => void): void {
    const time = new FakeTime(BASE_DATE);
    try {
        fn();
    }
    finally {
        time.restore();
    }
}

// Number conditions
Deno.test("Condition.NumberBetween", (): void => {
    const result = validateNumber("5", Condition.NumberBetween, "1", "10");
    assert(result);
    assertEquals(
        validateConditionally("5", Condition.NumberBetween, "1", "10"),
        result,
    );
});

Deno.test("Condition.NumberNotBetween", (): void => {
    const result = validateNumber("5", Condition.NumberNotBetween, "10", "20");
    assert(result);
    assertEquals(
        validateConditionally("5", Condition.NumberNotBetween, "10", "20"),
        result,
    );
});

Deno.test("Condition.NumberEqualTo", (): void => {
    const result = validateNumber("5", Condition.NumberEqualTo, "5");
    assert(result);
    assertEquals(
        validateConditionally("5", Condition.NumberEqualTo, "5"),
        result,
    );
});

Deno.test("Condition.NumberNotEqualTo", (): void => {
    const result = validateNumber("5", Condition.NumberNotEqualTo, "6");
    assert(result);
    assertEquals(
        validateConditionally("5", Condition.NumberNotEqualTo, "6"),
        result,
    );
});

Deno.test("Condition.NumberGreaterThan", (): void => {
    const result = validateNumber("5", Condition.NumberGreaterThan, "4");
    assert(result);
    assertEquals(
        validateConditionally("5", Condition.NumberGreaterThan, "4"),
        result,
    );
});

Deno.test("Condition.NumberLessThan", (): void => {
    const result = validateNumber("5", Condition.NumberLessThan, "6");
    assert(result);
    assertEquals(
        validateConditionally("5", Condition.NumberLessThan, "6"),
        result,
    );
});

Deno.test("Condition.NumberGreaterThanOrEqualTo", (): void => {
    const result = validateNumber("5", Condition.NumberGreaterThanOrEqualTo, "5");
    assert(result);
    assertEquals(
        validateConditionally("5", Condition.NumberGreaterThanOrEqualTo, "5"),
        result,
    );
});

Deno.test("Condition.NumberLessThanOrEqualTo", (): void => {
    const result = validateNumber("5", Condition.NumberLessThanOrEqualTo, "5");
    assert(result);
    assertEquals(
        validateConditionally("5", Condition.NumberLessThanOrEqualTo, "5"),
        result,
    );
});

// Text conditions
Deno.test("Condition.TextIsExactly", (): void => {
    const result = validateText("deno", Condition.TextIsExactly, "deno");
    assert(result);
    assertEquals(
        validateConditionally("deno", Condition.TextIsExactly, "deno"),
        result,
    );
});

Deno.test("Condition.TextNotExactly", (): void => {
    const result = validateText("deno", Condition.TextNotExactly, "node");
    assert(result);
    assertEquals(
        validateConditionally("deno", Condition.TextNotExactly, "node"),
        result,
    );
});

Deno.test("Condition.TextContains", (): void => {
    const result = validateText("hello world", Condition.TextContains, "world");
    assert(result);
    assertEquals(
        validateConditionally("hello world", Condition.TextContains, "world"),
        result,
    );
});

Deno.test("Condition.TextDoesntContains", (): void => {
    const result = validateText("hello world", Condition.TextDoesntContains, "moon");
    assert(result);
    assertEquals(
        validateConditionally("hello world", Condition.TextDoesntContains, "moon"),
        result,
    );
});

Deno.test("Condition.TextBeginsWith", (): void => {
    const result = validateText("hello world", Condition.TextBeginsWith, "hello");
    assert(result);
    assertEquals(
        validateConditionally("hello world", Condition.TextBeginsWith, "hello"),
        result,
    );
});

Deno.test("Condition.TextEndsWith", (): void => {
    const result = validateText("hello world", Condition.TextEndsWith, "world");
    assert(result);
    assertEquals(
        validateConditionally("hello world", Condition.TextEndsWith, "world"),
        result,
    );
});

Deno.test("Condition.TextIsBlank", (): void => {
    const result = validateText("   ", Condition.TextIsBlank, "ignored");
    assert(result);
    assertEquals(
        validateConditionally("   ", Condition.TextIsBlank, "ignored"),
        result,
    );
});

Deno.test("Condition.TextNotBlank", (): void => {
    const result = validateText("hello", Condition.TextNotBlank, "ignored");
    assert(result);
    assertEquals(
        validateConditionally("hello", Condition.TextNotBlank, "ignored"),
        result,
    );
});

// Date conditions
Deno.test("Condition.DateYesterday", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-05-14", Condition.DateYesterday);
        assert(result);
        assertEquals(
            validateConditionally("2024-05-14", Condition.DateYesterday, ""),
            result,
        );
    });
});

Deno.test("Condition.DateToday", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-05-15", Condition.DateToday);
        assert(result);
        assertEquals(
            validateConditionally("2024-05-15", Condition.DateToday, ""),
            result,
        );
    });
});

Deno.test("Condition.DateTomorrow", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-05-16", Condition.DateTomorrow);
        assert(result);
        assertEquals(
            validateConditionally("2024-05-16", Condition.DateTomorrow, ""),
            result,
        );
    });
});

Deno.test("Condition.DateInTheLast7Days", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-05-10", Condition.DateInTheLast7Days);
        assert(result);
        assertEquals(
            validateConditionally("2024-05-10", Condition.DateInTheLast7Days, ""),
            result,
        );
    });
});

Deno.test("Condition.DateLastWeek", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-05-06", Condition.DateLastWeek);
        assert(result);
        assertEquals(
            validateConditionally("2024-05-06", Condition.DateLastWeek, ""),
            result,
        );
    });
});

Deno.test("Condition.DateThisWeek", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-05-13", Condition.DateThisWeek);
        assert(result);
        assertEquals(
            validateConditionally("2024-05-13", Condition.DateThisWeek, ""),
            result,
        );
    });
});

Deno.test("Condition.DateNextWeek", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-05-20", Condition.DateNextWeek);
        assert(result);
        assertEquals(
            validateConditionally("2024-05-20", Condition.DateNextWeek, ""),
            result,
        );
    });
});

Deno.test("Condition.DateLastMonth", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-04-15", Condition.DateLastMonth);
        assert(result);
        assertEquals(
            validateConditionally("2024-04-15", Condition.DateLastMonth, ""),
            result,
        );
    });
});

Deno.test("Condition.DateThisMonth", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-05-10", Condition.DateThisMonth);
        assert(result);
        assertEquals(
            validateConditionally("2024-05-10", Condition.DateThisMonth, ""),
            result,
        );
    });
});

Deno.test("Condition.DateNextMonth", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-06-10", Condition.DateNextMonth);
        assert(result);
        assertEquals(
            validateConditionally("2024-06-10", Condition.DateNextMonth, ""),
            result,
        );
    });
});

Deno.test("Condition.DateLastYear", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2023-07-04", Condition.DateLastYear);
        assert(result);
        assertEquals(
            validateConditionally("2023-07-04", Condition.DateLastYear, ""),
            result,
        );
    });
});

Deno.test("Condition.DateThisYear", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2024-09-25", Condition.DateThisYear);
        assert(result);
        assertEquals(
            validateConditionally("2024-09-25", Condition.DateThisYear, ""),
            result,
        );
    });
});

Deno.test("Condition.DateNextYear", (): void => {
    withBaseDate((): void => {
        const result = validateDate("2025-03-30", Condition.DateNextYear);
        assert(result);
        assertEquals(
            validateConditionally("2025-03-30", Condition.DateNextYear, ""),
            result,
        );
    });
});

describe("Date", () => {
    it("Weekday test", () => {
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
        const offset = currentDay - firstDay; // Days since Monday.
        const currentWeekStart = new Date(today);
        currentWeekStart.setDate(today.getDate() - offset);
        const lastWeekStart = new Date(currentWeekStart);
        lastWeekStart.setDate(currentWeekStart.getDate() - 7);
        const lastWeekEnd = new Date(lastWeekStart);
        lastWeekEnd.setDate(lastWeekStart.getDate() + 6);
        //return dateValue >= lastWeekStart && dateValue <= lastWeekEnd;

        //assert(year.value === 1447);
        //assert(globalThis.NumberPrimitiveReaction === "Value changed → 1447");

        //assert();
    });
});
