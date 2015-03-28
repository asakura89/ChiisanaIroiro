namespace ChiisanaIroiro.View
{
    partial class ChangeCaseView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnChangeCase = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.cmbAvailableCase = new System.Windows.Forms.ComboBox();
            this.btnClipboard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTextCaseOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(3, 152);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(309, 67);
            this.txtOutput.TabIndex = 7;
            // 
            // btnChangeCase
            // 
            this.btnChangeCase.Location = new System.Drawing.Point(3, 108);
            this.btnChangeCase.Name = "btnChangeCase";
            this.btnChangeCase.Size = new System.Drawing.Size(309, 38);
            this.btnChangeCase.TabIndex = 6;
            this.btnChangeCase.Text = "Change Case";
            this.btnChangeCase.UseVisualStyleBackColor = true;
            this.btnChangeCase.Click += new System.EventHandler(this.btnChangeCase_Click);
            // 
            // txtTextCaseInput
            // 
            this.txtInput.Location = new System.Drawing.Point(3, 35);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(309, 67);
            this.txtInput.TabIndex = 5;
            // 
            // cmbAvailableCase
            // 
            this.cmbAvailableCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAvailableCase.FormattingEnabled = true;
            this.cmbAvailableCase.Location = new System.Drawing.Point(3, 3);
            this.cmbAvailableCase.Name = "cmbAvailableCase";
            this.cmbAvailableCase.Size = new System.Drawing.Size(309, 26);
            this.cmbAvailableCase.TabIndex = 4;
            // 
            // btnClipboard
            // 
            this.btnClipboard.Location = new System.Drawing.Point(3, 225);
            this.btnClipboard.Name = "btnClipboard";
            this.btnClipboard.Size = new System.Drawing.Size(309, 38);
            this.btnClipboard.TabIndex = 8;
            this.btnClipboard.Text = "Copy to Clipboard";
            this.btnClipboard.UseVisualStyleBackColor = true;
            this.btnClipboard.Click += new System.EventHandler(this.btnClipboard_Click);
            // 
            // ChangeCaseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnClipboard);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnChangeCase);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.cmbAvailableCase);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChangeCaseView";
            this.Size = new System.Drawing.Size(315, 300);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnChangeCase;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ComboBox cmbAvailableCase;
        private System.Windows.Forms.Button btnClipboard;
    }
}
