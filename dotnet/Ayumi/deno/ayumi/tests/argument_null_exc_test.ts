import { assert, assertThrows } from "@std/assert";
import { describe, it } from "@std/testing/bdd";
import { getType } from "../kayo-core/core.ts";
import ArgumentNullException from "../kayo-core/argument_null_exc.ts";

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