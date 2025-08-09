import { assertEquals } from "jsr:@std/assert";
import { maxOfNumbers, minOfNumbers, isNullOrEmpty } from "../kayo-core/array_helper.ts";

Deno.test("maxOfNumbers positive numbers", (): void => {
    const arr: number[] = [1, 2, 3, 4, 5];
    assertEquals(maxOfNumbers(arr), 5);
});

Deno.test("maxOfNumbers negative numbers", (): void => {
    const arr: number[] = [-10, -3, -7, -1];
    assertEquals(maxOfNumbers(arr), -1);
});

Deno.test("maxOfNumbers mixed numbers", (): void => {
    const arr: number[] = [-10, 0, 15, -7, 8];
    assertEquals(maxOfNumbers(arr), 15);
});

Deno.test("minOfNumbers positive numbers", (): void => {
    const arr: number[] = [1, 2, 3, 4, 5];
    assertEquals(minOfNumbers(arr), 1);
});

Deno.test("minOfNumbers negative numbers", (): void => {
    const arr: number[] = [-10, -3, -7, -1];
    assertEquals(minOfNumbers(arr), -10);
});

Deno.test("minOfNumbers mixed numbers", (): void => {
    const arr: number[] = [-10, 0, 15, -7, 8];
    assertEquals(minOfNumbers(arr), -10);
});

Deno.test("isNullOrEmpty null", (): void => {
    const arr: number[] | null = null;
    assertEquals(isNullOrEmpty(arr), true);
});

Deno.test("isNullOrEmpty undefined", (): void => {
    const arr: number[] | undefined = undefined;
    assertEquals(isNullOrEmpty(arr), true);
});

Deno.test("isNullOrEmpty empty array", (): void => {
    const arr: number[] = [];
    assertEquals(isNullOrEmpty(arr), true);
});

Deno.test("isNullOrEmpty non empty array", (): void => {
    const arr: number[] = [1];
    assertEquals(isNullOrEmpty(arr), false);
});
