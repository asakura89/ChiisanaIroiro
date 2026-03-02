/// <reference lib="dom" />

import { asArray } from "../kayo-core/core.ts";

export interface ASPError {
    IsASPError: boolean;
    Message: string;
}

export function validateASPError(errorMessage: string): ASPError {
    const tmp: HTMLDivElement = document.createElement("div");
    tmp.innerHTML = errorMessage;

    const comments: ChildNode[] = asArray(tmp.childNodes)
        .filter((el: Node): boolean =>
            el.nodeType === Node.COMMENT_NODE);

    const error: { IsASPError: boolean; Message: string } = {
        IsASPError: false,
        Message: errorMessage
    };

    if (comments.length > 0) {
        error.IsASPError = true;
        error.Message = comments[0].nodeValue ?? "";
    }

    return error;
}

export function onAjaxError(data: XMLHttpRequest, action?: (message: string) => void): void {
    const error: ASPError = validateASPError(data.responseText);
    const message: string = error.IsASPError ?
        error.Message :
        data.responseText;

    if (action)
        action(message);

    console.log(message);
}