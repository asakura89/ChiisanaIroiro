namespace ChiisanaIroiro.View
{
    partial class SourceHeaderTextView
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
        void InitializeComponent()
        {
            txtOutput = new System.Windows.Forms.TextBox();
            btnMakeHeader = new System.Windows.Forms.Button();
            txtInput = new System.Windows.Forms.TextBox();
            btnClipboard = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // txtOutput
            // 
            txtOutput.Location = new System.Drawing.Point(3, 152);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.Size = new System.Drawing.Size(309, 67);
            txtOutput.TabIndex = 7;
            // 
            // btnMakeHeader
            // 
            btnMakeHeader.Location = new System.Drawing.Point(3, 108);
            btnMakeHeader.Name = "btnMakeHeader";
            btnMakeHeader.Size = new System.Drawing.Size(309, 38);
            btnMakeHeader.TabIndex = 6;
            btnMakeHeader.Text = "Make Label";
            btnMakeHeader.UseVisualStyleBackColor = true;
            btnMakeHeader.Click += new System.EventHandler(btnMakeHeader_Click);
            // 
            // txtInput
            // 
            txtInput.Location = new System.Drawing.Point(3, 35);
            txtInput.Multiline = true;
            txtInput.Name = "txtInput";
            txtInput.Size = new System.Drawing.Size(309, 67);
            txtInput.TabIndex = 5;
            // 
            // btnClipboard
            // 
            btnClipboard.Location = new System.Drawing.Point(3, 225);
            btnClipboard.Name = "btnClipboard";
            btnClipboard.Size = new System.Drawing.Size(309, 38);
            btnClipboard.TabIndex = 9;
            btnClipboard.Text = "Copy to Clipboard";
            btnClipboard.UseVisualStyleBackColor = true;
            btnClipboard.Click += new System.EventHandler(btnClipboard_Click);
            // 
            // SourceHeaderTextView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(btnClipboard);
            Controls.Add(txtOutput);
            Controls.Add(btnMakeHeader);
            Controls.Add(txtInput);
            Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "SourceHeaderTextView";
            Size = new System.Drawing.Size(315, 300);
            ResumeLayout(false);
            PerformLayout();
        }

        System.Windows.Forms.TextBox txtOutput;
        System.Windows.Forms.Button btnMakeHeader;
        System.Windows.Forms.TextBox txtInput;
        System.Windows.Forms.Button btnClipboard;
    }
}
