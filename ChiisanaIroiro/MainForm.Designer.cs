using System.ComponentModel;
using System.Windows.Forms;

namespace ChiisanaIroiro {
    partial class MainForm {
        /// <summary>
        ///     Required designer variable.
        /// </summary>
        readonly IContainer components = null;

        ComboBox cmbAvailableFeatures;
        Panel pnlMain;

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
            cmbAvailableFeatures = new ComboBox();
            pnlMain = new Panel();
            SuspendLayout();
            // 
            // cmbAvailableFeatures
            // 
            cmbAvailableFeatures.Dock = DockStyle.Top;
            cmbAvailableFeatures.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAvailableFeatures.FlatStyle = FlatStyle.System;
            cmbAvailableFeatures.FormattingEnabled = true;
            cmbAvailableFeatures.Location = new System.Drawing.Point(0, 0);
            cmbAvailableFeatures.Name = "cmbAvailableFeatures";
            cmbAvailableFeatures.Size = new System.Drawing.Size(850, 26);
            cmbAvailableFeatures.TabIndex = 0;
            cmbAvailableFeatures.SelectedIndexChanged += cmbFeature_SelectedIndexChanged;
            // 
            // pnlMain
            // 
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 26);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new System.Drawing.Size(850, 574);
            pnlMain.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(850, 600);
            Controls.Add(pnlMain);
            Controls.Add(cmbAvailableFeatures);
            Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimumSize = new System.Drawing.Size(329, 337);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ちいさな色々";
            ResumeLayout(false);
        }
    }
}