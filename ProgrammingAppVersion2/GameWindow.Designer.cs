using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProgrammingApp
{
    partial class GameWindow
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox gridPictureBox;

        private System.Windows.Forms.ComboBox comboBoxOptions;
        private System.Windows.Forms.ComboBox comboBoxSecondaryOptions;
        private System.Windows.Forms.ComboBox comboBoxLevelOptions;

        private System.Windows.Forms.Label instructionsLabel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Label commandBoxLabel;
        private System.Windows.Forms.Label outputBoxLabel;

        private System.Windows.Forms.TextBox textBoxFilePath;

        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button metricsButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.components = new System.ComponentModel.Container();

            this.instructionsLabel = new System.Windows.Forms.Label();
            this.comboBoxOptions = new System.Windows.Forms.ComboBox();
            this.comboBoxSecondaryOptions = new System.Windows.Forms.ComboBox();
            this.comboBoxLevelOptions = new System.Windows.Forms.ComboBox();
            this.outputLabel = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.commandBoxLabel = new System.Windows.Forms.Label();
            this.outputBoxLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.metricsButton = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // PictureBox for Grid, Grid label nog weghalen
            this.gridPictureBox = new System.Windows.Forms.PictureBox();
            this.gridPictureBox.Location = new System.Drawing.Point(600, 100);
            this.gridPictureBox.Size = new System.Drawing.Size(450, 450); // Adjust size as needed
            this.gridPictureBox.BorderStyle = BorderStyle.FixedSingle;

            // instructionsLabel
            this.instructionsLabel.AutoSize = true;
            this.instructionsLabel.Location = new System.Drawing.Point(10, 70);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Text = "I want to:";

            // comboBoxOptions
            this.comboBoxOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOptions.Location = new System.Drawing.Point(10, 100);
            this.comboBoxOptions.Name = "comboBoxOptions";
            this.comboBoxOptions.Size = new System.Drawing.Size(200, 21);
            this.comboBoxOptions.SelectedIndexChanged += new System.EventHandler(this.comboBoxOptionsChoice);

            // comboBoxSecondaryOptions
            this.comboBoxSecondaryOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSecondaryOptions.Location = new System.Drawing.Point(10, 160);
            this.comboBoxSecondaryOptions.Name = "comboBoxSecondaryOptions";
            this.comboBoxSecondaryOptions.Size = new System.Drawing.Size(200, 21);
            this.comboBoxSecondaryOptions.Visible = false;
            this.comboBoxSecondaryOptions.SelectedIndexChanged += new System.EventHandler(this.comboBoxSecondaryOptionsChoice);

            // comboBoxLevelOptions
            this.comboBoxLevelOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelOptions.Location = new System.Drawing.Point(10, 210);
            this.comboBoxLevelOptions.Size = new System.Drawing.Size(200, 21);
            this.comboBoxLevelOptions.Visible = false;
            this.comboBoxLevelOptions.SelectedIndexChanged += new System.EventHandler(this.comboBoxLevelOptionsChoice);

            // textBoxFilePath
            this.textBoxFilePath.Location = new System.Drawing.Point(10, 160);
            this.textBoxFilePath.Size = new System.Drawing.Size(200, 20);
            this.textBoxFilePath.Visible = false;

            // outputLabel
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(10, 135);
            this.outputLabel.Name = "outputLabel";

            // commandBoxLabel
            this.commandBoxLabel.Location = new System.Drawing.Point(250, 100);
            this.commandBoxLabel.Size = new System.Drawing.Size(300, 270);
            this.commandBoxLabel.BackColor = Color.Gray;
            this.commandBoxLabel.BorderStyle = BorderStyle.FixedSingle;

            // outputBoxLabel
            this.outputBoxLabel.Location = new System.Drawing.Point(250, 400);
            this.outputBoxLabel.Size = new System.Drawing.Size(300, 150);
            this.outputBoxLabel.BackColor = Color.LightGray;
            this.outputBoxLabel.BorderStyle = BorderStyle.FixedSingle;

            // runButton
            this.runButton.Location = new System.Drawing.Point(10, 240);
            this.runButton.Size = new System.Drawing.Size(80, 30);
            this.runButton.Text = "Run";
            this.runButton.Visible = false;
            this.runButton.Click += new System.EventHandler(this.RunProgram);

            // metricsButton
            this.metricsButton.Location = new System.Drawing.Point(100, 240);
            this.metricsButton.Size = new System.Drawing.Size(80, 30);
            this.metricsButton.Text = "Metrics";
            this.metricsButton.Visible = false;
            this.metricsButton.Click += new System.EventHandler(this.CalculateMetrics);

            // adding everything 
            this.Controls.Add(this.gridPictureBox);
            this.Controls.Add(this.instructionsLabel);
            this.Controls.Add(this.comboBoxOptions);
            this.Controls.Add(this.comboBoxSecondaryOptions);
            this.Controls.Add(this.comboBoxLevelOptions);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.commandBoxLabel);
            this.Controls.Add(this.outputBoxLabel);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.metricsButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.metricsButton);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
