/// <reference no-default-lib="true" />
/// <reference lib="dom" />
/// <reference lib="dom.iterable" />
/// <reference lib="dom.asynciterable" />
/// <reference lib="deno.ns" />

import "$std/dotenv/load.ts";

import { start } from "$fresh/server.ts";
import manifest from "./fresh.gen.ts";
import config from "./fresh.config.ts";

import { init } from "./ayumi/app/app_initializer.ts";
import APP_CONSTANTS from "./app/app_constants.ts";

init([
    function setAppInfo() {
        globalThis.Context.appInfo.name = APP_CONSTANTS.APP_NAME;
        globalThis.Context.data.set("AdditionalAppInfo", APP_CONSTANTS);
    }
]);

await start(manifest, config);
