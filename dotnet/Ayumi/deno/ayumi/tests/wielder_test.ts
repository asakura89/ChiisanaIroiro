import { Wielder, AlphaType } from "../keywielder/wielder.ts";
import { assertEquals, assertMatch, assert } from "jsr:@std/assert";

Deno.test("Wielder.addRandomString generates correct length and type", () => {
    const wielder = Wielder.new().addRandomString(10, AlphaType.UpperLowerNumericSymbol);
    const key = wielder.buildKey();
    assertEquals(key.length, 10);
});

Deno.test("Wielder.addRandomNumber generates only numbers", () => {
    const wielder = Wielder.new().addRandomNumber(8);
    const key = wielder.buildKey();
    assertMatch(key, /^[0-9]{8}$/);
});

Deno.test("Wielder.addRandomAlphaNumeric uppercase", () => {
    const wielder = Wielder.new().addRandomAlphaNumeric(5, true);
    const key = wielder.buildKey();
    assertMatch(key, /^[A-Z]{5}$/);
});

Deno.test("Wielder.addRandomAlphaNumeric lowercase", () => {
    const wielder = Wielder.new().addRandomAlphaNumeric(5, false);
    const key = wielder.buildKey();
    assertMatch(key, /^[a-z]{5}$/);
});

Deno.test("Wielder.addGuidString appends guid without dashes", () => {
    const wielder = Wielder.new().addGuidString();
    const key = wielder.buildKey();
    assertEquals(key.length, 32);
    assertMatch(key, /^[a-f0-9]{32}$/i);
});

Deno.test("Wielder.addString truncates and uppercases", () => {
    const wielder = Wielder.new().addString("abcde", 3);
    const key = wielder.buildKey();
    assertEquals(key, "ABC");
});

Deno.test("Wielder.addRightPadded pads and truncates right", () => {
    const wielder = Wielder.new().addRightPadded("abc", 5, "*");
    const key = wielder.buildKey();
    assertEquals(key, "abc**");
});

Deno.test("Wielder.addLeftPadded pads and truncates left", () => {
    const wielder = Wielder.new().addLeftPadded("abc", 5, "*");
    const key = wielder.buildKey();
    assertEquals(key, "**abc");
});

Deno.test("Wielder.addShortYear and addLongYear", () => {
    const wielder = Wielder.new().addShortYear().addLongYear();
    const key = wielder.buildKey();
    const year = new Date().getFullYear().toString();
    assertEquals(key, year.slice(-2) + year);
});

Deno.test("Wielder.addShortMonth, addLongMonth, addNumericMonth", () => {
    const wielder = Wielder.new().addShortMonth().addLongMonth().addNumericMonth();
    const now = new Date();
    const shortMonth = now.toLocaleString("default", { month: "short" });
    const longMonth = now.toLocaleString("default", { month: "long" });
    const numericMonth = (now.getMonth() + 1).toString().padStart(2, "0");
    assertEquals(wielder.buildKey(), shortMonth + longMonth + numericMonth);
});

Deno.test("Wielder.addShortday, addLongDay, addNumericDay", () => {
    const wielder = Wielder.new().addShortday().addLongDay().addNumericDay();
    const now = new Date();
    const shortDay = now.toLocaleString("default", { weekday: "short" });
    const longDay = now.toLocaleString("default", { weekday: "long" });
    const numericDay = now.getDay().toString().padStart(2, "0");
    assertEquals(wielder.buildKey(), shortDay + longDay + numericDay);
});

Deno.test("Wielder.addDate pads day", () => {
    const wielder = Wielder.new().addDate();
    const day = new Date().getDate().toString().padStart(2, "0");
    assertEquals(wielder.buildKey(), day);
});

Deno.test("Wielder.addCounter increments", () => {
    const wielder = Wielder.new().addCounter(5, 2);
    assertEquals(wielder.buildKey(), "7");
});

Deno.test("Wielder chaining and buildKey", () => {
    const wielder = Wielder.new()
        .addRandomString(2, AlphaType.Upper)
        .addRandomNumber(2)
        .addString("xy", 2)
        .addRightPadded("z", 2, "*")
        .addLeftPadded("q", 2, "*")
        .addShortYear()
        .addLongYear()
        .addShortMonth()
        .addLongMonth()
        .addDate()
        .addCounter(1, 1);
    const key = wielder.buildKey();
    assertEquals(typeof key, "string");
    assert(key.length > 0);
});

Deno.test("Wielder negative: addString with empty string", () => {
    const wielder = Wielder.new().addString("", 3);
    assertEquals(wielder.buildKey(), "");
});

Deno.test("Wielder negative: addRightPadded with empty string", () => {
    const wielder = Wielder.new().addRightPadded("", 3, "*");
    assertEquals(wielder.buildKey(), "***");
});

Deno.test("Wielder negative: addLeftPadded with empty string", () => {
    const wielder = Wielder.new().addLeftPadded("", 3, "*");
    assertEquals(wielder.buildKey(), "***");
});

Deno.test("Wielder negative: addCounter with negative increment", () => {
    const wielder = Wielder.new().addCounter(5, -2);
    assertEquals(wielder.buildKey(), "3");
});

Deno.test("Wielder negative: addRandomString with zero length", () => {
    const wielder = Wielder.new().addRandomString(0, AlphaType.Upper);
    assertEquals(wielder.buildKey(), "");
});

Deno.test("buildKey with various add methods", (): void => {
    const originalRandomValues = crypto.getRandomValues;
    const originalRandomUUID = crypto.randomUUID;

    const numbers = [1, 2, 3, 4, 5, 6, 7];
    let idx = 0;
    crypto.getRandomValues = ((arr: ArrayBufferView) => {
        (arr as Int32Array)[0] = numbers[idx++];
        return arr;
    }) as typeof crypto.getRandomValues;
    crypto.randomUUID = () => "11111111-2222-3333-4444-555555555555";

    const key = Wielder.new()
        .addRandomString(3, AlphaType.Upper)
        .addRandomNumber(2)
        .addRandomAlphaNumeric(2, false)
        .addGuidString()
        .addString("xy")
        .addRightPadded("z", 3, "_")
        .addLeftPadded("w", 3, "_")
        .addShortYear()
        .addLongYear()
        .addShortMonth()
        .addLongMonth()
        .addNumericMonth()
        .addDate()
        .addCounter(10)
        .buildKey();

    const upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const lower = "abcdefghijklmnopqrstuvwxyz";
    const digits = "0123456789";
    const now = new Date();
    const shortYear = now.getFullYear().toString().slice(-2);
    const longYear = now.getFullYear().toString();
    const shortMonth = now.toLocaleString("default", { month: "short" });
    const longMonth = now.toLocaleString("default", { month: "long" });
    const numericMonth = (now.getMonth() +1).toString().padStart(2, "0");
    const date = now.getDate().toString().padStart(2, "0");

    const expected =
        upper[1] + upper[2] + upper[3] +
        digits[4] + digits[5] +
        lower[6] + lower[7] +
        "11111111222233334444555555555555" +
        "XY" +
        "z__" +
        "__w" +
        shortYear +
        longYear +
        shortMonth +
        longMonth +
        numericMonth +
        date +
        "11";

    assertEquals(key, expected);

    crypto.getRandomValues = originalRandomValues;
    crypto.randomUUID = originalRandomUUID;
});


Deno.test("padding helpers", (): void => {
    const key = Wielder.new()
        .addRightPadded("a", 3, "x")
        .addLeftPadded("b", 3, "y")
        .buildKey();

    assertEquals(key, "axx" + "yyb");
});

Deno.test("generate id similar to weebcentral.com", (): void => {
    const key = Wielder.new()
        .addTimeBasedAlphaNumeric()
        .buildKey();

    console.log(key);
});

Deno.test("Wielder.addRandomHex uppercase", () => {
    const wielder = Wielder.new().addRandomHex(6, true);
    const key = wielder.buildKey();
    assertMatch(key, /^[A-F0-9]{6}$/);
});

Deno.test("Wielder.addRandomHex lowercase", () => {
    const wielder = Wielder.new().addRandomHex(6, false);
    const key = wielder.buildKey();
    assertMatch(key, /^[a-f0-9]{6}$/);
});

Deno.test("Wielder.addDateBased", () => {
    const wielder = Wielder.new().addDateBased();
    const now = new Date();
    const year = now.getFullYear().toString();
    const month = (now.getMonth() + 1).toString().padStart(2, "0");
    const day = now.getDate().toString().padStart(2, "0");
    assertEquals(wielder.buildKey(), year + month + day);
});

Deno.test("Wielder.addTimeBased", () => {
    const wielder = Wielder.new().addTimeBased();
    assertEquals(wielder.buildKey(), Date.now().toString());
});

Deno.test("Wielder.addTimeBasedAlphaNumeric uppercase", () => {
    const base36ed = Date.now().toString(36).toUpperCase();
    const wielder = Wielder.new().addTimeBasedAlphaNumeric(true);

    // NOTE: because it is a time-based, the seconds may differ
    assertEquals(wielder.buildKey().slice(-2), base36ed.slice(-2));
});

Deno.test("Wielder.addTimeBasedAlphaNumeric lowercase", () => {
    const base36ed = Date.now().toString(36).toLowerCase();
    const wielder = Wielder.new().addTimeBasedAlphaNumeric(false);

    // NOTE: because it is a time-based, the seconds may differ
    assertEquals(wielder.buildKey().slice(-2), base36ed.slice(-2));
});
