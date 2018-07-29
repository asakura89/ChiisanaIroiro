using ChiisanaIroiro.Utility;
using FastColoredTextBoxNS;

namespace ChiisanaIroiro.View {
    partial class GenerateNumberView {
        System.Windows.Forms.Button btnClipboard;
        System.Windows.Forms.Button btnGenerate;
        System.Windows.Forms.ComboBox cmbAvailableType;

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
            btnGenerate = new System.Windows.Forms.Button();
            txtInput = TextEditorHelper.Initialize();
            cmbAvailableType = new System.Windows.Forms.ComboBox();
            btnClipboard = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // txtOutput
            // 
            txtOutput.Location = new System.Drawing.Point(3, 152);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new System.Drawing.Size(309, 67);
            txtOutput.TabIndex = 7;
            // 
            // btnChangeCase
            // 
            btnGenerate.Location = new System.Drawing.Point(3, 108);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new System.Drawing.Size(309, 38);
            btnGenerate.TabIndex = 6;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // txtInput
            // 
            txtInput.Location = new System.Drawing.Point(3, 35);
            txtInput.Name = "txtInput";
            txtInput.Size = new System.Drawing.Size(309, 67);
            txtInput.TabIndex = 5;
            // 
            // cmbAvailableCase
            // 
            cmbAvailableType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbAvailableType.FormattingEnabled = true;
            cmbAvailableType.Location = new System.Drawing.Point(3, 3);
            cmbAvailableType.Name = "cmbAvailableType";
            cmbAvailableType.Size = new System.Drawing.Size(309, 26);
            cmbAvailableType.TabIndex = 4;
            cmbAvailableType.SelectedIndexChanged += cmbAvailableTypeOnSelectedIndexChanged;
            // 
            // btnClipboard
            // 
            btnClipboard.Location = new System.Drawing.Point(3, 225);
            btnClipboard.Name = "btnClipboard";
            btnClipboard.Size = new System.Drawing.Size(309, 38);
            btnClipboard.TabIndex = 8;
            btnClipboard.Text = "Copy to Clipboard";
            btnClipboard.UseVisualStyleBackColor = true;
            btnClipboard.Click += btnClipboard_Click;
            // 
            // ChangeCaseView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(btnClipboard);
            Controls.Add(txtOutput);
            Controls.Add(btnGenerate);
            Controls.Add(txtInput);
            Controls.Add(cmbAvailableType);
            Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, 0);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "GenerateNumberView";
            Size = new System.Drawing.Size(315, 300);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}