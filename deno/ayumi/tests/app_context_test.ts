import { AppContext } from "../app/app_context.ts";
import { assertExists, assertStrictEquals } from "@std/assert";

Deno.test("AppContext is a singleton", (): void => {
    const ctx1: AppContext = AppContext.getInstance();
    const ctx2: AppContext = AppContext.getInstance();
    assertStrictEquals(ctx1, ctx2);
});

Deno.test("AppContext properties are initialized", (): void => {
    const ctx: AppContext = AppContext.getInstance();
    assertExists(ctx.appInfo);
    assertExists(ctx.currentScreen);
    assertExists(ctx.data);
    assertExists(ctx.eventEmitter);
});
