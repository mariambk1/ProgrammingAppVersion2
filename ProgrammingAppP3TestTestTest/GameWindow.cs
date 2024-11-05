using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProgrammingApp
{
    public partial class GameWindow : Form
    {
        private GameGrid gameGrid; 

        public GameWindow()
        {
            InitializeComponent();

            instructionsLabel.Text = "I want to:";

            comboBoxOptions.Items.Add("Choose your option");
            comboBoxOptions.Items.Add("Load a file");
            comboBoxOptions.Items.Add("Choose an example");
            comboBoxOptions.Items.Add("Export text basic program");
            comboBoxOptions.Items.Add("Export HTML basic program");
            comboBoxOptions.SelectedIndex = 0;

            gameGrid = new GameGrid();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            int xOffset = 600;
            int yOffset = 100;

            gameGrid.Draw(g, xOffset, yOffset);
        }

        private void comboBoxOptionsChoice(object sender, EventArgs e)
        {
            string selectedOption = comboBoxOptions.SelectedItem.ToString();
            textBoxFilePath.Visible = false;
            comboBoxSecondaryOptions.Visible = false;
            comboBoxLevelOptions.Visible = false;
            runButton.Visible = false;
            metricsButton.Visible = false;
            commandBoxLabel.Text = "hier komen de commands";
            outputBoxLabel.Text = "hier komen de metrics";

            switch (selectedOption)
            {
                case "Load a file":
                    textBoxFilePath.Visible = true;
                    outputLabel.Text = "Please enter the file path:";
                    break;

                case "Choose an example":
                    comboBoxSecondaryOptions.Items.Clear();
                    comboBoxSecondaryOptions.Items.Add("Basic Program");
                    comboBoxSecondaryOptions.Items.Add("Advanced Program");
                    comboBoxSecondaryOptions.Items.Add("Expert Program");
                    comboBoxSecondaryOptions.Visible = true;
                    outputLabel.Text = "Please choose an example:";
                    break;
                case "Export text basic program":
                case "Export HTML basic program":
                    outputLabel.Text = "Location: "; 
                    break;
            }
        }

        private void comboBoxSecondaryOptionsChoice(object sender, EventArgs e)
        {
            if (comboBoxSecondaryOptions.SelectedItem != null)
            {
                comboBoxLevelOptions.Items.Clear(); 

                if (comboBoxSecondaryOptions.SelectedItem.ToString() == "Basic Program")
                {
                    comboBoxLevelOptions.Items.Add("Basic Program 1");
                    comboBoxLevelOptions.Items.Add("Basic Program 2");
                    comboBoxLevelOptions.Items.Add("Basic Program 3");
                }
                else if (comboBoxSecondaryOptions.SelectedItem.ToString() == "Advanced Program")
                {
                    comboBoxLevelOptions.Items.Add("Advanced Program 1");
                    comboBoxLevelOptions.Items.Add("Advanced Program 2");
                    comboBoxLevelOptions.Items.Add("Advanced Program 3");
                }
                else if (comboBoxSecondaryOptions.SelectedItem.ToString() == "Expert Program")
                {
                    comboBoxLevelOptions.Items.Add("Expert Program 1");
                    comboBoxLevelOptions.Items.Add("Expert Program 2");
                    comboBoxLevelOptions.Items.Add("Expert Program 3");
                }

                comboBoxLevelOptions.Visible = true; 

                runButton.Visible = false;
                metricsButton.Visible = false;
            }
        }

        private void comboBoxLevelOptionsChoice(object sender, EventArgs e)
        {
            if (comboBoxLevelOptions.SelectedItem != null)
            {
                runButton.Visible = true;
                metricsButton.Visible = true;
            }
            else
            {
                runButton.Visible = false;
                metricsButton.Visible = false;
            }
        }



        private void RunProgram(object sender, EventArgs e)
        {
            // even als placeholder
            var commands = new List<string> { "(dit is placeholder data)","move 2", "repeat 3", "move 1", "end repeat", "turn left" };
            commandBoxLabel.Text = string.Join("\n", commands);
        }

        private void CalculateMetrics(object sender, EventArgs e)
        {
            outputBoxLabel.Text = "Total Commands: 4\nMax Nesting: 1\nRepeat Commands: 1";
        }
    }
}
