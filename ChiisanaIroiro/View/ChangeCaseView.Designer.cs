using FastColoredTextBoxNS;

namespace ChiisanaIroiro.View
{
    partial class ChangeCaseView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent() {
            txtOutput = TextEditorHelper.Initialize();
            btnChangeCase = new System.Windows.Forms.Button();
            txtInput = TextEditorHelper.Initialize();
            cmbAvailableCase = new System.Windows.Forms.ComboBox();
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
            // btnChangeCase
            // 
            btnChangeCase.Location = new System.Drawing.Point(3, 108);
            btnChangeCase.Name = "btnChangeCase";
            btnChangeCase.Size = new System.Drawing.Size(309, 38);
            btnChangeCase.TabIndex = 6;
            btnChangeCase.Text = "Change Case";
            btnChangeCase.UseVisualStyleBackColor = true;
            btnChangeCase.Click += new System.EventHandler(btnChangeCase_Click);
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
            cmbAvailableCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbAvailableCase.FormattingEnabled = true;
            cmbAvailableCase.Location = new System.Drawing.Point(3, 3);
            cmbAvailableCase.Name = "cmbAvailableCase";
            cmbAvailableCase.Size = new System.Drawing.Size(309, 26);
            cmbAvailableCase.TabIndex = 4;
            // 
            // btnClipboard
            // 
            btnClipboard.Location = new System.Drawing.Point(3, 225);
            btnClipboard.Name = "btnClipboard";
            btnClipboard.Size = new System.Drawing.Size(309, 38);
            btnClipboard.TabIndex = 8;
            btnClipboard.Text = "Copy to Clipboard";
            btnClipboard.UseVisualStyleBackColor = true;
            btnClipboard.Click += new System.EventHandler(btnClipboard_Click);
            // 
            // ChangeCaseView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(btnClipboard);
            Controls.Add(txtOutput);
            Controls.Add(btnChangeCase);
            Controls.Add(txtInput);
            Controls.Add(cmbAvailableCase);
            Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "ChangeCaseView";
            Size = new System.Drawing.Size(315, 300);
            ResumeLayout(false);
            PerformLayout();

        }

        FastColoredTextBox txtOutput;
        System.Windows.Forms.Button btnChangeCase;
        FastColoredTextBox txtInput;
        System.Windows.Forms.ComboBox cmbAvailableCase;
        System.Windows.Forms.Button btnClipboard;
    }
}
