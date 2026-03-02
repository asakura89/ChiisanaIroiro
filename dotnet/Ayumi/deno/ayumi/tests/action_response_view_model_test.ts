import { ActionResponseViewModel, asActionResponseViewModelFromString, asActionResponseViewModelFromException } from "../arvy/action_response_view_model.ts";
import { assertEquals, assertThrows } from "jsr:@std/assert";

Deno.test("toString returns formatted string when alwaysReturn is true", (): void => {
    const vm = new ActionResponseViewModel(ActionResponseViewModel.Info, "hello");
    assertEquals(vm.toString(), "I|hello");
});

Deno.test("toString throws when alwaysReturn is false and type is Error", (): void => {
    const vm = new ActionResponseViewModel(ActionResponseViewModel.Error, "boom");
    assertThrows(() => vm.toString(false), Error, "boom");
});

Deno.test("asActionResponseViewModelFromString parses well formed strings", (): void => {
    const vm = asActionResponseViewModelFromString("W|warn", true);
    assertEquals(vm.responseType, ActionResponseViewModel.Warning);
    assertEquals(vm.message, "warn");
});

Deno.test("asActionResponseViewModelFromString throws on error type when alwaysReturn is false", (): void => {
    assertThrows(() => asActionResponseViewModelFromString("E|fail"), Error, "fail");
});

Deno.test("asActionResponseViewModelFromString rejects malformed strings", (): void => {
    assertThrows(() => asActionResponseViewModelFromString("A|bad", true), Error, "bad formatted");
    assertThrows(() => asActionResponseViewModelFromString("I", true), Error, "bad formatted");
});

Deno.test("asActionResponseViewModelFromException handles Error objects", (): void => {
    const vm = asActionResponseViewModelFromException(new Error("err"));
    assertEquals(vm.responseType, ActionResponseViewModel.Error);
    assertEquals(vm.message, "err");
});

Deno.test("asActionResponseViewModelFromException handles string errors", (): void => {
    const vm = asActionResponseViewModelFromException("oops");
    assertEquals(vm.responseType, ActionResponseViewModel.Error);
    assertEquals(vm.message, "oops");
});
