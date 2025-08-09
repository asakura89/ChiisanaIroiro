import type { Signal } from "@preact/signals";

export interface InputTextBoxProps {
    shortLabel: string;
    fullLabel?: string;
    //input: Signal<string>;
    inputText: string;
    placeholder?: string;
    processIdentifier: string;
    processButtonLabel?: string;
}

/*function onCopy() {
    navigator.clipboard.writeText(output).then(() => {
        toast("Output copied to clipboard.");
    }).catch(() => toast("Clipboard blocked by browser."));
}

function onDownload() {
    const blob = new Blob([output], { type: "text/plain;charset=utf-8" });
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = "decoded.txt"; // You can change this name
    document.body.appendChild(a);
    a.click();
    a.remove();
    URL.revokeObjectURL(url);
    toast("Downloaded decoded.txt");
}*/

export function InputTextBox(props: InputTextBoxProps) {
    const label = props.fullLabel || `Input (${props.shortLabel})`;
    const buttonLabel = props.processButtonLabel || "Process";

    /*function onProcess(e: MouseEvent) {
        const button = e.target as HTMLButtonElement;
        const processId = button.dataset.processid;

    }*/

    function onClearInput() {
        props.inputText = "";
    }

    return (
        <div className="flex flex-col gap-2">
            <form method="post" action={`/${props.processIdentifier}`}>
                <label className="text-sm opacity-80">
                    {label}
                </label>
                <textarea
                    name="input"
                    value={props.inputText}
                    /* onInput={(e: any) => (input.value = e.currentTarget.value)} */
                    placeholder={props.placeholder}
                    className="textarea textarea-bordered textarea-lg w-full min-h-[260px] font-mono"
                />
                <div className="flex gap-2 mt-2">
                    <button
                        type="submit"
                        className="btn btn-primary"
                        /*onClick={onProcess}*/>
                        {buttonLabel}
                    </button>
                    <button
                        type="button"
                        className="btn btn-ghost"
                        onClick={onClearInput}>
                        Clear
                    </button>
                </div>
            </form>
        </div>
    );
}