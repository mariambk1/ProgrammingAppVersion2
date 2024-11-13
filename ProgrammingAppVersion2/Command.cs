using ProgrammingApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingApp
{
    public abstract class Command
    {
        public string CommandType { get; protected set; }
        public abstract void Execute(Person person);
        public override string ToString()
        {
            return CommandType;
        }
        public abstract string ToHtml();
    }

    public class Move : Command
    {
        private int Steps { get; set; }

        public Move(int steps)
        {
            Steps = steps;
            CommandType = "Move";
        }

        public override void Execute(Person person)
        {
            person.Move(Steps);
        }
        public override string ToString()
        {
            return $"Move {Steps}";
        }

        public override string ToHtml()
        {
            return $"Move <i>{Steps}</i>";
        }
    }

    public class Turn : Command
    {
        private string TurnDirection { get; set; } 

        public Turn(string turnDirection)
        {
            if (turnDirection != "Left" && turnDirection != "Right")
                throw new ArgumentException("Turn must be either 'Left' or 'Right'");

            TurnDirection = turnDirection;
            CommandType = "Turn";
        }

        public override void Execute(Person person)
        {
            person.Turn(TurnDirection); 
        }

        public override string ToString()
        {
            return $"Turn {TurnDirection}";
        }

        public override string ToHtml()
        {
            return $"Turn <i>{TurnDirection}</i>";
        }
    }

    public class Repeat : Command
    {
        public int Times { get; private set; }
        public List<Command> CommandList { get; private set; }

        public Repeat(int times, List<Command> commandList)
        {
            Times = times;
            CommandList = commandList;
            CommandType = "Repeat";
        }

        public override void Execute(Person person)
        {
            for (int i = 0; i < Times; i++)
            {
                foreach (var command in CommandList)
                {
                    command.Execute(person);
                }
            }
        }
        public override string ToString()
        {
            var result = $"Repeat {Times}"; 
            foreach (var command in CommandList)
            {
                result += $"\n    {command.ToString()}"; 
            }
            return result;
        }

        public override string ToHtml()
        {
            string nestedCommandsHtml = "<ul>";
            foreach (var command in CommandList)
            {
                nestedCommandsHtml += $"<li>{command.ToHtml()}</li>";
            }
            nestedCommandsHtml += "</ul>";
            return $"Repeat <i>{Times}</i> times:{nestedCommandsHtml}";
        }
    }
    public class RepeatUntil : Command
    {
        public Func<Person, bool> Condition { get; private set; }
        public List<Command> CommandList { get; private set; }

        public RepeatUntil(Func<Person, bool> condition, List<Command> commandList)
        {
            Condition = condition;
            CommandList = commandList;
            CommandType = "RepeatUntil";
        }

        public override void Execute(Person person)
        {
            while (!Condition(person))
            {
                foreach (var command in CommandList)
                {
                    command.Execute(person);
                }
            }
        }
        public override string ToString()
        {
            var result = "RepeatUntil [Condition]"; 
            foreach (var command in CommandList)
            {
                result += $"\n    {command.ToString()}"; 
            }
            return result;
        }
        public override string ToHtml()
        {
            string nestedCommandsHtml = "<ul>";
            foreach (var command in CommandList)
            {
                nestedCommandsHtml += $"<li>{command.ToHtml()}</li>";
            }
            nestedCommandsHtml += "</ul>";
            return $"RepeatUntil [Condition]:{nestedCommandsHtml}";
        }
    }
}
