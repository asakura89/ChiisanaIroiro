import { Handlers } from "$fresh/server.ts";
import { loadHistory, saveHistoryEntry } from "./engine.ts";

export const handler: Handlers = {
    async GET() {
        const data = await loadHistory();
        return new Response(JSON.stringify(data), {headers: {"content-type": "application/json"}});
    },
    async POST(req) {
        const body = await req.json();
        await saveHistoryEntry(body);
        return new Response(JSON.stringify({ok: true}), {headers: {"content-type": "application/json"}});
    }
};
