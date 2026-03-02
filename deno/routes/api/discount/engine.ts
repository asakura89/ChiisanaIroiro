import { ensureFile } from "@std/fs";

const VOUCHER_PATH = "app_data/discount_vouchers.json";
const HISTORY_PATH = "app_data/discount_history.json";
const TEMPLATES_PATH = "app_data/discount_templates.json";

export type Voucher = {
    id: string;
    name: string;
    category: string;
    description: string;
    type: "percent" | "fixed";
    percent?: number;
    cap?: number;
    amount?: number;
    minSpend?: number;
    paymentMethod?: string;
    deliveryMethod?: string;
    stackableWithSameCategory?: boolean;
    stackableWithOtherCategories?: boolean;
    priority?: number;
    expiresAt?: string | null;
    usageLimit?: number | null;
};

async function readJson(path: string) {
    try {
        const txt = await Deno.readTextFile(path);
        return JSON.parse(txt);
    }
    catch (_e) {
        return null;
    }
}

export async function loadVouchers(): Promise<Voucher[]> {
    await ensureFile(VOUCHER_PATH);
    const data = await readJson(VOUCHER_PATH);
    if (!data) {
        return [];
    }
    return data as Voucher[];
}

export async function saveVouchers(vouchers: Voucher[]) {
    await ensureFile(VOUCHER_PATH);
    await Deno.writeTextFile(VOUCHER_PATH, JSON.stringify(vouchers, null, 2));
}

export async function loadHistory() {
    await ensureFile(HISTORY_PATH);
    const data = await readJson(HISTORY_PATH);
    return data || [];
}

export async function saveHistoryEntry(entry: any) {
    await ensureFile(HISTORY_PATH);
    const existing = await loadHistory();
    existing.push(entry);
    await Deno.writeTextFile(HISTORY_PATH, JSON.stringify(existing, null, 2));
}

export async function loadTemplates() {
    await ensureFile(TEMPLATES_PATH);
    const data = await readJson(TEMPLATES_PATH);
    return data || [];
}

export async function saveTemplate(tpl: any) {
    await ensureFile(TEMPLATES_PATH);
    const existing = await loadTemplates();
    existing.push(tpl);
    await Deno.writeTextFile(TEMPLATES_PATH, JSON.stringify(existing, null, 2));
}

function matchesRule(v: Voucher, paymentMethod?: string, deliveryMethod?: string) {
    if (v.paymentMethod && paymentMethod !== v.paymentMethod) {
        return false;
    }
    if (v.deliveryMethod && deliveryMethod !== v.deliveryMethod) {
        return false;
    }
    return true;
}

export function evaluateVouchersTotal(total: number, availableVouchers: Voucher[], paymentMethod?: string, deliveryMethod?: string, selectedIds?: string[]) {
    const now = Date.now();
    let candidates = availableVouchers.filter(v => {
        if (v.expiresAt) {
            const exp = Date.parse(v.expiresAt);
            if (isFinite(exp) && exp < now) {
                return false;
            }
        }
        if (!matchesRule(v, paymentMethod, deliveryMethod)) {
            return false;
        }
        if (v.minSpend && total < v.minSpend) {
            return false;
        }
        if (selectedIds && selectedIds.length > 0 && !selectedIds.includes(v.id)) {
            return false;
        }
        return true;
    });

    candidates.sort((a, b) => (a.priority || 0) - (b.priority || 0));

    const applied: any[] = [];
    let remaining = total;

    for (const v of candidates) {
        const conflictSameCategory = applied.some(a => a.category === v.category) && !v.stackableWithSameCategory;
        const conflictOther = applied.length > 0 && !v.stackableWithOtherCategories;
        if (conflictSameCategory || conflictOther) {
            continue;
        }

        let discount = 0;
        if (v.type === "percent" && v.percent) {
            discount = Math.floor(remaining * (v.percent / 100));
            if (v.cap) {
                discount = Math.min(discount, v.cap);
            }
        }
        else if (v.type === "fixed" && v.amount) {
            discount = Math.min(v.amount, remaining);
        }

        if (discount <= 0) {
            continue;
        }

        applied.push({id: v.id, name: v.name, discount, category: v.category});
        remaining = Math.max(0, remaining - discount);
    }

    return {applied, finalAfterVouchers: remaining};
}

export function applyFixedDiscounts(remaining: number, basePrice: number, rules: any[]) {
    let fixed = 0;
    for (const r of rules || []) {
        const conditionsMet = (!r.requiredPriceComponent || basePrice >= (r.requiredPriceComponentAmount || 0)) && (!r.paymentMethod || r.paymentMethod === r.paymentMethod) && (!r.deliveryMethod || r.deliveryMethod === r.deliveryMethod);
        if (!conditionsMet) {
            continue;
        }
        fixed += r.amount || 0;
        remaining = Math.max(0, remaining - (r.amount || 0));
    }
    return {fixed, remaining};
}
