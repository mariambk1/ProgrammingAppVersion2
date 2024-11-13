using ProgrammingApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ProgrammingApp
{
    public abstract class ReadFile
    {
        public abstract List<Command> GetListCommands(string filePath);
    }

    public class ReadTxtFile : ReadFile
    {
        public override List<Command> GetListCommands(string filePath)
        {
            List<Command> commands = new List<Command>();
            string[] input = FileReader(filePath);
            if (input.Length == 0)
                return commands;

            int lineIndex = 0;
            commands = ParseCommands(input, ref lineIndex);

            return commands;
        }

        private string[] FileReader(string filepath)
        {
            if (File.Exists(filepath))
                return File.ReadAllLines(filepath);
            else
                return new string[0];
        }

        private static List<Command> ParseCommands(string[] lines, ref int lineIndex, int indentLevel = 0)
        {
            List<Command> commands = new List<Command>();

            while (lineIndex < lines.Length)
            {
                string line = lines[lineIndex].Trim();

                if (string.IsNullOrEmpty(line))
                {
                    lineIndex++;
                    continue;
                }

                int currentIndentLevel = GetIndentationLevel(lines[lineIndex]);

                if (currentIndentLevel < indentLevel)
                {
                    break;
                }

                if (line.StartsWith("repeat", StringComparison.OrdinalIgnoreCase))
                {
                    string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2 && int.TryParse(parts[1], out int repeatCount))
                    {
                        if (parts.Length > 2 && parts[2].Equals("times", StringComparison.OrdinalIgnoreCase))
                        {
                            lineIndex++; 
                            List<Command> nestedCommands = ParseCommands(lines, ref lineIndex, currentIndentLevel + 1);
                            commands.Add(new Repeat(repeatCount, nestedCommands));
                        }
                    }
                }

                else if (line.StartsWith("move", StringComparison.OrdinalIgnoreCase))
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length >= 2 && int.TryParse(parts[1], out int steps))
                    {
                        commands.Add(new Move(steps));
                    }
                    lineIndex++;
                }

                else if (line.StartsWith("turn", StringComparison.OrdinalIgnoreCase))
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length >= 2 && (parts[1].Equals("Left", StringComparison.OrdinalIgnoreCase) ||
                                               parts[1].Equals("Right", StringComparison.OrdinalIgnoreCase)))
                    {
                        commands.Add(new Turn(parts[1]));
                    }
                    lineIndex++;
                }

                else if (line.Equals("end", StringComparison.OrdinalIgnoreCase))
                {
                    lineIndex++;  
                    return commands;
                }
                else
                {
                    lineIndex++;
                }
            }

            return commands;
        }




        private static int GetIndentationLevel(string line)
        {
            int indentLevel = 0;

            while (line.Length > 0 && (line[0] == '\t' || line[0] == ' '))
            {
                indentLevel++;
                line = line.Substring(1); 
            }

            return indentLevel;
        }

    }
}



