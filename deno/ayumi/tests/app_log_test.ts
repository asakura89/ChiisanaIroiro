import { assert } from "@std/assert";
import { describe, it } from "@std/testing/bdd";
import { getAppInfo } from "../app/app_info.ts";
import { log } from "../app/app_log.ts";
import { join } from "@std/path/join";

describe("Log", () => {
    it("Should satisfy custom checking", () => {
        const scriptInfo = getAppInfo();
        const srlzdScriptInfo: string = JSON.stringify(scriptInfo);
        console.log(srlzdScriptInfo);

        log(":: Start Script ::", true);
        log(srlzdScriptInfo);
        log("Doing something...");
        log("Doing another thing...");
        log(".:: Finish Script ::.");

        /* const logFile = join(
            scriptInfo.directory,
            `${scriptInfo.name}_${new Date().toISOString().replace(/[\.\-:TZ]/gm, "").slice(0, 14)}.log`, "");
        
        const fileInfo = Deno.statSync(logFile);
        assert(fileInfo.isFile); */
    });
});