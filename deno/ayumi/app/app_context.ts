import { Emitter } from "../mod.ts";
import { getAppInfo } from "./app_info.ts";
import type { AppInfo } from "./app_info.ts";
import AppScreen from "./app_screen.ts";
import { Locales } from "../kayo-core/locales.ts";

export class AppContext {
    public appInfo: AppInfo;
    public currentScreen: AppScreen;
    public currentCulture: string;
    public data: Map<string, Record<string, unknown>>;
    public eventEmitter: Emitter;
    private static instance: AppContext;

    constructor() {
        this.appInfo = getAppInfo();
        this.currentScreen = new AppScreen();
        this.currentCulture = Locales["en_US"]; // Default culture
        this.data = new Map();
        this.eventEmitter = new Emitter();
    }
    
    public static getInstance(): AppContext {
        if (!this.instance)
            this.instance = new AppContext();

        return this.instance;
    }
}
