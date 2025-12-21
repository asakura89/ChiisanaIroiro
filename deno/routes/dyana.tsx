import * as React from "@preact/compat";
import { useSignal } from "@preact/signals";
import HtmlHead from "../components/server/HtmlHead.tsx";
import SectionHeader from "../components/server/SectionHeader.tsx";
import { DyanaWindow } from "../islands/dyana.tsx";

export default function Index() {
    return (
        <>
            <HtmlHead componentName="Dyana" />
            <div class="relative overflow-hidden bg-base-200 min-h-[calc(100vh-4rem)]">
                <div class="absolute inset-0">
                    <div class="absolute -top-24 -left-24 h-80 w-80 rounded-full bg-primary/20 blur-3xl"></div>
                    <div class="absolute top-40 -right-32 h-96 w-96 rounded-full bg-accent/20 blur-3xl"></div>
                </div>
                <div class="relative px-6 py-10 md:px-10">
                    <SectionHeader
                        heading="Time Tracking"
                        title="Dyana"
                        subtitle="Work hours calculator"
                    />
                    <div class="mt-8 max-w-3xl">
                        <DyanaWindow
                            startAt={useSignal("09:00")}
                            endAt={useSignal("18:00")}
                            workHour={useSignal("8h")}
                            breakHour={useSignal("1h")}
                            total={useSignal("0")}
                            responsibility={useSignal("0")}
                            message={useSignal("")}
                        />
                    </div>
                </div>
            </div>
        </>
    );
}

