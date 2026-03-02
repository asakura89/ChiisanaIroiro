export default class ViewData {
    private data: Record<string, unknown>;

    constructor() {
        this.data = {};
    }

    get(key: string): unknown {
        //return this.data.hasOwnProperty(key) ? this.data[key] : undefined;
        return Object.hasOwn(this.data, key) ? this.data[key] : undefined;
    }

    add(key: string, value: unknown): void {
        this.data[key] = value;
    }

    remove(key: string): void {
        delete this.data[key];
    }

    clear(): void {
        this.data = {};
    }

    getKeys(): string[] {
        return Object.keys(this.data);
    }
}