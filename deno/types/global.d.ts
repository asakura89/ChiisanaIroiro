/// <reference lib="es6" />
import type { AppContext } from "../ayumi/app/app_context.ts";

declare global {
    namespace globalThis {
        var Context: AppContext;
    }
}

interface ErrorConstructor {
    captureStackTrace(error: object, constructor?: Function): void;
}

declare type Action<T> = (param: T) => void;
