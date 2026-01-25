import { h } from "preact";
import { useEffect, useState } from "preact/hooks";

type PriceComponent = { id: string; name: string; amount: number };

function formatIdr(value: number) {
    return new Intl.NumberFormat("id-ID", {
        style: "currency",
        currency: "IDR",
        maximumFractionDigits: 0
    }).format(value);
}

export default function DiscountVoucherIsland() {
    const [basePrice, setBasePrice] = useState(0);
    const [extraComponents, setExtraComponents] = useState<PriceComponent[]>([]);
    const [newExtraName, setNewExtraName] = useState("");
    const [newExtraAmount, setNewExtraAmount] = useState(0);
    const [paymentMethod, setPaymentMethod] = useState("");
    const [deliveryMethod, setDeliveryMethod] = useState("");
    const [vouchers, setVouchers] = useState<any[]>([]);
    const [selectedVoucherIds, setSelectedVoucherIds] = useState<string[]>([]);
    const [result, setResult] = useState<any>(null);

    useEffect(() => {
        fetchVouchers();
    }, []);

    async function fetchVouchers() {
        const res = await fetch("/api/discount/vouchers");
        const data = await res.json();
        setVouchers(data);
    }

    function addExtraComponent() {
        if (!newExtraName) {
            return;
        }
        setExtraComponents(
            (prev) => [...prev, {
                id: crypto.randomUUID(),
                name: newExtraName,
                amount: newExtraAmount
            }]
        );
        setNewExtraName("");
        setNewExtraAmount(0);
    }

    function toggleVoucher(id: string) {
        setSelectedVoucherIds((prev) =>
            prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id]
        );
    }

    async function calculate() {
        const payload = {
            basePrice,
            extraComponents,
            paymentMethod,
            deliveryMethod,
            selectedVoucherIds
        };
        const res = await fetch("/api/discount/calculate", {
            method: "POST",
            headers: {"content-type": "application/json"},
            body: JSON.stringify(payload)
        });
        const data = await res.json();
        setResult(data);
    }

    async function saveHistory() {
        if (!result) {
            return;
        }
        await fetch("/api/discount/history", {
            method: "POST",
            headers: {"content-type": "application/json"},
            body: JSON.stringify(result)
        });
        alert("Saved to history");
    }

    return (
        <div>
            <div
                style={{display: "grid", gridTemplateColumns: "1fr 1fr", gap: "12px"}}
            >
                <div>
                    <label>Base Price</label>
                    <input
                        type="number"
                        value={basePrice}
                        onInput={(e: any) => setBasePrice(Number(e.target.value))}
                    />
                    <h4>Extra Price Components</h4>
                    <div>
                        <input
                            placeholder="name"
                            value={newExtraName}
                            onInput={(e: any) => setNewExtraName(e.target.value)}
                        />
                        <input
                            type="number"
                            value={newExtraAmount}
                            onInput={(e: any) => setNewExtraAmount(Number(e.target.value))}
                        />
                        <button onClick={addExtraComponent}>Add</button>
                    </div>
                    <ul>
                        {extraComponents.map((c) => (
                            <li key={c.id}>{c.name}: {formatIdr(c.amount)}</li>
                        ))}
                    </ul>
                </div>
                <div>
                    <div>
                        <label>Payment Method</label>
                        <select
                            value={paymentMethod}
                            onChange={(e: any) => setPaymentMethod(e.target.value)}
                        >
                            <option value="">--select--</option>
                            <option value="card:BCA">Card - BCA</option>
                            <option value="card:BNI">Card - BNI</option>
                            <option value="card:BSI">Card - BSI</option>
                            <option value="ewallet:OVO">E-Wallet - OVO</option>
                            <option value="ewallet:DANA">E-Wallet - DANA</option>
                            <option value="ewallet:GOPAY">E-Wallet - GOPAY</option>
                            <option value="ewallet:SHOPEEPAY">E-Wallet - ShopeePay</option>
                            <option value="cash">Cash</option>
                        </select>
                    </div>
                    <div>
                        <label>Delivery Method</label>
                        <select
                            value={deliveryMethod}
                            onChange={(e: any) => setDeliveryMethod(e.target.value)}
                        >
                            <option value="">--select--</option>
                            <option value="priority">Priority</option>
                            <option value="saver">Saver</option>
                        </select>
                    </div>
                    <h4>Available Vouchers</h4>
                    <ul>
                        {vouchers.map((v) => (
                            <li key={v.id}>
                                <label>
                                    <input
                                        type="checkbox"
                                        checked={selectedVoucherIds.includes(v.id)}
                                        onChange={() => toggleVoucher(v.id)}
                                    />{" "}
                                    {v.name} - {v.description}
                                </label>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>

            <div style={{marginTop: "16px"}}>
                <button onClick={calculate}>Calculate</button>
                <button onClick={saveHistory} style={{marginLeft: "8px"}}>
                    Save to history
                </button>
            </div>

            {result && (
                <div style={{marginTop: "20px"}}>
                    <h3>Result</h3>
                    <div>Original Total: {formatIdr(result.originalTotal)}</div>
                    <div>
                        Applied Vouchers:
                        <ul>
                            {result.appliedVouchers.map((v: any) => (
                                <li key={v.id}>{v.name}: -{formatIdr(v.discount)}</li>
                            ))}
                        </ul>
                    </div>
                    <div>Fixed Discounts: -{formatIdr(result.fixedDiscount || 0)}</div>
                    <div>
                        <strong>Final Total: {formatIdr(result.finalTotal)}</strong>
                    </div>
                </div>
            )}
        </div>
    );
}
