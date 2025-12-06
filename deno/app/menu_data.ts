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
    }, {
        href: "/dyana",
        title: "Dyana - Work hours calculator", // t("work_hours"),
        icon: "ph-duotone ph-hourglass-medium",
        class: "hover:bg-primary hover:text-primary-content rounded"
    }, {
        href: "/generate-hex",
        title: "Hex Generator",
        icon: "ph-duotone ph-hourglass-medium",
        class: "hover:bg-primary hover:text-primary-content rounded"
    }, {
        href: "/generate-guid",
        title: "Guid Generator",
        icon: "ph-duotone ph-hourglass-medium",
        class: "hover:bg-primary hover:text-primary-content rounded"
    }, {
        href: "/generate-taskid",
        title: "Task Id Generator",
        icon: "ph-duotone ph-hourglass-medium",
        class: "hover:bg-primary hover:text-primary-content rounded"
    }, {
        href: "/timespan-explainer",
        title: "Timespan Explainer",
        icon: "ph-duotone ph-hourglass-medium",
        class: "hover:bg-primary hover:text-primary-content rounded"
    }]
    .sort((a: MenuItem, b: MenuItem) => {
        const skip = [
            "dashboard"
        ];
        const aSkip = skip.includes(a.title.toLowerCase());
        const bSkip = skip.includes(b.title.toLowerCase());

        if (aSkip && bSkip)
            return 0;
        if (aSkip)
            return -1;
        if (bSkip)
            return 1;

        return a.title.localeCompare(b.title);
    });
