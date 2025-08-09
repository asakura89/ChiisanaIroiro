import { Head } from "$fresh/runtime.ts";
import APP_CONSTANTS from "../core/app_constants.ts";
import ClientContext from "./ClientContext.tsx";

interface HtmlHeadProps {
    componentName: string;
}

export default function HtmlHead(props: HtmlHeadProps) {
    const PAGE_TITLE = `${props.componentName} | ${APP_CONSTANTS.APP_NAME}`;
    const context = new Map<string, Record<string, unknown>>();
    context.set(`ComponentContext.${props.componentName}`, {
        "AppName": APP_CONSTANTS.APP_NAME
    });

    return (
        <>
            <Head>
                <title>{PAGE_TITLE}</title>
                <ClientContext value={context} />
            </Head>
        </>
    );
}
