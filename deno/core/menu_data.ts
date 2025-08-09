// import { t } from "./i18n.ts";

export interface MenuItem {
    href: string;
    title: string; // For i18n, wrap with a translation function in the future
    icon: string;
    class: string;
}

export const sidebarMenu: MenuItem[] = [{
        href: "/",
        title: "Dashboard", // t("dashboard"),
        icon: "ph-duotone ph-squares-four",
        class: "hover:bg-primary hover:text-primary-content rounded"
    },
    {
        href: "/tools",
        title: "Tools", // t("work_hours"),
        icon: "ph-duotone ph-hourglass-medium",
        class: "hover:bg-primary hover:text-primary-content rounded"
    }
];
