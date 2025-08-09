import type { Signal } from "@preact/signals";
import { Partial } from "$fresh/runtime.ts";
/*import type { RouteConfig } from "$fresh/server.ts";
import { PartialOutputTextBox, type PartialOutputTextBoxProps } from "./PartialOutTextBox.tsx";*/

/*export const config: RouteConfig = {
    skipAppWrapper: true,
    skipInheritedLayouts: true
}*/

export interface OutputTextBoxProps {
    shortLabel: string;
    fullLabel?: string;
    // partialOutputTextBoxProps: PartialOutputTextBoxProps;
    //output: Signal<string>;
    outputText: string;
    placeholder?: string;
}

export function OutputTextBox(props: OutputTextBoxProps) {
    const label = props.fullLabel || `Output (${props.shortLabel})`;

    function onClearOutput() {
        props.outputText = "";
    }

    return (
        <div className="flex flex-col gap-2">
            <label className="text-sm opacity-80">
                {label}
            </label>
            {/*<Partial name="partial-output-textbox">
                <textarea
                    value=""
                    //* onInput={(e: any) => (output.value = e.currentTarget.value)} *
                    placeholder={props.placeholder}
                    className="textarea textarea-bordered textarea-lg w-full min-h-[260px] font-mono"
                />
            </Partial>*/}
            <textarea
                value={props.outputText}
                /* onInput={(e: any) => (output.value = e.currentTarget.value)} */
                placeholder={props.placeholder}
                className="textarea textarea-bordered textarea-lg w-full min-h-[260px] font-mono"
            />
            <div className="flex flex-wrap gap-2 mt-2">
                <button
                    type="button"
                    className="btn btn-primary">
                    To clipboard
                </button>
                <button
                    type="button"
                    className="btn btn-primary">
                    To .txt
                </button>
                <button
                    type="button"
                    className="btn btn-ghost" onClick={onClearOutput}>
                    Clear
                </button>
            </div>
        </div>
    );
}