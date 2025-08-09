import type EmitterEventArgs from "./emitter_event_args.ts";
import EmitterException from "./emitter_exc.ts";
import { isNullOrEmpty } from "../kayo-core/string_helper.ts";
import type { Action } from "../kayo-core/action.ts";

export class Emitter {
    e: Map<string, Array<Action<EmitterEventArgs>>> = new Map();

    get count(): number {
        return this.e.size;
    }

    on(name: string, callback: Action<EmitterEventArgs>): Emitter {
        if (isNullOrEmpty(name))
            throw new EmitterException("Name must be specified.");

        if (!callback)
            throw new EmitterException("Invalid callback.");

        if (!this.e.has(name))
            this.e.set(name, []);

        this.e.get(name)!.push(callback);

        return this;
    }

    off(name: string): Emitter;
    off(name: string, callback: Action<EmitterEventArgs> | null): Emitter;
    off(name: string, callback?: Action<EmitterEventArgs> | null): Emitter {
        if (isNullOrEmpty(name))
            throw new EmitterException("Name must be specified.");

        if (!this.e.has(name))
            return this;

        if (!callback || callback === null) {
            this.e.delete(name);
            return this;
        }

        const callbacks = this.e.get(name);
        const liveCallbacks = callbacks!.filter(item => item !== callback);
        if (liveCallbacks.length > 0)
            this.e.set(name, liveCallbacks);
        else
            this.e.delete(name);

        return this;
    }

    once(name: string, callback: Action<EmitterEventArgs>): Emitter {
        if (isNullOrEmpty(name))
            throw new EmitterException("Name must be specified.");

        if (!callback)
            throw new EmitterException("Invalid callback.");

        // deno-lint-ignore no-this-alias
        const self = this;
        let wrapper: Action<EmitterEventArgs> | null = null;
        wrapper = arg => {
            self.off(name, wrapper);
            callback(arg);
        };

        return self.on(name, wrapper);
    }

    emit(name: string, arg: EmitterEventArgs): Emitter {
        if (isNullOrEmpty(name))
            throw new EmitterException("Name must be specified.");

        if (!this.e.has(name))
            return this;

        const callbacks = this.e.get(name);
        for (const callback of callbacks!)
            callback(arg);

        return this;
    }
}