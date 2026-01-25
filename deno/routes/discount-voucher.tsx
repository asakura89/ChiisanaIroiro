import HtmlHead from "../components/server/HtmlHead.tsx";
import DiscountVoucherIsland from "../islands/DiscountVoucher.tsx";

export default function DiscountVoucherPage() {
    return (
        <>
            <HtmlHead componentName="Discount Voucher Calculator" />
            <div style={{padding: "20px", maxWidth: "900px", margin: "0 auto"}}>
                <h1>Discount Voucher Calculator</h1>
                <DiscountVoucherIsland/>
            </div>
        </>
    );
}
