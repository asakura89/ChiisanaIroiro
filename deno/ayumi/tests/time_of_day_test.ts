import { assertEquals } from "@std/assert";
import { TimeOfDay } from "../itsu/time_of_day.ts";
import { Duration } from "../itsu/duration.ts";

Deno.test("fromMinutes wraps around 24h and handles negative values", () => {
    assertEquals(TimeOfDay.fromMinutes(0).toString(), "0:00");
    assertEquals(TimeOfDay.fromMinutes(1439).toString(), "23:59");
    assertEquals(TimeOfDay.fromMinutes(1440).toString(), "0:00");
    assertEquals(TimeOfDay.fromMinutes(-1).toString(), "23:59");
    assertEquals(TimeOfDay.fromMinutes(-1440).toString(), "0:00");
});

Deno.test("addDuration adds minutes correctly and wraps around", () => {
    const t = new TimeOfDay(23, 50);
    const d = new Duration(15);
    assertEquals(t.addDuration(d).toString(), "0:05");
});

Deno.test("subtractDuration subtracts minutes correctly and wraps around", () => {
    const t = new TimeOfDay(0, 10);
    const d = new Duration(15);
    assertEquals(t.subtractDuration(d).toString(), "23:55");
});

Deno.test("diff returns correct Duration (positive and negative)", () => {
    const t1 = new TimeOfDay(10, 30);
    const t2 = new TimeOfDay(9, 45);
    assertEquals(t1.diff(t2).minutes, 45);
    assertEquals(t2.diff(t1).minutes, -45);
});