import { Handlers } from "$fresh/server.ts";
import { loadTemplates, saveTemplate } from "./engine.ts";

export const handler: Handlers = {
    async GET() {
        const data = await loadTemplates();
        return new Response(JSON.stringify(data), {headers: {"content-type": "application/json"}});
    },
    async POST(req) {
        const body = await req.json();
        await saveTemplate(body);
        return new Response(JSON.stringify({ok: true}), {headers: {"content-type": "application/json"}});
    }
};
