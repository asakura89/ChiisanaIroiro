import { RyandomNumberGenerator } from "../ryang/ryandom_number_generator.ts";

export enum AlphaType {
    UpperLowerNumericSymbol,
    UpperLowerNumeric,
    UpperLowerSymbol,
    UpperLower,
    UpperNumeric,
    UpperSymbol,
    Upper,
    LowerNumeric,
    LowerSymbol,
    Lower,
    NumericSymbol,
    Numeric,
    Symbol,
}

const UppercaseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
const LowercaseAlphabet = "abcdefghijklmnopqrstuvwxyz";
const Numeric = "0123456789";
const Symbol = "~!@#$%^&*_\-+=`|\\(){}[]:;<>,.?/";

const alphaTypeDict: Record<AlphaType, string[]> = {
    [AlphaType.UpperLowerNumericSymbol]: `${UppercaseAlphabet} ${LowercaseAlphabet} ${Numeric} ${Symbol}`.split(""),
    [AlphaType.UpperLowerNumeric]: `${UppercaseAlphabet} ${LowercaseAlphabet} ${Numeric}`.split(""),
    [AlphaType.UpperLowerSymbol]: `${UppercaseAlphabet} ${LowercaseAlphabet} ${Symbol}`.split(""),
    [AlphaType.UpperLower]: `${UppercaseAlphabet} ${LowercaseAlphabet}`.split(""),
    [AlphaType.UpperNumeric]: `${UppercaseAlphabet} ${Numeric}`.split(""),
    [AlphaType.UpperSymbol]: `${UppercaseAlphabet} ${Symbol}`.split(""),
    [AlphaType.Upper]: UppercaseAlphabet.split(""),
    [AlphaType.LowerNumeric]: `${LowercaseAlphabet} ${Numeric}`.split(""),
    [AlphaType.LowerSymbol]: `${LowercaseAlphabet} ${Symbol}`.split(""),
    [AlphaType.Lower]: LowercaseAlphabet.split(""),
    [AlphaType.NumericSymbol]: `${Numeric} ${Symbol}`.split(""),
    [AlphaType.Numeric]: Numeric.split(""),
    [AlphaType.Symbol]: Symbol.split(""),
};

export class Wielder {
    private keyBuilder: string[] = [];

    private constructor() {}

    static new(): Wielder {
        return new Wielder();
    }

    addRandomString(valueLength: number, type: AlphaType = AlphaType.Upper): this {
        const charCombination = alphaTypeDict[type];
        for (let i = 0; i < valueLength; i++) {
            const randomIdx = RyandomNumberGenerator.ryandomizeSingle(charCombination.length);
            this.keyBuilder.push(charCombination[randomIdx]);
        }

        return this;
    }

    addRandomNumber(valueLength: number): this {
        return this.addRandomString(valueLength, AlphaType.Numeric);
    }

    addRandomAlphaNumeric(valueLength: number, uppercase = true): this {
        return uppercase ?
            this.addRandomString(valueLength, AlphaType.Upper) :
            this.addRandomString(valueLength, AlphaType.Lower);
    }

    addGuidString(): this {
        this.keyBuilder.push(crypto.randomUUID().replace(/-/g, ""));

        return this;
    }

    addString(value: string, valueLength: number = value.length): this {
        this.keyBuilder.push(value.slice(0, valueLength).toUpperCase());

        return this;
    }

    addRightPadded(value: string, valueLength: number, paddedBy: string = " "): this {
        const resultString = value.padEnd(valueLength, paddedBy).slice(0, valueLength);
        this.keyBuilder.push(resultString);

        return this;
    }

    addLeftPadded(value: string, valueLength: number, paddedBy: string = " "): this {
        const resultString = value.padStart(valueLength, paddedBy).slice(0, valueLength);
        this.keyBuilder.push(resultString);

        return this;
    }

    addShortYear(): this {
        return this.addYear(2);
    }

    addLongYear(): this {
        return this.addYear(4);
    }

    private addYear(valueLength: number): this {
        const currentYear = new Date().getFullYear().toString();
        const yearWithLength = valueLength === 4 ?
            currentYear :
            currentYear.slice(-2);
        this.keyBuilder.push(yearWithLength);

        return this;
    }

    addShortMonth(): this {
        return this.addMonth(3);
    }

    addLongMonth(): this {
        return this.addMonth(4);
    }

    addNumericMonth(): this {
        return this.addMonth(2);
    }

    private addMonth(valueLength: number): this {
        const month = new Date().toLocaleString("default", {
            month: valueLength === 3 ?
                "short" :
                "long"
        });
        this.keyBuilder.push(month);

        return this;
    }

    addDate(valueLength: number = 2): this {
        const day = new Date().getDate().toString().padStart(valueLength, "0");
        this.keyBuilder.push(day);

        return this;
    }

    addCounter(currentCounter: number, increment: number = 1): this {
        this.keyBuilder.push((currentCounter + increment).toString());

        return this;
    }

    buildKey(): string {
        return this.keyBuilder.join("");
    }
}


/** ~<< Example usage

const wielder = Wielder.new()
    .addRandomString(5, AlphaType.UpperLowerNumeric)
    .addGuidString()
    .addShortYear()
    .buildKey();

console.log(wielder);

>>~ */

