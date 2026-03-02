function getStringPadding(width: number, pad?: string): string {
    if (pad === undefined || pad === null) {
        pad = " ";
    }

    let padded = "";
    for (let idx = 0; idx < width; idx++) {
        padded += pad;
    }

    return padded;
}

export function padLeft(str: string, width: number): string;
export function padLeft(str: string, width: number, pad: string): string;
export function padLeft(str: string, width: number, pad?: string): string {
    if (width <= 0) {
        return str;
    }

    const padWidth = width - str.length;
    if (padWidth <= 0) {
        return str;
    }

    const padded = getStringPadding(padWidth, pad);
    const resulting = padded + str;
    return resulting.substring(resulting.length - width, resulting.length);
}

export function padRight(str: string, width: number): string;
export function padRight(str: string, width: number, pad: string): string;
export function padRight(str: string, width: number, pad?: string): string {
    if (width <= 0) {
        return str;
    }

    const padWidth = width - str.length;
    if (padWidth <= 0) {
        return str;
    }

    const padded = getStringPadding(padWidth, pad);
    return (str + padded).substring(0, width);
}

export function isNullOrEmpty(str: string | null | undefined): boolean {
    return !str ||
        str === null ||
        str.trim() === "";
}
