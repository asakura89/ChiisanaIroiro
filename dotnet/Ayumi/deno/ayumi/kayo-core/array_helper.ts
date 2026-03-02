// import { isNullOrEmpty } from "./string_helper.ts";

export function maxOfNumbers(arr: number[]): number {
    return arr.reduce((p, n) => {
        return Math.max(p, n);
    });
}

export function minOfNumbers(arr: number[]): number {
    return arr.reduce((p, n) => {
        return Math.min(p, n);
    });
}

export function isNullOrEmpty<T>(arr: ArrayLike<T> | null | undefined): boolean {
    return !arr ||
        arr === null ||
        arr.length === 0;
}

/* export function maxOfItems<T extends Record<string, unknown>>(arr: T[], prop: keyof T): T {
    if (!isNullOrEmpty(prop as string)) {
        return arr.reduce((prev, curr) => {
            return [prev, curr].filter(item =>
                item[prop] === Math.max(prev[prop], curr[prop]))[0];
        });
    }
}

export function minOfItems<T extends Record<string, unknown>>(arr: T[], prop: keyof T): T {
    if (!isNullOrEmpty(prop as string)) {
        return arr.reduce((prev, curr) => {
            return [prev, curr].filter(item =>
                item[prop] === Math.min(prev[prop], curr[prop]))[0];
        });
    }
} */
