import { assertEquals, assertNotStrictEquals, assertThrows } from "jsr:@std/assert";
import EmitterEventArgs from "../emi/emitter_event_args.ts";
import EmitterException from "../emi/emitter_exc.ts";

Deno.test("constructing with valid arguments copies data properly", (): void => {
    const data = new Map<string, Record<string, unknown>>();
    const foo = { bar: 1 };
    const baz = { qux: "hello" };
    data.set("foo", foo);
    data.set("baz", baz);

    const args = new EmitterEventArgs("test", data);

    assertEquals(args.eventName, "test");
    assertNotStrictEquals(args.data, data);
    assertEquals(args.data.get("foo"), foo);
    assertEquals(args.data.get("baz"), baz);
    assertEquals(args.data.size, 2);
});

Deno.test("throws when event name is missing or empty", (): void => {
    const data = new Map<string, Record<string, unknown>>();
    data.set("foo", {});
    assertThrows(
        () => new EmitterEventArgs("", data),
        EmitterException,
        "Event name must be specified."
    );
});

Deno.test("throws when data is missing", (): void => {
    // deno-lint-ignore no-explicit-any
    assertThrows(
        () => new EmitterEventArgs("test", undefined as any),
        EmitterException,
        "Data must be specified."
    );
});

Deno.test("throws when data is empty", (): void => {
    const data = new Map<string, Record<string, unknown>>();
    assertThrows(
        () => new EmitterEventArgs("test", data),
        EmitterException,
        "Data must not be empty."
    );
});

