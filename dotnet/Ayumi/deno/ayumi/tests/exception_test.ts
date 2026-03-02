import { assert, assertEquals, assertThrows } from "jsr:@std/assert";
import { describe, it } from "jsr:@std/testing/bdd";

import { getType } from "../kayo-core/core.ts";

import ArgumentNullException from "../kayo-core/argument_null_exc.ts";
import ArgumentOutOfRangeException from "../kayo-core/argument_out_of_range_exc.ts";
import BadConfigurationException from "../kayo-core/bad_config_exc.ts";
import InvalidOperationException from "../kayo-core/invalid_operation_exc.ts";

interface ExceptionEntry {
    Class: new (message?: string) => Error;
    name: string;
}

const entries: ExceptionEntry[] = [
    { Class: ArgumentNullException, name: "ArgumentNullException" },
    { Class: ArgumentOutOfRangeException, name: "ArgumentOutOfRangeException" },
    { Class: BadConfigurationException, name: "BadConfigurationException" },
    { Class: InvalidOperationException, name: "InvalidOperationException" },
];

for (const { Class, name } of entries) {
    Deno.test(`${name} default message`, (): void => {
        const exc = new Class();
        assertEquals(exc.name, name);
        assertEquals(exc.message, name);
    });

    Deno.test(`${name} custom message`, (): void => {
        const customMsg = "custom message";
        const exc = new Class(customMsg);
        assertEquals(exc.name, name);
        assertEquals(exc.message, customMsg);
    });
}

describe("Argument Null Exception", () => {
    it("Should throw with a message", () => {
        assertThrows(
            () => {
                throw new ArgumentNullException("Invalid value");
            },
            ArgumentNullException,
            "Invalid value"
        );
    });

    it("Should throw with default message", () => {
        assertThrows(
            () => {
                throw new ArgumentNullException();
            },
            ArgumentNullException,
            "ArgumentNullException"
        );
    });

    it("Should satisfy custom checking", () => {
        try {
            const e = new ArgumentNullException("Invalid value");
            throw e;
        }
        catch(error: any) {
            assert(error.name === "ArgumentNullException");
            assert(error instanceof ArgumentNullException);
            assert(error instanceof Error);
            assert(typeof error === "object");
            console.log(JSON.stringify(error));

            assert(error.toString() === "ArgumentNullException: Invalid value");
            assert(getType(error) === "Error");
        }
    });
});