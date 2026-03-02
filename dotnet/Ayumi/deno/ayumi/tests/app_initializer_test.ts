/// <reference path="../../types/global.d.ts" />

import { init } from "../app/app_initializer.ts";
import { AppContext } from "../app/app_context.ts";
import { assert, assertInstanceOf } from "@std/assert";
import { spy, assertSpyCalls } from "@std/testing/mock";

Deno.test("init sets Context and executes callbacks", (): void => {
    // prepare spies
    const cb1 = spy();
    const cb2 = spy();

    // remove any existing context for isolation
    // deno-lint-ignore no-explicit-any
    (globalThis as any).Context = undefined;

    init([cb1, cb2]);

    assert(globalThis.Context, "Context should be defined");
    assertInstanceOf(globalThis.Context, AppContext);

    assertSpyCalls(cb1, 1);
    assertSpyCalls(cb2, 1);
});
