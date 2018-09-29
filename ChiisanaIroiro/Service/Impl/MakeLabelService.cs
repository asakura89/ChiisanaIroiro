using System;
using System.Text;

namespace ChiisanaIroiro.Service.Impl {
    public class MakeLabelService : IMakeLabelService {
        String IMakeLabelService.MakeLabel(String labelText) {
            var labelBuilder = new StringBuilder();
            labelBuilder.Append(MakeLabel(labelText + ": begin") + "\r\n");
            labelBuilder.Append(MakeLabel(labelText + ": end"));

            return labelBuilder.ToString();
        }

        String IMakeLabelService.MakeRegionLabel(String labelText) {
            var regionLabelBuilder = new StringBuilder();
            regionLabelBuilder.Append("#region " + MakeRegionLabel(labelText) + "\r\n");
            regionLabelBuilder.Append("#endregion");

            return regionLabelBuilder.ToString();
        }

        String MakeLabel(String labelText) {
            const Int16 MaxFinalLabelLength = 80;
            const Int16 AdditionalCharLength = 8; // NOTE: additionalChar is Opening and Closing Chars + 2 space before and after label
            const String OpeningChar = "/* ";
            const String ClosingChar = " */";

            var labelBuilder = new StringBuilder();
            labelBuilder.Append(OpeningChar);

            Int16 totalLength = (Int16) ((MaxFinalLabelLength - AdditionalCharLength - labelText.Length) / 2);
            for (int i = 0; i < totalLength; i++)
                labelBuilder.Append("=");
            labelBuilder.Append(" ");
            labelBuilder.Append(labelText);
            labelBuilder.Append(" ");
            for (int i = 0; i < totalLength; i++)
                labelBuilder.Append("=");
            labelBuilder.Append(ClosingChar);

            String finalLabel = labelBuilder.ToString();

            return finalLabel;
        }

        String MakeRegionLabel(String labelText) {
            const String OpeningChar = ": ";
            const String ClosingChar = " :";

            var regionLabelBuilder = new StringBuilder();
            regionLabelBuilder.Append(OpeningChar);
            regionLabelBuilder.Append(labelText);
            regionLabelBuilder.Append(ClosingChar);
            String finalLabel = regionLabelBuilder.ToString();

            return finalLabel;
        }
    }
}