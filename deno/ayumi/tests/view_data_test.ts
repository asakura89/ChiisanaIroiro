import ViewData from "../kayo-core/view_data.ts";
import { assertEquals, assertArrayIncludes } from "jsr:@std/assert";

Deno.test("add inserts value retrievable by get", (): void => {
    const vd = new ViewData();
    vd.add("foo", 123);
    assertEquals(vd.get("foo"), 123);
});

Deno.test("remove deletes an entry", (): void => {
    const vd = new ViewData();
    vd.add("bar", "baz");
    vd.remove("bar");
    assertEquals(vd.get("bar"), undefined);
});

Deno.test("clear empties all entries", (): void => {
    const vd = new ViewData();
    vd.add("a", 1);
    vd.add("b", 2);
    vd.clear();
    assertEquals(vd.getKeys().length, 0);
});

Deno.test("getKeys returns all inserted keys", (): void => {
    const vd = new ViewData();
    vd.add("x", 1);
    vd.add("y", 2);
    assertArrayIncludes(vd.getKeys().sort(), ["x", "y"]);
});
