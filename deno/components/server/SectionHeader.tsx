import * as React from "@preact/compat";

interface SectionHeaderProps {
    heading?: string;
    title: string;
    subtitle?: string;
}

export default function SectionHeader({ heading, title, subtitle }: SectionHeaderProps) {
    return (
        <div class="flex flex-col gap-3 md:flex-row md:items-center md:justify-between">
            <div>
                {heading ? <div class="text-xs uppercase tracking-[0.2em] text-base-content/50">{heading}</div> : null}
                <h2 class="text-2xl font-semibold">{title}</h2>
                {subtitle ? <p class="text-sm text-base-content/70">{subtitle}</p> : null}
            </div>
        </div>
    );
}
