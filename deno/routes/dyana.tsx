import { useSignal } from "@preact/signals";
import { DyanaWindow } from "../islands/dyana.tsx";

export default function Index() {
    return (
        <DyanaWindow
            startAt={useSignal("09:00")}
            endAt={useSignal("18:00")}
            workHour={useSignal("8h")}
            breakHour={useSignal("1h")}
            total={useSignal("0")}
            responsibility={useSignal("0")}
            message={useSignal("")}
        />
    );
}

