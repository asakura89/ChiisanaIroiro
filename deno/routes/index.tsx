import * as React from "@preact/compat" // https://github.com/denoland/fresh/issues/785
import { useSignal } from "@preact/signals";
import Counter from "../islands/Counter.tsx";

import type { PageProps } from "$fresh/server.ts";
import HtmlHead from "../components/server/HtmlHead.tsx";

export default function Dashboard(props: PageProps) {
    const COMPONENT_NAME = "Dashboard";
    props.Component.displayName = COMPONENT_NAME;
    const count = useSignal(3);

    return (
        <>
            <HtmlHead componentName={COMPONENT_NAME} />
            <div class="hero h-4/5 bg-base-200">
                <div class="hero-content">
                    <div class="max-w-md">
                        <h1>{COMPONENT_NAME}</h1>
                        <p class="py-2 mt-4">
                            📈 Welcome to <span class="font-semibold">{COMPONENT_NAME}</span>
                        </p>
                        <img
                            class="my-6"
                            src="/logo.svg"
                            width="128"
                            height="128"
                            alt="the Fresh logo: a sliced lemon dripping with juice"
                        />
                        <h1 class="text-4xl font-bold">Welcome to Fresh</h1>
                        <p class="my-4">
                            Try updating this message in the
                            <code class="mx-2">./routes/index.tsx</code> file, and refresh.
                        </p>
                        <Counter count={count} />
                    </div>
                </div>
            </div>
        </>
    );
}
