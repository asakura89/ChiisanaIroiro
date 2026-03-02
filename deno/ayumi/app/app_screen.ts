export default class AppScreen {
    name: string;
    data: Map<string, Record<string, unknown>>;

    constructor() {
        this.name = "";
        this.data = new Map();
    }
}