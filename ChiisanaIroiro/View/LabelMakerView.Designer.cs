namespace ChiisanaIroiro.View
{
    partial class LabelMakerView
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
            this.btnMakeLabel = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(3, 152);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(309, 67);
            this.txtOutput.TabIndex = 7;
            // 
            // btnMakeLabel
            // 
            this.btnMakeLabel.Location = new System.Drawing.Point(3, 108);
            this.btnMakeLabel.Name = "btnMakeLabel";
            this.btnMakeLabel.Size = new System.Drawing.Size(309, 38);
            this.btnMakeLabel.TabIndex = 6;
            this.btnMakeLabel.Text = "Make Label";
            this.btnMakeLabel.UseVisualStyleBackColor = true;
            this.btnMakeLabel.Click += new System.EventHandler(this.btnMakeLabel_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(3, 35);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(309, 67);
            this.txtInput.TabIndex = 5;
            // 
            // LabelMakerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnMakeLabel);
            this.Controls.Add(this.txtInput);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LabelMakerView";
            this.Size = new System.Drawing.Size(315, 300);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnMakeLabel;
        private System.Windows.Forms.TextBox txtInput;
    }
}
