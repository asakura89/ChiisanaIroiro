function clampInt(n: number, min: number, max: number) {
    return Math.max(min, Math.min(max, Math.floor(n)));
}

export function readText(form: FormData, name: string): string | null {
    const value = form.get(name);
    if (value === null)
        return null;

    if (typeof value === "string")
        return value;

    return null;
}

export function readFloat(form: FormData, name: string, opts: { min?: number; max?: number; default?: number } = {}): number | null {
    const raw = readText(form, name);
    if (raw === null || raw.trim() === "")
        return opts.default ?? null;

    const float = Number(raw);
    if (Number.isNaN(float))
        return opts.default ?? null;

    if (opts.min !== undefined && float < opts.min)
        return opts.default ?? null;

    if (opts.max !== undefined && float > opts.max)
        return opts.default ?? null;

    return float;
}

export function readInteger(form: FormData, name: string, opts: { min?: number; max?: number; default?: number } = {}): number | null {
    const float = readFloat(form, name);
    if (float === null)
        return opts.default ?? null;

    return Math.floor(float);
}

export function readBoolean(form: FormData, name: string, opts: { default?: boolean } = {}): boolean {
    const raw = readText(form, name);
    if (raw === null)
        return opts.default ?? false;

    const selection = raw.toLowerCase();
    if (["1", "true", "on", "yes"].includes(selection))
        return true;

    if (["0", "false", "off", "no"].includes(selection))
        return false;

    return true;
}