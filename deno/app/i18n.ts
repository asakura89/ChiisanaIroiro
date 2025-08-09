const en_US: Record<string, string> = { };

const id_ID: Record<string, string> = { };

export function t(key: string, lang: "en_US" | "id_ID" = "en_US"): string {
    const dict = lang === "id_ID" ? id_ID : en_US;
    return dict[key] || key;
}
