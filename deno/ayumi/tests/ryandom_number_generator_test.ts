import { RyandomNumberGenerator } from "../ryang/ryandom_number_generator.ts";

function assertInRange(value: number, min: number, max: number): void {
    if (value < min || value > max) {
        throw new Error(`Value ${value} not within [${min}, ${max}]`);
    }
}

Deno.test("ryandomize and ryandomizeSingle return numbers within bounds", (): void => {
    const original = crypto.getRandomValues.bind(crypto);
    const predetermined = [10, -8];
    let index = 0;
    crypto.getRandomValues = ((arr: Int32Array): Int32Array => {
        arr[0] = predetermined[index++];
        return arr;
    }) as typeof crypto.getRandomValues;

    const val1 = RyandomNumberGenerator.ryandomize(0, 5);
    assertInRange(val1, 0, 5);

    const val2 = RyandomNumberGenerator.ryandomizeSingle(3);
    assertInRange(val2, 0, 3);

    crypto.getRandomValues = original;
});
