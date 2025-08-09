import State from "../satella/state.ts";
import { assertEquals } from "jsr:@std/assert";

Deno.test("State listener called only on value change", (): void => {
    const state = new State<number>(0);
    let observed: number | undefined;

    state.onChange((value) => {
        observed = value;
    });

    state.value = 1;
    assertEquals(observed, 1, "listener should be called when value changes");

    observed = undefined;
    state.value = 1;
    assertEquals(observed, undefined, "listener should not be called when value is unchanged");

    let observed2: string | undefined;
    const reaction = (value: number): void => {
        observed2 = `Value changed → ${value}`;
    };

    const year = new State<number>(1444);
    year.onChange(reaction);
    year.value += 3;

    assertEquals(year.value, 1447);
    assertEquals(observed2, "Value changed → 1447");
});
