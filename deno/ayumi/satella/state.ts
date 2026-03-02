export default class State<T> {
    private _value: T;
    private _listeners: Array<(newValue: T) => void> = [];

    constructor(initial: T) {
        this._value = initial;
    }

    onChange(listener: (newValue: T) => void): void {
        this._listeners.push(listener);
    }

    private notifyChange(newValue: T): void {
        this._listeners.forEach(listener => {
            listener(newValue);
        });
    }

    get value(): T {
        return this._value;
    }

    set value(newValue: T) {
        const oldValue = this._value;
        if (oldValue !== newValue) {
            this._value = newValue;
            this.notifyChange(newValue);
        }
    }
}
