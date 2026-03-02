/// <reference path="../../types/global.d.ts" />

import { AppContext } from "./app_context.ts";

export function init(customInit?: (() => void)[]) {
    globalThis.Context = AppContext.getInstance();

    if (customInit && customInit.length > 0)
        for (let idx = 0; idx < customInit.length; idx++)
            customInit[idx]();
}