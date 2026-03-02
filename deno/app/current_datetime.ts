import {Wielder} from "../ayumi/keywielder/wielder.ts";

export function generateCurrentDateTimeString(): string {
    const currentDateTime =
        Wielder.new()
            .addLongYear()
            .addLeftPadded(
                Wielder.new()
                    .addNumericMonth()
                    .buildKey(),
                2, "0")
            .addLeftPadded(
                Wielder.new()
                    .addDate()
                    .buildKey(),
                2, "0")
            .addLeftPadded(
                new Date().getHours().toString(),
                2, "0")
            .addLeftPadded(
                new Date().getMinutes().toString(),
                2, "0")
            .addLeftPadded(
                new Date().getSeconds().toString(),
                2, "0")
            .buildKey();

    return currentDateTime;
}

export function generateCurrentDateString(): string {
    const currentDate =
        Wielder.new()
            .addLongYear()
            .addLeftPadded(
                Wielder.new()
                    .addNumericMonth()
                    .buildKey(),
                2, "0")
            .addLeftPadded(
                Wielder.new()
                    .addDate()
                    .buildKey(),
                2, "0")
            .buildKey();

    return currentDate;
}