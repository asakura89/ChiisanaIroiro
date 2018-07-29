using System.Windows.Forms;
using ChiisanaIroiro.View;

namespace ChiisanaIroiro {
    partial class MainForm {
        TabPage changeCaseTab;
        ChangeCaseView changeCaseView;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        TabPage generateNumberTab;
        GenerateNumberView generateNumberView;

        TabPage generateTemplateTab;
        GenerateSqlTemplateView generateTemplateView;
        /*TabPage headerMakerTab;
        SourceHeaderTextView headerMakerView;*/
        TabPage labelMakerTab;
        LabelMakerView labelMakerView;

        TabControl mainTabControl;

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
            mainTabControl = new TabControl();
            changeCaseTab = new TabPage();
            labelMakerTab = new TabPage();
            //headerMakerTab = new TabPage();
            generateTemplateTab = new TabPage();
            generateNumberTab = new TabPage();
            changeCaseView = new ChangeCaseView();
            labelMakerView = new LabelMakerView();
            //headerMakerView = new SourceHeaderTextView();
            generateTemplateView = new GenerateSqlTemplateView();
            generateNumberView = new GenerateNumberView();
            mainTabControl.SuspendLayout();
            changeCaseTab.SuspendLayout();
            labelMakerTab.SuspendLayout();
            //headerMakerTab.SuspendLayout();
            generateTemplateTab.SuspendLayout();
            generateNumberTab.SuspendLayout();
            SuspendLayout();
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(changeCaseTab);
            mainTabControl.Controls.Add(labelMakerTab);
            //mainTabControl.Controls.Add(headerMakerTab);
            mainTabControl.Controls.Add(generateTemplateTab);
            mainTabControl.Controls.Add(generateNumberTab);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new System.Drawing.Point(0, 0);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new System.Drawing.Size(329, 337);
            mainTabControl.TabIndex = 0;
            // 
            // changeCaseTab
            // 
            changeCaseTab.Controls.Add(changeCaseView);
            changeCaseTab.Location = new System.Drawing.Point(4, 27);
            changeCaseTab.Name = "changeCaseTab";
            changeCaseTab.Padding = new Padding(3);
            changeCaseTab.Size = new System.Drawing.Size(321, 306);
            changeCaseTab.TabIndex = 0;
            changeCaseTab.Text = "Text Case";
            changeCaseTab.UseVisualStyleBackColor = true;
            // 
            // labelMakerTab
            // 
            labelMakerTab.Controls.Add(labelMakerView);
            labelMakerTab.Location = new System.Drawing.Point(4, 27);
            labelMakerTab.Name = "labelMakerTab";
            labelMakerTab.Padding = new Padding(3);
            labelMakerTab.Size = new System.Drawing.Size(321, 306);
            labelMakerTab.TabIndex = 1;
            labelMakerTab.Text = "Label Maker";
            labelMakerTab.UseVisualStyleBackColor = true;
            // 
            // headerMakerTab
            // 
            /*headerMakerTab.Controls.Add(headerMakerView);
            headerMakerTab.Location = new System.Drawing.Point(4, 27);
            headerMakerTab.Name = "headerMakerTab";
            headerMakerTab.Padding = new Padding(3);
            headerMakerTab.Size = new System.Drawing.Size(321, 306);
            headerMakerTab.TabIndex = 2;
            headerMakerTab.Text = "Header Maker";
            headerMakerTab.UseVisualStyleBackColor = true;*/
            // 
            // generateTemplateTab
            // 
            generateTemplateTab.Controls.Add(generateTemplateView);
            generateTemplateTab.Location = new System.Drawing.Point(4, 27);
            generateTemplateTab.Name = "generateTemplateTab";
            generateTemplateTab.Padding = new Padding(3);
            generateTemplateTab.Size = new System.Drawing.Size(321, 306);
            generateTemplateTab.TabIndex = 2;
            generateTemplateTab.Text = "Generate Sql Template";
            generateTemplateTab.UseVisualStyleBackColor = true;
            // 
            // generateNumberTab
            // 
            generateNumberTab.Controls.Add(generateNumberView);
            generateNumberTab.Location = new System.Drawing.Point(4, 27);
            generateNumberTab.Name = "generateNumberTab";
            generateNumberTab.Padding = new Padding(3);
            generateNumberTab.Size = new System.Drawing.Size(321, 306);
            generateNumberTab.TabIndex = 2;
            generateNumberTab.Text = "Generate Number";
            generateNumberTab.UseVisualStyleBackColor = true;
            // 
            // changeCaseView
            // 
            changeCaseView.BackColor = System.Drawing.Color.Transparent;
            changeCaseView.Dock = DockStyle.Fill;
            changeCaseView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, 0);
            changeCaseView.Location = new System.Drawing.Point(3, 3);
            changeCaseView.Margin = new Padding(3, 4, 3, 4);
            changeCaseView.Name = "changeCaseView";
            changeCaseView.InputString = "";
            changeCaseView.Size = new System.Drawing.Size(315, 300);
            changeCaseView.TabIndex = 0;
            // 
            // labelMakerView
            // 
            labelMakerView.BackColor = System.Drawing.Color.Transparent;
            labelMakerView.Dock = DockStyle.Fill;
            labelMakerView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, 0);
            labelMakerView.Location = new System.Drawing.Point(3, 3);
            labelMakerView.Margin = new Padding(3, 4, 3, 4);
            labelMakerView.Name = "labelMakerView";
            labelMakerView.InputString = "";
            labelMakerView.Size = new System.Drawing.Size(315, 305);
            labelMakerView.TabIndex = 0;
            // 
            // headerMakerView
            // 
            /*headerMakerView.BackColor = System.Drawing.Color.Transparent;
            headerMakerView.Dock = DockStyle.Fill;
            headerMakerView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, 0);
            headerMakerView.Location = new System.Drawing.Point(3, 3);
            headerMakerView.Margin = new Padding(3, 4, 3, 4);
            headerMakerView.Name = "headerMakerView";
            headerMakerView.InputString = "";
            headerMakerView.Size = new System.Drawing.Size(315, 305);
            headerMakerView.TabIndex = 0;*/
            // 
            // generateTemplateView
            // 
            generateTemplateView.BackColor = System.Drawing.Color.Transparent;
            generateTemplateView.Dock = DockStyle.Fill;
            generateTemplateView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, 0);
            generateTemplateView.Location = new System.Drawing.Point(3, 3);
            generateTemplateView.Margin = new Padding(3, 4, 3, 4);
            generateTemplateView.Name = "generateTemplateView";
            generateTemplateView.Size = new System.Drawing.Size(315, 305);
            generateTemplateView.TabIndex = 0;
            // 
            // generateNumberView
            // 
            generateNumberView.BackColor = System.Drawing.Color.Transparent;
            generateNumberView.Dock = DockStyle.Fill;
            generateNumberView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, 0);
            generateNumberView.Location = new System.Drawing.Point(3, 3);
            generateNumberView.Margin = new Padding(3, 4, 3, 4);
            generateNumberView.Name = "generateNumberView";
            generateNumberView.Size = new System.Drawing.Size(315, 305);
            generateNumberView.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(329, 337);
            Controls.Add(mainTabControl);
            Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ちいさな色々";
            mainTabControl.ResumeLayout(false);
            changeCaseTab.ResumeLayout(false);
            labelMakerTab.ResumeLayout(false);
            //headerMakerTab.ResumeLayout(false);
            generateTemplateTab.ResumeLayout(false);
            generateNumberTab.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}