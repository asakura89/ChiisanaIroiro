using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ChiisanaIroiro
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        readonly IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) 
                components.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            this.cmbAvailableFeatures = new System.Windows.Forms.ComboBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // cmbAvailableFeatures
            // 
            this.cmbAvailableFeatures.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbAvailableFeatures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAvailableFeatures.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbAvailableFeatures.FormattingEnabled = true;
            this.cmbAvailableFeatures.Location = new System.Drawing.Point(0, 0);
            this.cmbAvailableFeatures.Name = "cmbAvailableFeatures";
            this.cmbAvailableFeatures.Size = new System.Drawing.Size(850, 26);
            this.cmbAvailableFeatures.TabIndex = 0;
            this.cmbAvailableFeatures.SelectedIndexChanged += new System.EventHandler(this.cmbFeature_SelectedIndexChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 26);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(850, 574);
            this.pnlMain.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 600);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.cmbAvailableFeatures);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(329, 337);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ちいさな色々";
            this.ResumeLayout(false);

        }

        ComboBox cmbAvailableFeatures;
        Panel pnlMain;
    }
}

