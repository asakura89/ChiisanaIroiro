import { Handlers } from "$fresh/server.ts";
import { loadVouchers, evaluateVouchersTotal, applyFixedDiscounts } from "./engine.ts";

export const handler: Handlers = {
    async POST(req) {
        const body = await req.json();
        const basePrice: number = Number(body.basePrice || 0);
        const extras = Array.isArray(body.extraComponents) ? body.extraComponents : [];
        const paymentMethod = body.paymentMethod;
        const deliveryMethod = body.deliveryMethod;
        const selectedVoucherIds: string[] = body.selectedVoucherIds || [];

        const extrasTotal = extras.reduce((s: number, e: any) => s + Number(e.amount || 0), 0);
        const originalTotal = basePrice + extrasTotal;

        const vouchers = await loadVouchers();

        const {
            applied,
            finalAfterVouchers
        } = evaluateVouchersTotal(originalTotal, vouchers, paymentMethod, deliveryMethod, selectedVoucherIds);

        const fixedRules = [
            {amount: 0}
        ];

        const {fixed, remaining} = applyFixedDiscounts(finalAfterVouchers, basePrice, fixedRules);

        const finalTotal = remaining;

        const response = {
            originalTotal,
            appliedVouchers: applied,
            fixedDiscount: fixed,
            finalTotal,
            timestamp: new Date().toISOString()
        };

        return new Response(JSON.stringify(response), {headers: {"content-type": "application/json"}});
    }
};
