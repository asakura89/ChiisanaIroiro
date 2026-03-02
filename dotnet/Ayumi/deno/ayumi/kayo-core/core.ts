export function getType(obj: unknown): string {
    if (typeof obj === "undefined")
        return "Undefined";

    if (obj === undefined)
        return "Undefined";

    if (obj === null)
        return "Null";

    const match = Object
        .prototype
        .toString
        .call(obj)
        .match(/^\[object\s+(.*?)\]$/);

    if (!match)
        return "Unknown";

    return match[1];
}

export function asArray<T>(arrayLike: ArrayLike<T>): T[] {
    return Array
        .prototype
        .slice
        .call(arrayLike);
}

export function join(array: ArrayLike<unknown>, separator: string): string {
    return Array
        .prototype
        .join
        .call(array, separator);
}

export function extend(target: Record<string, unknown>, source: object): object {
    for (const prop in source) {
        const propInfo = Object.getOwnPropertyDescriptor(source, prop);
        if (propInfo) {
            delete target[prop];
            Object.defineProperty(target, prop, propInfo);
        }
    }

    return target;
}

export function merge(sourceA: object, sourceB: object): object {
    const target: Record<string, unknown> = {};
    for (const prop in sourceA) {
        const propInfo = Object.getOwnPropertyDescriptor(sourceA, prop);
        if (propInfo) {
            delete target[prop];
            Object.defineProperty(target, prop, propInfo);
        }
    }

    for (const prop in sourceB) {
        const propInfo = Object.getOwnPropertyDescriptor(sourceB, prop);
        if (propInfo) {
            delete target[prop];
            Object.defineProperty(target, prop, propInfo);
        }
    }

    return target;
}

