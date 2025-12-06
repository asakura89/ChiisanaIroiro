import { useSignal } from "@preact/signals";
import { FreshContext, Handlers, PageProps } from "$fresh/server.ts";
import { InOutTextBoxGeneralProps, InOutTextBoxView } from "../islands/InOutTextBoxView.tsx";
import { Duration } from "../ayumi/itsu/duration.ts";

const styles: Record<string, any> = {
    page: {
        minHeight: "100vh",
        background: "#0b1020",
        color: "#eaf0ff",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        padding: "24px",
    },
    card: {
        width: "min(1100px, 100%)",
        background: "#121933",
        border: "1px solid #243056",
        borderRadius: "16px",
        padding: "24px",
        boxShadow: "0 10px 30px rgba(0,0,0,0.25)",
    },
    title: { fontSize: "24px", margin: 0, fontWeight: 700 },
    sub: { opacity: 0.8, marginTop: 8, marginBottom: 20 },
    grid: {
        display: "grid",
        gridTemplateColumns: "1fr 1fr",
        gap: "16px",
    },
    section: { display: "flex", flexDirection: "column", gap: 8 },
    label: { fontSize: 14, opacity: 0.85 },
    textarea: {
        width: "100%",
        minHeight: 260,
        resize: "vertical" as const,
        borderRadius: 12,
        padding: 12,
        background: "#0c1430",
        color: "#eaf0ff",
        border: "1px solid #243056",
        outline: "none",
        fontFamily:
            "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, 'Liberation Mono', 'Courier New', monospace",
        fontSize: 14,
    },
    row: { display: "flex", gap: 8, marginTop: 8 },
    rowWrap: {
        display: "flex",
        flexWrap: "wrap" as const,
        gap: 8,
        marginTop: 8,
    },
    btn: {
        padding: "10px 14px",
        borderRadius: 10,
        border: "1px solid #3b4a7a",
        background: "#1a2550",
        color: "#eaf0ff",
        cursor: "pointer",
        fontSize: 14,
    },
    btnGhost: {
        padding: "10px 14px",
        borderRadius: 10,
        border: "1px solid #2b365e",
        background: "transparent",
        color: "#b7c6ff",
        cursor: "pointer",
        fontSize: 14,
    },
    error: {
        marginTop: 12,
        padding: "10px 12px",
        borderRadius: 10,
        border: "1px solid #6b2737",
        background: "#35121e",
        color: "#ffbfc9",
        fontSize: 14,
    },
    toast: {
        position: "fixed",
        bottom: 16,
        left: "50%",
        transform: "translateX(-50%)",
        background: "#1a2550",
        border: "1px solid #3b4a7a",
        color: "#eaf0ff",
        borderRadius: 999,
        padding: "10px 14px",
        fontSize: 14,
        pointerEvents: "none",
    },
};

function round(toBeRounded: number): number {
    return Math.ceil(
        Math.round(
            toBeRounded
        ) *2
    ) /2;
}

export const handler: Handlers = {
    async GET(_req, ctx: FreshContext) {
        return await ctx.render({
            inputText: "",
            outputText: "",
            error: ""
        });
    },

    async POST(req: Request, ctx: FreshContext) {
        const form: FormData = await req.formData();
        const input: string = form.get("input") as string;
        const duration: Duration = Duration.fromString(input);
        const output: string = Array.prototype.join.call([
            `Total Days: ${round(duration.totalDays)}`,
            `Total Hours: ${round(duration.totalHours)}`,
            `Total Minutes: ${round(duration.totalMinutes)}`,
            `Total Seconds: ${round(duration.totalSeconds)}`,
            `Total Milliseconds: ${round(duration.totalMilliseconds)}`
        ],"\n");

        return await ctx.render({
            inputText: input,
            outputText: output,
            error: ""
        });
    }
}

export default function TimespanExplainer(props: PageProps<InOutTextBoxGeneralProps>/*: TimespanExplainerProps*/) {
    return (
        <InOutTextBoxView
            toolLabel="Timespan Explainer"
            toolDescription="This tool explains a timespan in a human-readable format."
            processIdentifier="timespan-explainer"
            inputTextBoxProps={{
                fullLabel: "Timespan",
                inputText: props.data.inputText,
                placeholder: "Enter a timespan (e.g., 2d, 3h, 422s, 457872ms)",
                processButtonLabel: "Explain"
            }}
            outputTextBoxProps={{
                fullLabel: "Explanation",
                outputText: useSignal(props.data.outputText),
                placeholder: "The explanation will appear here..."
            }}
        />
    );
}

