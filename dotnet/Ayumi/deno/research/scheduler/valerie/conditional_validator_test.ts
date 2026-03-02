/// <reference path="../../types/global.d.ts" />

import { assert } from "@std/assert";
import { describe, it } from "@std/testing/bdd";

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