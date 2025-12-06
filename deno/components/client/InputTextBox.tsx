export interface InputTextBoxProps {
    fullLabel: string;
    shortLabel?: string;
    inputText: string;
    placeholder?: string;
    processButtonLabel?: string;
}

export function InputTextBox(props: InputTextBoxProps) {
    const label = props.fullLabel || `Input (${props.shortLabel})`;
    const buttonLabel = props.processButtonLabel || "Process";

    function onClearInput() {
        props.inputText = "";
    }

    return (
        <div className="flex flex-col gap-2">
            <label className="text-sm opacity-80">
                {label}
            </label>
            <textarea
                name="input"
                value={props.inputText}
                placeholder={props.placeholder}
                className="textarea textarea-bordered textarea-lg w-full min-h-[260px] font-mono"
            />
            <div className="flex gap-2 mt-2">
                <button
                    type="submit"
                    className="btn btn-primary">
                    {buttonLabel}
                </button>
                <button
                    type="button"
                    className="btn btn-ghost"
                    onClick={onClearInput}>
                    Clear
                </button>
            </div>
        </div>
    );
}