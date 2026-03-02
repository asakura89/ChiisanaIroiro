import { assertEquals } from "jsr:@std/assert";
import { getType } from "../kayo-core/core.ts";
import { getAppInfo } from "../app/app_info.ts";

function stubDeno(path: string, module: string) {
    const originalCwd = Deno.cwd;
    const originalMainModule = Deno.mainModule;
    Object.defineProperty(Deno, "cwd", { value: () => path, configurable: true });
    Object.defineProperty(Deno, "mainModule", { value: module, configurable: true });
    return (): void => {
        Object.defineProperty(Deno, "cwd", { value: originalCwd, configurable: true });
        Object.defineProperty(Deno, "mainModule", { value: originalMainModule, configurable: true });
    };
}

Deno.test("getAppInfo returns correct info", (): void => {
    const restore = stubDeno("/tmp/project", "file:///tmp/project/main.ts");
    try {
        const info = getAppInfo();
        const expectedIsServer = typeof window === "undefined" || getType(window) === "Undefined";
        const expectedIsBrowser = typeof Deno === "undefined" || getType(Deno) === "Undefined";

        assertEquals(info.name, "main");
        assertEquals(info.directory, "/tmp/project");
        assertEquals(info.isServer, expectedIsServer);
        assertEquals(info.isBrowser, expectedIsBrowser);
    } finally {
        restore();
    }
});
