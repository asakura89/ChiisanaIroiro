import { assertEquals } from "jsr:@std/assert";
import {validateASPError, onAjaxError, ASPError} from "../kayo-web/aspnet.ts";

class CommentNode {
    constructor(public nodeValue: string | null) {}
    nodeType: number = 8;
}

class DivElement {
    childNodes: CommentNode[] = [];
    private _innerHTML = "";
    set innerHTML(value: string) {
        this._innerHTML = value;
        this.childNodes = [];
        const regex: RegExp = /<!--([\s\S]*?)-->/g;
        let m: RegExpExecArray | null;
        while ((m = regex.exec(value)) !== null) {
            this.childNodes.push(new CommentNode(m[1]));
        }
    }
    get innerHTML() {
        return this._innerHTML;
    }
}

(globalThis as any).document = {
    createElement: (_: string) => new DivElement(),
};

(globalThis as any).Node = { COMMENT_NODE: 8 };

Deno.test("validateASPError parses ASP.NET comment", (): void => {
    const result: ASPError = validateASPError("<!--Test Message-->");
    assertEquals(result.IsASPError, true);
    assertEquals(result.Message, "Test Message");
});

Deno.test("validateASPError handles normal text", (): void => {
    const result: ASPError = validateASPError("No error");
    assertEquals(result.IsASPError, false);
    assertEquals(result.Message, "No error");
});

Deno.test("onAjaxError invokes callback with parsed message", (): void => {
    const xhr: XMLHttpRequest = { responseText: "<!--Ajax Error-->" } as unknown as XMLHttpRequest;
    let message = "";
    onAjaxError(xhr, (m: string) => (message = m));
    assertEquals(message, "Ajax Error");
});

Deno.test("onAjaxError falls back to responseText", (): void => {
    const xhr: XMLHttpRequest = { responseText: "Plain" } as unknown as XMLHttpRequest;
    let message = "";
    onAjaxError(xhr, (m: string) => (message = m));
    assertEquals(message, "Plain");
});
