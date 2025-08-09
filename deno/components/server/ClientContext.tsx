import { encodeBase64 } from "jsr:@std/encoding/base64";

interface ClientContextProps {
    value: Map<string, Record<string, unknown>>;
}

export default function ClientContext(props: ClientContextProps) {
    return (
        <>
            <script
                type="application/json"
                id="__CLIENTCONTEXT"
                dangerouslySetInnerHTML={{
                    __html: encodeBase64(
                        JSON.stringify(
                            Object.fromEntries(
                                props.value)))
                }}>
            </script>
        </>
    );
}