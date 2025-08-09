import { isNullOrEmpty, padLeft, padRight } from "../kayo-core/string_helper.ts";
import { assert, assertEquals } from "jsr:@std/assert";

Deno.test("padLeft with default padding", (): void => {
    const result = padLeft("abc", 5);
    assertEquals(result, "  abc");
});

Deno.test("padLeft with custom padding", (): void => {
    const result = padLeft("abc", 5, "0");
    assertEquals(result, "00abc");
});

Deno.test("padLeft does not truncates when width is less than string length", (): void => {
    const result = padLeft("abcdef", 3);
    assertEquals(result, "abcdef");
});

Deno.test("padRight with default padding", (): void => {
    const result = padRight("abc", 5);
    assertEquals(result, "abc  ");
});

Deno.test("padRight with custom padding", (): void => {
    const result = padRight("abc", 5, "0");
    assertEquals(result, "abc00");
});

Deno.test("padRight does not truncates when width is less than string length", (): void => {
    const result = padRight("abcdef", 3);
    assertEquals(result, "abcdef");
});

Deno.test("isNullOrEmpty cases", (): void => {
    assert(isNullOrEmpty(""));
    assert(isNullOrEmpty("   "));
    assert(isNullOrEmpty(null));
    assertEquals(isNullOrEmpty("text"), false);
});
