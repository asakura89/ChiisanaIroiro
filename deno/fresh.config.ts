import { defineConfig, FreshConfig } from "$fresh/server.ts";
import tailwind from "@pakornv/fresh-plugin-tailwindcss";

export default defineConfig({
    server: {
        port: 16732
    },
    plugins: [
        tailwind()
    ]
} as FreshConfig);
