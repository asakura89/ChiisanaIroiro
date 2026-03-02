import { ComponentChildren } from "preact";
import { InputTextBox, type InputTextBoxProps } from "../components/client/InputTextBox.tsx";
import { OutputTextBox, type OutputTextBoxProps } from "../components/client/OutputTextBox.tsx";

export interface InOutTextBoxGeneralProps {
    inputText: string;
    outputText: string;
    error: string;
}

export interface InOutTextBoxViewProps {
    toolLabel: string;
    toolDescription: string;
    children?: ComponentChildren;
    processIdentifier: string;
    inputTextBoxProps: InputTextBoxProps;
    outputTextBoxProps: OutputTextBoxProps;
}

function RenderAdditionalControls(controls: ComponentChildren) {
    if (controls && controls !== null) {
        return (
            <div className="mb-4">
                {controls}
            </div>
        );
    }
    else
        return null;
}

export function InOutTextBoxView(props: InOutTextBoxViewProps) {
    return (
        <div className="min-h-screen bg-base-200 text-base-content flex items-center justify-center p-6">
            <div className="w-full max-w-[1100px] card bg-base-300 border border-base-300 shadow-2xl rounded-2xl p-6">
                <h1 className="text-2xl font-bold m-0">{props.toolLabel}</h1>
                <p className="opacity-80 mt-2 mb-5">
                    {props.toolDescription}
                </p>

                <form method="post" action={`/${props.processIdentifier}`}>
                    {/**
                     https://fresh.deno.dev/docs/1.x/concepts/islands#passing-jsx-to-islands
                     must be named "children"
                     */}
                    {RenderAdditionalControls(props.children)}

                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                        <InputTextBox {...props.inputTextBoxProps} />

                        <OutputTextBox {...props.outputTextBoxProps} />
                    </div>
                </form>
            </div>
        </div>
    );
}