export class RyandomNumberGenerator {
    static Feigenbaum: number = 46692016;

    /** ~<<
    private static BigMath = {
        abs(bigN: bigint): bigint {
            return bigN < 0n ? -bigN : bigN;
        }
    };
    >>~ */

    static ryandomize(lowerBound: number, upperBound: number): number {
        /** ~<<
        const uuid = crypto.randomUUID();
        const uuidNum = BigInt('0x' + uuid.replace(/-/g, ''));
        const seed: bigint = this.BigMath.abs(
            uuidNum %
            BigInt(RyandomNumberGenerator.Feigenbaum)
        );
        >>~ */

        const randArray: Int32Array<ArrayBuffer> = crypto.getRandomValues(new Int32Array(1));
        const random: number = lowerBound + (randArray[0] % (upperBound - lowerBound + 1));

        return Math.abs(Math.floor(random));
    }

    static ryandomizeSingle(upperBound: number): number {
        return this.ryandomize(0, upperBound);
    }
}


/** ~<< Test

let i: number = 23;
let table: number[] = [];
while (i > 0) {
    table.push({
        single: RyandomNumberGenerator.ryandomizeSingle(4),
        upperLower: RyandomNumberGenerator.ryandomize(0, 4),
        single2: RyandomNumberGenerator.ryandomizeSingle(5),
        upperLower2: RyandomNumberGenerator.ryandomize(0, 5)
    });

    i--;
}

console.table(table);

┌───────┬────────┬────────────┬─────────┬─────────────┐
│ (idx) │ single │ upperLower │ single2 │ upperLOwer2 │
├───────┼────────┼────────────┼─────────┼─────────────┤
│     0 │      4 │          4 │       4 │           0 │
│     1 │      2 │          0 │       2 │           4 │
│     2 │      4 │          2 │       3 │           4 │
│     3 │      2 │          2 │       5 │           5 │
│     4 │      0 │          0 │       3 │           2 │
│     5 │      3 │          2 │       5 │           5 │
│     6 │      4 │          0 │       2 │           4 │
│     7 │      0 │          4 │       5 │           3 │
│     8 │      0 │          0 │       3 │           4 │
│     9 │      3 │          3 │       5 │           0 │
│    10 │      1 │          1 │       1 │           0 │
│    11 │      1 │          2 │       5 │           1 │
│    12 │      4 │          2 │       3 │           0 │
│    13 │      1 │          4 │       2 │           0 │
│    14 │      4 │          0 │       0 │           5 │
│    15 │      3 │          0 │       0 │           4 │
│    16 │      0 │          0 │       5 │           1 │
│    17 │      4 │          4 │       2 │           5 │
│    18 │      2 │          3 │       0 │           2 │
│    19 │      3 │          0 │       0 │           0 │
│    20 │      1 │          1 │       3 │           0 │
│    21 │      1 │          0 │       3 │           5 │
│    22 │      2 │          0 │       3 │           5 │
└───────┴────────┴────────────┴─────────┴─────────────┘

>>~ */

