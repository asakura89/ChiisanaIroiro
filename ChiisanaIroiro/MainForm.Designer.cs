using ChiisanaIroiro.View;

namespace ChiisanaIroiro
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.changeCaseTab = new System.Windows.Forms.TabPage();
            this.labelMakerTab = new System.Windows.Forms.TabPage();
            this.headerMakerTab = new System.Windows.Forms.TabPage();
            this.changeCaseView = new ChangeCaseView();
            this.labelMakerView = new LabelMakerView();
            this.headerMakerView = new SourceHeaderTextView();
            this.mainTabControl.SuspendLayout();
            this.changeCaseTab.SuspendLayout();
            this.labelMakerTab.SuspendLayout();
            this.headerMakerTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.changeCaseTab);
            this.mainTabControl.Controls.Add(this.labelMakerTab);
            this.mainTabControl.Controls.Add(this.headerMakerTab);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(329, 337);
            this.mainTabControl.TabIndex = 0;
            // 
            // changeCaseTab
            // 
            this.changeCaseTab.Controls.Add(this.changeCaseView);
            this.changeCaseTab.Location = new System.Drawing.Point(4, 27);
            this.changeCaseTab.Name = "changeCaseTab";
            this.changeCaseTab.Padding = new System.Windows.Forms.Padding(3);
            this.changeCaseTab.Size = new System.Drawing.Size(321, 306);
            this.changeCaseTab.TabIndex = 0;
            this.changeCaseTab.Text = "Text Case";
            this.changeCaseTab.UseVisualStyleBackColor = true;
            // 
            // labelMakerTab
            // 
            this.labelMakerTab.Controls.Add(this.labelMakerView);
            this.labelMakerTab.Location = new System.Drawing.Point(4, 27);
            this.labelMakerTab.Name = "labelMakerTab";
            this.labelMakerTab.Padding = new System.Windows.Forms.Padding(3);
            this.labelMakerTab.Size = new System.Drawing.Size(321, 306);
            this.labelMakerTab.TabIndex = 1;
            this.labelMakerTab.Text = "Label Maker";
            this.labelMakerTab.UseVisualStyleBackColor = true;
            // 
            // headerMakerTab
            // 
            this.headerMakerTab.Controls.Add(this.headerMakerView);
            this.headerMakerTab.Location = new System.Drawing.Point(4, 27);
            this.headerMakerTab.Name = "headerMakerTab";
            this.headerMakerTab.Padding = new System.Windows.Forms.Padding(3);
            this.headerMakerTab.Size = new System.Drawing.Size(321, 306);
            this.headerMakerTab.TabIndex = 2;
            this.headerMakerTab.Text = "Header Maker";
            this.headerMakerTab.UseVisualStyleBackColor = true;
            // 
            // changeCaseView
            // 
            this.changeCaseView.BackColor = System.Drawing.Color.Transparent;
            this.changeCaseView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeCaseView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeCaseView.Location = new System.Drawing.Point(3, 3);
            this.changeCaseView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.changeCaseView.Name = "changeCaseView";
            this.changeCaseView.InputString = "";
            this.changeCaseView.Size = new System.Drawing.Size(315, 300);
            this.changeCaseView.TabIndex = 0;
            // 
            // labelMakerView
            // 
            this.labelMakerView.BackColor = System.Drawing.Color.Transparent;
            this.labelMakerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMakerView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMakerView.Location = new System.Drawing.Point(3, 3);
            this.labelMakerView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelMakerView.Name = "labelMakerView";
            this.labelMakerView.InputString = "";
            this.labelMakerView.Size = new System.Drawing.Size(315, 305);
            this.labelMakerView.TabIndex = 0;
            // 
            // headerMakerView
            // 
            this.headerMakerView.BackColor = System.Drawing.Color.Transparent;
            this.headerMakerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerMakerView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerMakerView.Location = new System.Drawing.Point(3, 3);
            this.headerMakerView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.headerMakerView.Name = "headerMakerView";
            this.headerMakerView.InputString = "";
            this.headerMakerView.Size = new System.Drawing.Size(315, 305);
            this.headerMakerView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 337);
            this.Controls.Add(this.mainTabControl);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ちいさな色々";
            this.mainTabControl.ResumeLayout(false);
            this.changeCaseTab.ResumeLayout(false);
            this.labelMakerTab.ResumeLayout(false);
            this.headerMakerTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage changeCaseTab;
        private System.Windows.Forms.TabPage labelMakerTab;
        private System.Windows.Forms.TabPage headerMakerTab;
        private ChangeCaseView changeCaseView;
        private LabelMakerView labelMakerView;
        private SourceHeaderTextView headerMakerView;
    }
}

