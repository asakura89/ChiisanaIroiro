/// <reference lib="es6" />
import type { AppContext } from "./ayumi/app/app_context.ts";

declare global {
    namespace globalThis {
        // deno-lint-ignore no-var
        var Context: AppContext;
    }
}

interface ErrorConstructor {
    // deno-lint-ignore ban-types
    captureStackTrace(error: object, constructor?: Function): void;
}

declare type Action<T> = (param: T) => void;

