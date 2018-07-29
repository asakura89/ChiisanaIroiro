using ChiisanaIroiro.Utility;
using FastColoredTextBoxNS;

namespace ChiisanaIroiro.View {
    partial class LabelMakerView {
        System.Windows.Forms.Button btnClipboard;
        System.Windows.Forms.Button btnMakeLabel;
        System.Windows.Forms.ComboBox cmbLabelType;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        FastColoredTextBox txtInput;

        FastColoredTextBox txtOutput;

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent() {
            txtOutput = TextEditorHelper.Initialize();
            btnMakeLabel = new System.Windows.Forms.Button();
            txtInput = TextEditorHelper.Initialize();
            cmbLabelType = new System.Windows.Forms.ComboBox();
            btnClipboard = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // txtOutput
            // 
            txtOutput.Location = new System.Drawing.Point(3, 152);
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.Size = new System.Drawing.Size(309, 67);
            txtOutput.TabIndex = 7;
            // 
            // btnMakeLabel
            // 
            btnMakeLabel.Location = new System.Drawing.Point(3, 108);
            btnMakeLabel.Name = "btnMakeLabel";
            btnMakeLabel.Size = new System.Drawing.Size(309, 38);
            btnMakeLabel.TabIndex = 6;
            btnMakeLabel.Text = "Make Label";
            btnMakeLabel.UseVisualStyleBackColor = true;
            btnMakeLabel.Click += btnMakeLabel_Click;
            // 
            // txtInput
            // 
            txtInput.Location = new System.Drawing.Point(3, 35);
            txtInput.Name = "txtInput";
            txtInput.Size = new System.Drawing.Size(309, 67);
            txtInput.TabIndex = 5;
            // 
            // cmbLabelType
            // 
            cmbLabelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbLabelType.FormattingEnabled = true;
            cmbLabelType.Location = new System.Drawing.Point(3, 3);
            cmbLabelType.Name = "cmbAvailableCase";
            cmbLabelType.Size = new System.Drawing.Size(309, 26);
            cmbLabelType.TabIndex = 4;
            // 
            // btnClipboard
            // 
            btnClipboard.Location = new System.Drawing.Point(3, 225);
            btnClipboard.Name = "btnClipboard";
            btnClipboard.Size = new System.Drawing.Size(309, 38);
            btnClipboard.TabIndex = 9;
            btnClipboard.Text = "Copy to Clipboard";
            btnClipboard.UseVisualStyleBackColor = true;
            btnClipboard.Click += btnClipboard_Click;
            // 
            // LabelMakerView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(btnClipboard);
            Controls.Add(txtOutput);
            Controls.Add(btnMakeLabel);
            Controls.Add(txtInput);
            Controls.Add(cmbLabelType);
            Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, 0);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "LabelMakerView";
            Size = new System.Drawing.Size(315, 300);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}