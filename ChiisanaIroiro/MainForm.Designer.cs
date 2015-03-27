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
            this.textCaseTab = new System.Windows.Forms.TabPage();
            this.changeCaseView = new ChiisanaIroiro.View.ChangeCaseView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.mainTabControl.SuspendLayout();
            this.textCaseTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.textCaseTab);
            this.mainTabControl.Controls.Add(this.tabPage2);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(329, 337);
            this.mainTabControl.TabIndex = 0;
            // 
            // textCaseTab
            // 
            this.textCaseTab.Controls.Add(this.changeCaseView);
            this.textCaseTab.Location = new System.Drawing.Point(4, 27);
            this.textCaseTab.Name = "textCaseTab";
            this.textCaseTab.Padding = new System.Windows.Forms.Padding(3);
            this.textCaseTab.Size = new System.Drawing.Size(321, 306);
            this.textCaseTab.TabIndex = 0;
            this.textCaseTab.Text = "Text Case";
            this.textCaseTab.UseVisualStyleBackColor = true;
            // 
            // changeCaseView
            // 
            this.changeCaseView.BackColor = System.Drawing.Color.Transparent;
            this.changeCaseView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeCaseView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeCaseView.Location = new System.Drawing.Point(3, 3);
            this.changeCaseView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.changeCaseView.Name = "changeCaseView";
            this.changeCaseView.ProcessedString = "";
            this.changeCaseView.Size = new System.Drawing.Size(315, 300);
            this.changeCaseView.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(321, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 337);
            this.Controls.Add(this.mainTabControl);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ちいさな色々";
            this.mainTabControl.ResumeLayout(false);
            this.textCaseTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage textCaseTab;
        private System.Windows.Forms.TabPage tabPage2;
        private View.ChangeCaseView changeCaseView;
    }
}

