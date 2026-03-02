import EmitterException from "./emitter_exc.ts";

export default class EmitterEventArgs {
    eventName: string;
    target: unknown;
    // deno-lint-ignore no-explicit-any
    data: Map<string, Record<string, any>> = new Map();

    // deno-lint-ignore no-explicit-any
    constructor(eventName: string, data: Map<string, Record<string, any>>) {
        if (!eventName)
            throw new EmitterException("Event name must be specified.");

        this.eventName = eventName;

        if (!data)
            throw new EmitterException("Data must be specified.");

        if (data.size === 0)
            throw new EmitterException("Data must not be empty.");

        const dataKeys: string[] = Array.from(data.keys());
        for (const key of dataKeys) {
            const value = data.get(key);
            this.data.set(key, value!);
        }
    }
}