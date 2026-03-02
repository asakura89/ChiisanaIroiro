import { ensureFile } from "@std/fs";
import { join } from "@std/path";
import { getAppInfo } from "./app_info.ts";

export async function log(message: string, isStarting = false, writeToScreen = true): Promise<void> {
    const scriptInfo = getAppInfo();

    if (scriptInfo.name && scriptInfo.directory) {
        const now = new Date().toISOString();
        const timestamp = now
            .replace(/[\.Z]/gm, "")
            .replace(/\-/gm, ".")
            .replace(/T/gm, ".")
            .slice(0, 19);
        const logName = `${scriptInfo.name}_${now.replace(/[\.\-:TZ]/gm, "").slice(0, 14)}.log`;
        const logFile = join(scriptInfo.directory, logName);
        const logMessage = `[${timestamp}] ${message}`;

        if (writeToScreen) {
            console.log(logMessage);
        }

        await ensureFile(logFile);

        const encoder = new TextEncoder();
        const logData = new Uint8Array(encoder.encode(`${logMessage}\n`));

        if (isStarting) {
            await Deno.writeFile(logFile, logData);
        }
        else {
            await Deno.writeFile(logFile, logData, { append: true });
        }
    }
}


