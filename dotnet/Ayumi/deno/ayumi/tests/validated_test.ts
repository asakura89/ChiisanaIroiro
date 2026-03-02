import { Validated } from "../valerie/validated.ts";
import { ActionResponseViewModel } from "../arvy/action_response_view_model.ts";
import { assert } from "jsr:@std/assert";

Deno.test("containsFail returns true if any message type is Error", (): void => {
    const validated = new Validated<unknown>();
    validated.messages = [
        new ActionResponseViewModel(ActionResponseViewModel.Info, "info"),
        new ActionResponseViewModel(ActionResponseViewModel.Error, "bad"),
        new ActionResponseViewModel(ActionResponseViewModel.Success, "ok"),
    ];
    assert(validated.containsFail);
});

Deno.test("allFails returns true only when all messages are Error", (): void => {
    const allErrors = new Validated<unknown>();
    allErrors.messages = [
        new ActionResponseViewModel(ActionResponseViewModel.Error, "a"),
        new ActionResponseViewModel(ActionResponseViewModel.Error, "b"),
    ];
    assert(allErrors.allFails);

    const notAllErrors = new Validated<unknown>();
    notAllErrors.messages = [
        new ActionResponseViewModel(ActionResponseViewModel.Error, "a"),
        new ActionResponseViewModel(ActionResponseViewModel.Warning, "b"),
    ];
    assert(!notAllErrors.allFails);
});
