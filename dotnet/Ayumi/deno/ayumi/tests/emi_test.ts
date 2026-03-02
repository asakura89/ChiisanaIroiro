import { assertEquals, assertThrows } from "@std/assert";
import { Emitter, EmitterEventArgs } from "../mod.ts";

function createArgs(name: string): EmitterEventArgs {
    const data = new Map<string, Record<string, unknown>>();
    data.set("v", { ok: true });
    return new EmitterEventArgs(name, data);
}

Deno.test("on/emit passes argument to callback", (): void => {
    const emitter = new Emitter();
    let received: EmitterEventArgs | null = null;
    emitter.on("test", arg => {
        received = arg;
    });
    const args = createArgs("test");
    emitter.emit("test", args);
    assertEquals(received, args);
});

Deno.test("off removes callbacks", (): void => {
    const emitter = new Emitter();
    let count = 0;
    const cb1 = (): void => {
        count += 1;
    };
    const cb2 = (): void => {
        count += 2;
    };
    emitter.on("evt", cb1).on("evt", cb2);
    emitter.off("evt", cb1);
    emitter.emit("evt", createArgs("evt"));
    assertEquals(count, 2);

    count = 0;
    emitter.off("evt");
    emitter.emit("evt", createArgs("evt"));
    assertEquals(count, 0);
});

Deno.test("once fires only once", (): void => {
    const emitter = new Emitter();
    let count = 0;
    emitter.once("o", () => count++);
    const args = createArgs("o");
    emitter.emit("o", args);
    emitter.emit("o", args);
    assertEquals(count, 1);
});

Deno.test("error when name or callback missing", (): void => {
    const emitter = new Emitter();
    assertThrows(() => emitter.on("", (): void => {}));
    // deno-lint-ignore no-explicit-any
    assertThrows(() => emitter.on("evt", undefined as any));
    assertThrows(() => emitter.off(""));
    assertThrows(() => emitter.once("", (): void => {}));
    // deno-lint-ignore no-explicit-any
    assertThrows(() => emitter.once("evt", undefined as any));
    const args = createArgs("e");
    assertThrows(() => emitter.emit("", args));
});
