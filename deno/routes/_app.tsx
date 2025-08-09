import * as React from "@preact/compat" // https://github.com/denoland/fresh/issues/785
import { type PageProps } from "$fresh/server.ts";
import { asset } from "$fresh/runtime.ts";
import APP_CONSTANTS from "../app/app_constants.ts";
import { sidebarMenu, type MenuItem } from "../app/menu_data.ts";

export default function App({Component}: PageProps) {
    return (
        <html lang="en">
            <head>
                <title>{APP_CONSTANTS.APP_NAME}</title>

                {/** [Meta] */}
                <meta charset="utf-8" />
                <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
                <meta http-equiv="X-UA-Compatible" content="IE=edge" />
                <meta name="description" content="{APP_CONSTANTS.APP_DESCRIPTION}" />
                <meta name="keywords" content="{APP_CONSTANTS.APP_KEYWORDS}" />
                <link rel="icon" href={asset("/logo.svg")} type="image/x-icon" />

                {/** [Assets] */}
                <link rel="stylesheet" href={asset("/styles.css")} id="main-style-link" />
            </head>

            <body data-theme="dark">
            <div class="drawer drawer-open">
                <input id="left-sidebar-drawer" type="checkbox" class="drawer-toggle"/>

                {/** [ Sidebar ] start */}
                <nav class="drawer-side z-30" aria-label="Sidebar Menu">
                    <label for="left-sidebar-drawer" aria-label="close sidebar" class="drawer-overlay"></label>
                    {/* Sidebar menu items data */}
                    <ul class="menu pt-4 overflow-y-auto w-70 bg-base-100 min-h-full text-base-content">
                        <li>
                            <a class="text-2xl font-bold text-base-content" href="/">{APP_CONSTANTS.APP_NAME}</a>
                        </li>
                        {sidebarMenu.map((item: MenuItem) => (
                            <li key={item.href}>
                                <a class={item.class} href={item.href}>
                                    <i class={item.icon}></i>
                                    {/* TODO: Replace with i18n function, e.g. t(item.title) */}
                                    {item.title}
                                </a>
                            </li>
                        ))}
                    </ul>
                </nav>
                {/** [ Sidebar ] end */}

                <div class="drawer-content flex flex-col">
                    <label for="left-sidebar-drawer" class="hidden"></label>
                    {/** [ Topbar Navigation ] start */}
                    <nav class="navbar bg-neutral text-neutral-content">
                        <div class="navbar-start">
                            <a class="text-md normal-case" href="/">{APP_CONSTANTS.APP_NAME}</a>
                            <ul class="menu menu-horizontal p-0">
                                <li>
                                    <a class="btn btn-ghost rounded" href="/">Dashboard</a>
                                </li>
                                <li>
                                    <details>
                                        <summary class="btn btn-ghost rounded">Mini Apps</summary>
                                        <ul class="bg-neutral p-2 rounded-box w-52">
                                            {sidebarMenu.filter(item => item.href !== "/").map((item: MenuItem) => (
                                                <li key={item.href}>
                                                    <a class={item.class} href={item.href}>
                                                        <i class={item.icon}></i>
                                                        {item.title}
                                                    </a>
                                                </li>
                                            ))}
                                        </ul>
                                    </details>
                                </li>
                            </ul>
                        </div>
                    </nav>
                    {/** [ Topbar Navigation ] end */}

                    {/** [ Container ] start */}
                    <main class="flex-1 overflow-y-auto pt-0 px-0 bg-base-200">
                        <Component/>
                    </main>
                    {/** [ Container ] end */}
                </div>
            </div>

            {/** [Body Assets] */}
            </body>
        </html>
    );
}
