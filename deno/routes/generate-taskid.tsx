import { useSignal } from "@preact/signals";
import { FreshContext, Handlers, PageProps } from "$fresh/server.ts";
import { OutTextBoxView, OutTextBoxGeneralProps } from "../islands/OutTextBoxView.tsx";
import { readInteger } from "../app/form_data_reader.ts";
import { generateCurrentDateString } from "../app/current_datetime.ts";

/**
function getCurrentDateAsYYYYMMDD(): string {
    const now = new Date();
    const year = now.getFullYear();
    const month = String(now.getMonth() +1).padStart(2, "0");
    const day = String(now.getDate()).padStart(2, "0");

    return `${year}${month}${day}`
}
*/

export const handler: Handlers = {
    async GET(_req, ctx: FreshContext) {
        return await ctx.render({
            AdditionalControlsProps: {
                startAt: 1,
                iterateCount: 5,
                output: ""
            },
            OutTextBoxGeneralProps: {
                inputText: "",
                outputText: "",
                error: ""
            }
        });
    },

    async POST(req: Request, ctx: FreshContext) {
        const form: FormData = await req.formData();
        const startAt: number = readInteger(form, "startat-textbox", { min: 1, max: 10, default: 1 }) ?? 0;
        const iterateCount: number = readInteger(form, "iteratecount-textbox", { min: 1, max: 100, default: 5 }) ?? 0;

        let current: number = startAt;
        const end: number = current + iterateCount;
        const datePrefix: string = generateCurrentDateString();
        const lines: string[] = [];

        while (current < end) {
            lines.push(datePrefix + String(current++).padStart(4, "0"));
        }

        return await ctx.render({
            AdditionalControlsProps: {
                startAt: startAt,
                iterateCount: iterateCount,
                output: ""
            },
            OutTextBoxGeneralProps: {
                inputText: "",
                outputText: lines.join("\n"),
                error: ""
            }
        });
    }
}

interface AdditionalControlsProps {
    startAt: number;
    iterateCount: number;
    output: string;
}

function AdditionalControl(props: AdditionalControlsProps) {
    return (
        <fieldset className="fieldset">
            <label className="input">
                <span className="label">Start At</span>
                <input
                    name="startat-textbox"
                    type="number"
                    min={1}
                    step={1}
                    value={props.startAt}
                    /*onInput={(e) => props.startAt = clampInt(Number((e.currentTarget as HTMLInputElement).value || 0), 0, 10000)}*/
                />
            </label>

            <label className="input">
                <span className="label">Iterate Count</span>
                <input
                    name="iteratecount-textbox"
                    type="number"
                    min={1}
                    step={1}
                    value={props.iterateCount}
                    /*onInput={(e) => props.iterateCount = clampInt(Number((e.currentTarget as HTMLInputElement).value || 0), 0, 8192)}*/
                />
            </label>
        </fieldset>
    );
}

interface ThisPageProps {
    AdditionalControlsProps: AdditionalControlsProps;
    OutTextBoxGeneralProps: OutTextBoxGeneralProps;
}

export default function Generate(props: PageProps<ThisPageProps>) {
    return (
        <OutTextBoxView
            toolLabel="Generate Task Id"
            toolDescription="Generate sequential number-based Id."
            children={
                <AdditionalControl
                    startAt={props.data.AdditionalControlsProps.startAt}
                    iterateCount={props.data.AdditionalControlsProps.iterateCount}
                    output={props.data.AdditionalControlsProps.output}
                />}
            processIdentifier="generate-taskid"
            outputTextBoxProps={{
                fullLabel: "Output",
                outputText: useSignal(props.data.OutTextBoxGeneralProps.outputText),
                processButtonLabel: "Generate",
                placeholder: "Generated Id will appear here..."
            }}
        />
    );
}

