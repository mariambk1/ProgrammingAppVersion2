using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace ProgrammingApp
{
    public partial class GameWindow : Form
    {
        private GameGrid gameGrid;
        private Person person;
        private ProgramRepository programRepository;  

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

            person = new Person();
            gameGrid = new GameGrid(person);
            programRepository = new ProgramRepository();  

            gridPictureBox.Paint += DrawGameGrid;
            textBoxFilePath.KeyPress += textBoxFilePath_KeyPress;
        }

        private void DrawGameGrid(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int xOffset = 0;
            int yOffset = 0;

            gameGrid.Draw(g, xOffset, yOffset);

            Bitmap currentImage = person.GetCharacterImage(); 
            if (currentImage != null)
            {
                int imageX = xOffset + person.PlaceX * gameGrid.CellSize;
                int imageY = yOffset + person.PlaceY * gameGrid.CellSize;

                g.DrawImage(currentImage, imageX, imageY, gameGrid.CellSize, gameGrid.CellSize);
            }
        }


        private void comboBoxOptionsChoice(object sender, EventArgs e)
        {
            string selectedOption = comboBoxOptions.SelectedItem.ToString();
            textBoxFilePath.Visible = false;
            comboBoxSecondaryOptions.Visible = false;
            comboBoxLevelOptions.Visible = false;
            runButton.Visible = false;
            metricsButton.Visible = false;
            commandBoxLabel.Text = "commands:";
            outputBoxLabel.Text = "metrics:";

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
            string selectedProgram = comboBoxLevelOptions.SelectedItem.ToString();
            List<Command> commands = new List<Command>();

            if (comboBoxSecondaryOptions.SelectedItem.ToString() == "Basic Program")
            {
                commands = programRepository.BasicPrograms
                    .FirstOrDefault(p => p.Name == selectedProgram)?.CommandList;
            }
            else if (comboBoxSecondaryOptions.SelectedItem.ToString() == "Advanced Program")
            {
                commands = programRepository.AdvancedPrograms
                    .FirstOrDefault(p => p.Name == selectedProgram)?.CommandList;
            }
            else if (comboBoxSecondaryOptions.SelectedItem.ToString() == "Expert Program")
            {
                commands = programRepository.ExpertPrograms
                    .FirstOrDefault(p => p.Name == selectedProgram)?.CommandList;
            }

            if (commands != null && commands.Count > 0)
            {
                person.ResetPosition();
                commandBoxLabel.Text = string.Join("\n", commands.Select(c => c.ToString()));

                foreach (var command in commands)
                {
                    command.Execute(person);
                }

                gridPictureBox.Invalidate();
            }
            else
            {
                MessageBox.Show("no valid commands.");
            }
        }


        private void CalculateMetrics(object sender, EventArgs e)
        {
            string selectedProgram = comboBoxLevelOptions.SelectedItem.ToString();
            List<Command> commands = new List<Command>();

            if (comboBoxSecondaryOptions.SelectedItem.ToString() == "Basic Program")
            {
                commands = programRepository.BasicPrograms
                    .FirstOrDefault(p => p.Name == selectedProgram)?.CommandList;
            }
            else if (comboBoxSecondaryOptions.SelectedItem.ToString() == "Advanced Program")
            {
                commands = programRepository.AdvancedPrograms
                    .FirstOrDefault(p => p.Name == selectedProgram)?.CommandList;
            }
            else if (comboBoxSecondaryOptions.SelectedItem.ToString() == "Expert Program")
            {
                commands = programRepository.ExpertPrograms
                    .FirstOrDefault(p => p.Name == selectedProgram)?.CommandList;
            }

            if (commands != null && commands.Count > 0)
            {
                Metrics metrics = new Metrics();
                Metrics resultMetrics = metrics.CalculateMetrics(commands);

                outputBoxLabel.Text = $"Total Commands: {resultMetrics.Total}\n" +
                                      $"Max Nesting Level: {resultMetrics.MaxNesting}\n" +
                                      $"Repeat Commands: {resultMetrics.RepeatCount}";
            }
            else
            {
                MessageBox.Show("no valid commands");
            }
        }

        private void textBoxFilePath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string filePath = textBoxFilePath.Text;

                filePath = filePath.Replace(@"\", @"\\");

                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("please enter a valid path.");
                    return;
                }

                try
                {
                    ReadTxtFile fileReader = new ReadTxtFile();
                    List<Command> commands = fileReader.GetListCommands(filePath);

                    if (commands.Count == 0)
                    {
                        MessageBox.Show("no valid commands. ");
                    }
                    else
                    {
                        var commandTexts = commands.Select(c => c.ToString()).ToList();
                        commandBoxLabel.Text = string.Join("\n", commandTexts);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"error  while reading the file:\n {ex.Message} \n check the file format and path.");
                }
            }
        }
    }
}
