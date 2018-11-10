using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ChiisanaIroiro.Utility;
using FastColoredTextBoxNS;

namespace ChiisanaIroiro.View {
    partial class OokiView {
        Button btnClipboard;
        Button btnObjectCreate;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        IContainer components = null;

        TableLayoutPanel pnlLayout;
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
            components = new Container();
            txtOutput = new FastColoredTextBox();
            txtInput = new FastColoredTextBox();
            btnObjectCreate = new Button();
            btnClipboard = new Button();
            pnlLayout = new TableLayoutPanel();
            pnlLayout.SuspendLayout();
            SuspendLayout();
            ((ISupportInitialize) txtOutput).BeginInit();
            ((ISupportInitialize) txtInput).BeginInit();
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(3, 152);
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.Size = new Size(309, 67);
            txtOutput.TabIndex = 7;
            TextEditorHelper.Initialize(txtOutput);
            // 
            // txtInput
            // 
            txtInput.Location = new Point(3, 152);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(309, 67);
            txtInput.TabIndex = 5;
            TextEditorHelper.Initialize(txtInput);
            // 
            // btnObjectCreate
            // 
            btnObjectCreate.Dock = DockStyle.Fill;
            btnObjectCreate.Location = new Point(3, 259);
            btnObjectCreate.Name = "btnObjectCreate";
            btnObjectCreate.Size = new Size(844, 39);
            btnObjectCreate.TabIndex = 6;
            btnObjectCreate.Text = "Create Object";
            btnObjectCreate.UseVisualStyleBackColor = true;
            btnObjectCreate.Click += btnObjectCreate_Click;
            // 
            // btnClipboard
            // 
            btnClipboard.Dock = DockStyle.Fill;
            btnClipboard.Location = new Point(3, 530);
            btnClipboard.Name = "btnClipboard";
            btnClipboard.Size = new Size(844, 41);
            btnClipboard.TabIndex = 8;
            btnClipboard.Text = "Copy to Clipboard";
            btnClipboard.UseVisualStyleBackColor = true;
            btnClipboard.Click += btnClipboard_Click;
            // 
            // pnlLayout
            // 
            pnlLayout.ColumnCount = 1;
            pnlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlLayout.Controls.Add(btnClipboard, 0, 3);
            pnlLayout.Controls.Add(txtOutput, 0, 2);
            pnlLayout.Controls.Add(btnObjectCreate, 0, 1);
            pnlLayout.Controls.Add(txtInput, 0, 0);
            pnlLayout.Dock = DockStyle.Fill;
            pnlLayout.Location = new Point(0, 0);
            pnlLayout.Name = "pnlLayout";
            pnlLayout.RowCount = 4;
            pnlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 241F));
            pnlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            pnlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 241F));
            pnlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            pnlLayout.Size = new Size(850, 574);
            pnlLayout.TabIndex = 10;
            // 
            // ObjectCreateView
            // 
            AutoScaleDimensions = new SizeF(7F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlLayout);
            Font = new Font("Trebuchet MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ObjectCreateView";
            Size = new Size(850, 574);
            ((ISupportInitialize) txtOutput).EndInit();
            ((ISupportInitialize) txtInput).EndInit();
            pnlLayout.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}