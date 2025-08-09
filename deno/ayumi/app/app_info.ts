import { getType } from "../kayo-core/core.ts";

export interface AppInfo {
    name: string;
    directory: string;
    isServer: boolean;
    isBrowser: boolean;
}

export function getAppInfo(name?: string): AppInfo {
    try {
        const info: AppInfo = {
            name: name ? name : "",
            directory: Deno.cwd(),
            isServer: typeof window === "undefined" || getType(window) === "Undefined",
            isBrowser: typeof Deno === "undefined" || getType(Deno) === "Undefined"
        };

        if (!name) {
            const appName = Deno.mainModule.split("/").pop() || "";
            info.name = appName ? appName.split(".")[0] : "";
        }

        return info;
    }
    catch {
        return {
            name: "",
            directory: "",
            isServer: false,
            isBrowser: false
        };
    }
}