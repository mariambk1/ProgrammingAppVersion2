using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection.Metadata;

namespace ProgrammingApp
{

    public class CommandsProgram
    {
        public string Name { get; set; }
        public List<Command> CommandList { get; private set; }
        private ProgramRepository Repository { get; set; }

        public CommandsProgram()
        {
            Repository = new ProgramRepository();
            CommandList = new List<Command>();
        }

        public CommandsProgram(string name, List<Command> commandList)
        {
            Name = name;
            CommandList = commandList;
        }

        public void Run()
        {
            Console.WriteLine("If you want to:\n\n" +
                              "load a file:                press 1\n" +
                              "choose an example:          press 2\n" +
                              "export text basic program:  press 3\n" +
                              "export HTML basic program:  press 4\n");
            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine("Please enter the file path:");
                string filePath = Console.ReadLine();
                ReadFile readFile = new ReadTxtFile();
                CommandList = readFile.GetListCommands(filePath);
                if (CommandList.Count > 0)
                {
                    ChooseExecutionOption();
                }
                else
                {
                    Console.WriteLine("No valid commands found in the file.");
                }
            }
            else if (input == "2")
            {
                ChooseExampleProgram();
            }
            else if (input == "3")
            {
                Export("text", Repository.AdvancedPrograms[0].CommandList);
            }
            else if (input == "4")
            {
                Export("html", Repository.AdvancedPrograms[0].CommandList);
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
        private void Export(string format, List<Command> commandlist)
        {
            Console.WriteLine("Enter the filepath");
            string filePath = Console.ReadLine();
            WriteFile writefile = new WriteFile(filePath);
            switch (format)
            {
                case "text":
                    writefile.TextExport(commandlist);
                    break;
                case "html":
                    writefile.HtmlExport(commandlist, "my program");
                    break;
                default:
                    Console.WriteLine("an error occured");
                    return;

            }
        }

        private void ChooseExampleProgram()
        {
            Console.WriteLine("Choose:\n" +
                              "1: for a basic program\n" +
                              "2: for an advanced program\n" +
                              "3: for an expert program\n\n");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Select a Basic Program:");
                    for (int i = 0; i < Repository.BasicPrograms.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {Repository.BasicPrograms[i].Name}");
                    }
                    string basicProgramChoice = Console.ReadLine();
                    if (int.TryParse(basicProgramChoice, out int basicProgramIndex) && basicProgramIndex >= 1 && basicProgramIndex <= Repository.BasicPrograms.Count)
                    {
                        CommandList = Repository.BasicPrograms[basicProgramIndex - 1].CommandList; 
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                        return;
                    }
                    break;
                case "2":
                    Console.WriteLine("Select an Advanced Program:");
                    for (int i = 0; i < Repository.AdvancedPrograms.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {Repository.AdvancedPrograms[i].Name}");
                    }
                    string advancedProgramChoice = Console.ReadLine();
                    if (int.TryParse(advancedProgramChoice, out int advancedProgramIndex) && advancedProgramIndex >= 1 && advancedProgramIndex <= Repository.AdvancedPrograms.Count)
                    {
                        CommandList = Repository.AdvancedPrograms[advancedProgramIndex - 1].CommandList; 
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                        return;
                    }
                    break;
                case "3":
                    Console.WriteLine("Select an Expert Program:");
                    for (int i = 0; i < Repository.ExpertPrograms.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {Repository.ExpertPrograms[i].Name}");
                    }
                    string expertProgramChoice = Console.ReadLine();
                    if (int.TryParse(expertProgramChoice, out int expertProgramIndex) && expertProgramIndex >= 1 && expertProgramIndex <= Repository.ExpertPrograms.Count)
                    {
                        CommandList = Repository.ExpertPrograms[expertProgramIndex - 1].CommandList;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            ChooseExecutionOption();
        }


        private void ChooseExecutionOption()
        {
            Console.WriteLine("Choose:\n" +
                              "1 to execute the program\n" +
                              "2 to calculate metrics\n\n");
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                ExecuteProgram();
                Console.ReadLine();
            }
            else if (answer == "2")
            {
                PrintMetrics();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        public void PrintMetrics()
        {
            Metrics metrics = new Metrics();
            Metrics resultMetrics = metrics.CalculateMetrics(CommandList);

            Console.WriteLine($"Total Commands: {resultMetrics.Total}");
            Console.WriteLine($"Max Nesting Level: {resultMetrics.MaxNesting}");
            Console.WriteLine($"Repeat Commands: {resultMetrics.RepeatCount}");
        }



        public void ExecuteProgram()
        {
            Person person = new Person();
            foreach (var command in CommandList)
            {
                command.Execute(person);
            }
            Console.WriteLine($"Final position of the person: {person.GetPosition()}");
            Console.WriteLine($"Final direction of the person: {person.GetDirection()}");
        }
    }

}

    





 