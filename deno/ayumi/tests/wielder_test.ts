import { Wielder, AlphaType } from "../keywielder/wielder.ts";
import { assertEquals } from "jsr:@std/assert";

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
    const numericMonth = longMonth;
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
