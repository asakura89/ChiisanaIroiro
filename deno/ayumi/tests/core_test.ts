import { asArray, extend, getType, join, merge } from "../kayo-core/core.ts";
import { assert, assertEquals } from "jsr:@std/assert";

Deno.test("getType works for various values", (): void => {
    assertEquals(getType(undefined), "Undefined");
    assertEquals(getType(null), "Null");
    assertEquals(getType(123), "Number");
    assertEquals(getType([1, 2, 3]), "Array");
    assertEquals(getType({ foo: "bar" }), "Object");
    assertEquals(getType(new Date()), "Date");
});

Deno.test("asArray converts array-like objects", (): void => {
    const arrayLike = { 0: "a", 1: "b", length: 2 };
    const arr = asArray(arrayLike);
    assertEquals(arr, ["a", "b"]);
});

Deno.test("join joins with separators", (): void => {
    assertEquals(join([1, 2, 3], ","), "1,2,3");
    assertEquals(join(["a", "b", "c"], "-"), "a-b-c");
    assertEquals(join(["x", "y"], ""), "xy");
});

Deno.test("extend copies property descriptors", (): void => {
    const target: Record<string, unknown> = {};
    const source: Record<string, unknown> = {};
    // property is enumerable so extend iterates over it
    Object.defineProperty(source, "hidden", {
        value: 42,
        enumerable: true,
        writable: false,
    });
    extend(target, source);
    const desc = Object.getOwnPropertyDescriptor(target, "hidden");
    assert(desc != null);
    assertEquals(desc!.enumerable, true);
    assertEquals(desc!.writable, false);
    assertEquals((target as Record<string, number>).hidden, 42);
});

Deno.test("merge combines properties without altering sources", (): void => {
    const a: Record<string, unknown> = {};
    const b: Record<string, unknown> = {};
    Object.defineProperty(a, "foo", {
        value: 1,
        enumerable: true,
        writable: false,
    });
    Object.defineProperty(b, "bar", {
        value: 2,
        enumerable: true,
        configurable: false,
    });

    const result = merge(a, b) as Record<string, unknown>;

    assertEquals(Object.getOwnPropertyDescriptor(a, "bar"), undefined);
    assertEquals(Object.getOwnPropertyDescriptor(b, "foo"), undefined);

    assertEquals((result as Record<string, number>).foo, 1);
    const fooDesc = Object.getOwnPropertyDescriptor(result, "foo");
    assert(fooDesc != null);
    assertEquals(fooDesc!.writable, false);
    const barDesc = Object.getOwnPropertyDescriptor(result, "bar");
    assert(barDesc != null);
    assertEquals(barDesc!.enumerable, true);
    assertEquals(barDesc!.configurable, false);
    assertEquals((result as Record<string, number>).bar, 2);
});

