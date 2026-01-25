import { Handlers } from "$fresh/server.ts";
import { loadVouchers, saveVouchers } from "./engine.ts";

export const handler: Handlers = {
    async GET() {
        const vouchers = await loadVouchers();
        return new Response(JSON.stringify(vouchers), {headers: {"content-type": "application/json"}});
    },
    async POST(req) {
        const body = await req.json();
        await saveVouchers(body);
        return new Response(JSON.stringify({ok: true}), {headers: {"content-type": "application/json"}});
    }
};
