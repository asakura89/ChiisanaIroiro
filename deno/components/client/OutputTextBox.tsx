import type { Signal } from "@preact/signals";
import { Wielder } from "../../ayumi/keywielder/wielder.ts";

export interface OutputTextBoxProps {
    fullLabel: string;
    shortLabel?: string;
    outputText: Signal<string>;
    placeholder?: string;
    processButtonLabel?: string;
    isOutputOnly?: boolean;
}

export function OutputTextBox(props: OutputTextBoxProps) {
    const label = props.fullLabel || `Output (${props.shortLabel})`;
    const buttonLabel = props.processButtonLabel || "Process";

    function onToClipboard() {
        navigator.clipboard
            .writeText(props.outputText.value)
            .then(() => {
                //toast("Output copied to clipboard.");
                console.log("Output copied to clipboard.");
            })
            .catch(() => {
                //toast("Clipboard blocked by browser.");
                console.log("Clipboard blocked by browser.");
            });
    }

    function onToTxt() {
        const blob = new Blob([props.outputText.value], { type: "text/plain;charset=utf-8" });
        const url = URL.createObjectURL(blob);
        const currentTime =
            Wielder.new()
                .addLongYear()
                .addLeftPadded(
                    Wielder.new()
                        .addNumericMonth()
                        .buildKey(),
                    2, "0")
                .addLeftPadded(
                    Wielder.new()
                        .addDate()
                        .buildKey(),
                    2, "0")
                .addLeftPadded(
                    new Date().getHours().toString(),
                    2, "0")
                .addLeftPadded(
                    new Date().getMinutes().toString(),
                    2, "0")
                .addLeftPadded(
                    new Date().getSeconds().toString(),
                    2, "0")
                .buildKey();
        const filename = `output_${currentTime}.txt`;

        const a = document.createElement("a");
        a.href = url;
        a.download = filename;
        document.body.appendChild(a);
        a.click();
        a.remove();
        URL.revokeObjectURL(url);
        //toast("Downloaded decoded.txt");
        console.log(`Downloaded ${filename}`);
    }

    function onClearOutput() {
        props.outputText.value = "";
    }

    return (
        <div className="flex flex-col gap-2">
            <label className="text-sm opacity-80">
                {label}
            </label>

            <textarea
                id="output-textbox"
                value={props.outputText}
                placeholder={props.placeholder}
                className="textarea textarea-bordered textarea-lg w-full min-h-[260px] font-mono"
            />
            <div className="flex flex-wrap gap-2 mt-2">
                {props.isOutputOnly && (
                    <button type="submit" className="btn btn-primary">
                        {buttonLabel}
                    </button>
                )}
                <button
                    type="button"
                    className="btn btn-primary"
                    onClick={onToClipboard}>
                    To clipboard
                </button>
                <button
                    type="button"
                    className="btn btn-primary"
                    onClick={onToTxt}>
                    To .txt
                </button>
                <button
                    type="button"
                    className="btn btn-ghost"
                    onClick={onClearOutput}>
                    Clear
                </button>
            </div>
        </div>
    );
}