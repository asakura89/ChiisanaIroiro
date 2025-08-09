import { FreshContext } from "$fresh/server.ts";
import { AppContext } from "../ayumi/mod.ts";

async function initServerApp(_req: Request, ctx: FreshContext) {
    /* initialize AppContext */
    ctx.state.AppContext = AppContext.getInstance();
    const res = await ctx.next();

    return res;
}

async function reqLogging(req: Request, ctx: FreshContext) {
    const res = await ctx.next();
    console.log(`${req.method} ${res.status} ${req.url}`);
    return res;
}

export const handler = [
    initServerApp,
    reqLogging
];