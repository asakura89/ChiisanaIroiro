import { useSignal } from "@preact/signals";
import { FreshContext, Handlers, PageProps } from "$fresh/server.ts";
import { Wielder } from "../ayumi/keywielder/wielder.ts";
import { OutTextBoxView, OutTextBoxGeneralProps } from "../islands/OutTextBoxView.tsx";

function clampInt(n: number, min: number, max: number) {
    return Math.max(min, Math.min(max, Math.floor(n)));
}

function readText(form: FormData, name: string): string | null {
    const value = form.get(name);
    if (value === null)
        return null;

    if (typeof value === "string")
        return value;

    return null;
}

export function readFloat(form: FormData, name: string, opts: { min?: number; max?: number; default?: number } = {}): number | null {
    const raw = readText(form, name);
    if (raw === null || raw.trim() === "")
        return opts.default ?? null;

    const float = Number(raw);
    if (Number.isNaN(float))
        return opts.default ?? null;

    if (opts.min !== undefined && float < opts.min)
        return opts.default ?? null;

    if (opts.max !== undefined && float > opts.max)
        return opts.default ?? null;

    return float;
}

export function readInteger(form: FormData, name: string, opts: { min?: number; max?: number; default?: number } = {}): number | null {
    const float = readFloat(form, name);
    if (float === null)
        return opts.default ?? null;

    return Math.floor(float);
}

export function readBoolean(form: FormData, name: string, opts: { default?: boolean } = {}): boolean {
    const raw = readText(form, name);
    if (raw === null)
        return opts.default ?? false;

    const selection = raw.toLowerCase();
    if (["1", "true", "on", "yes"].includes(selection))
        return true;

    if (["0", "false", "off", "no"].includes(selection))
        return false;

    return true;
}

export const handler: Handlers = {
    async GET(_req, ctx: FreshContext) {
        const dataProps = {
            AdditionalControlsProps: {
                count: 1,
                length: 16,
                uppercase: false,
                output: ""
            },
            OutTextBoxGeneralProps: {
                outputText: "",
                error: ""
            }
        };

        return await ctx.render(dataProps);
    },

    async POST(req: Request, ctx: FreshContext) {
        const form: FormData = await req.formData();
        const count = readInteger(form, "count-textbox", { min: 1, max: 10, default: 1 });
        const length = readInteger(form, "length-textbox", { min: 1, max: 48, default: 16 });
        const uppercase = readBoolean(form, "upper-checkbox", { default: false });

        let counter: number = 0;
        const output: string[] = [];
        while (counter < (count||0)) {
            output.push(
                Wielder.new()
                    .addRandomHex((length||0), uppercase)
                    .buildKey()
            );

            counter++;
        }

        const dataProps = {
            AdditionalControlsProps: {
                count: count,
                length: length,
                uppercase: uppercase,
                output: ""
            },
            OutTextBoxGeneralProps: {
                outputText: output.join("\n"),
                error: ""
            }
        };

        return await ctx.render(dataProps);
    }
}

interface AdditionalControlsProps {
    count: number;
    length: number;
    uppercase: boolean;
    output: string;
}

function AdditionalControl(props: AdditionalControlsProps) {
    return (
        <>
            <fieldset className="fieldset">
                <label className="input">
                    <span className="label">Count</span>
                    <input
                        name="count-textbox"
                        type="number"
                        min={1}
                        step={1}
                        value={props.count}
                        onInput={(e) => props.count = clampInt(Number((e.currentTarget as HTMLInputElement).value || 0), 0, 10000)}
                    />
                </label>

                <label className="input">
                    <span className="label">Length</span>
                    <input
                        name="length-textbox"
                        type="number"
                        min={1}
                        step={1}
                        value={props.length}
                        onInput={(e) => props.length = clampInt(Number((e.currentTarget as HTMLInputElement).value || 0), 0, 8192)}
                    />
                </label>

                <label className="input">
                    <input
                        name="upper-checkbox"
                        type="checkbox"
                        class="checkbox"
                        checked={props.uppercase}
                        onChange={(e) => props.uppercase = (e.currentTarget as HTMLInputElement).checked}
                    />
                    <span className="label">Uppercase</span>
                </label>
            </fieldset>
        </>
    );
}

interface GenerateHexProps {
    AdditionalControlsProps: AdditionalControlsProps;
    OutTextBoxGeneralProps: OutTextBoxGeneralProps;
}

export default function GenerateHex(props: PageProps<GenerateHexProps>) {
    return (
        <OutTextBoxView
            toolLabel="Generate Hex"
            toolDescription="Generate a random hex string."
            children={
                <AdditionalControl
                    count={props.data.AdditionalControlsProps.count!}
                    length={props.data.AdditionalControlsProps.length!}
                    uppercase={props.data.AdditionalControlsProps.uppercase}
                    output={props.data.AdditionalControlsProps.output}
                />
            }
            processIdentifier="generate-hex"
            outputTextBoxProps={{
                fullLabel: "",
                outputText: useSignal(props.data.OutTextBoxGeneralProps.outputText),
                processButtonLabel: "Generate",
                placeholder: "Generated hex will appear here..."
            }}
        />
    );
}

