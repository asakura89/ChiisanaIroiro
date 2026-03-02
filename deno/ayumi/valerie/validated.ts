import {ActionResponseViewModel} from "../arvy/action_response_view_model.ts";

export class Validated<TData> {
    messages: ActionResponseViewModel[] = [];
    validData: TData[] = [];

    get containsFail(): boolean {
        return this
            .messages
            .some(msg =>
                msg.responseType === ActionResponseViewModel.Error);
    }

    get allFails(): boolean {
        return this
            .messages
            .length > 0 &&
            this
                .messages
                .every(msg =>
                    msg.responseType === ActionResponseViewModel.Error);
    }
}