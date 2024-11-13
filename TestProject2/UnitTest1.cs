
//run the tests in the terminal by typing 'dotnet test' 

using ProgrammingApp;
using Xunit; 
namespace TestProject2
 
{
    public class CommandTests
    {
        [Theory]
        [InlineData(5, 5, 0)] 
        [InlineData(0, 0, 0)]   
        [InlineData(-3, -3, 0)] 
        public void MoveCommand_Execute(int moveDistance, int expectedPlaceX, int expectedPlaceY)
        {
            Person person = new Person();
            Move moveCommand = new Move(moveDistance);
            moveCommand.Execute(person);

            Assert.Equal(expectedPlaceX, person.PlaceX);
            Assert.Equal(expectedPlaceY, person.PlaceY);
        }

        [Theory]
        [InlineData("Left", Direction.North)] 
        [InlineData("Right", Direction.South)]  
      
        public void TurnCommand_Execute(string turnDirection, Direction expectedDirection)
        {
            Person person = new Person();

            person.Turn(turnDirection);  

            Assert.Equal(expectedDirection, person.Direction);
        }

        [Fact]
        public void RepeatCommand_Execute()
        {
            Person person = new Person();
            Move moveCommand = new Move(2);
            List<Command> commandList = new List<Command> { moveCommand };
            Repeat  repeatCommand = new Repeat(3, commandList);
            repeatCommand.Execute(person);

            Assert.Equal(6, person.PlaceX); 
        }

        [Fact]
        public void RepeatUntilCommand_Execute()
        {
            Person person = new Person();
            Move moveCommand = new Move(2);
            List<Command> commandList = new List<Command> { moveCommand };
            RepeatUntil repeatUntilCommand = new RepeatUntil(p => p.PlaceX == 8, commandList);

            repeatUntilCommand.Execute(person);
            Assert.True(person.PlaceX == 8); 
        }

        [Fact]
        public void MoveCommand_ToString()
        {
            Move moveCommand = new Move(3);
            String result = moveCommand.ToString();
            Assert.Equal("Move 3", result);
        }

        [Fact]
        public void TurnCommand_ToString()
        {
            Turn turnCommand = new Turn("Left");
            String result = turnCommand.ToString();
            Assert.Equal("Turn Left", result);
        }

        [Fact]
        public void RepeatCommand_ToString()
        {
            Move moveCommand = new Move(2);
            List<Command> commandList = new List<Command> { moveCommand };
            Repeat repeatCommand = new Repeat(2, commandList);
            String result = repeatCommand.ToString();
            Assert.Equal("Repeat 2\n    Move 2", result.Trim());
        }

        [Fact]
        public void RepeatUntilCommand_ToString()
        {
            Move moveCommand = new Move(2);
            List<Command> commandList = new List<Command> { moveCommand };
            RepeatUntil repeatUntilCommand = new RepeatUntil(p => p.PlaceX >= 10, commandList);
            String result = repeatUntilCommand.ToString();

            Assert.StartsWith("RepeatUntil [Condition]", result);
            Assert.Contains("Move 2", result);
        }
    }
    public class MetricsTests
    {

        [Fact]
        public void CalculateMetrics_NestedRepeatCommand()
        {
            Metrics metricsCalculator = new Metrics();
            List<Command> commandList = new List<Command> { new Repeat(2, new List<Command> { new Move(1), new Repeat(3, new List<Command> { new Turn("Left") }) }) };

            Metrics metrics = metricsCalculator.CalculateMetrics(commandList);
            Assert.Equal(4, metrics.Total);
            Assert.Equal(2, metrics.MaxNesting);
            Assert.Equal(2, metrics.RepeatCount);
        }

        [Fact]
        public void CalculateMetrics_All()
        {
            Metrics metricsCalculator = new Metrics();
            List<Command> commandList = new List<Command> { new Move(1), new Repeat(2, new List<Command> { new Turn("Left"), new Move(1), new Repeat(3, new List<Command> { new Move(2), new Turn("Right") }) }), new Turn("Left") };
            Metrics metrics = metricsCalculator.CalculateMetrics(commandList);
            Assert.Equal(8, metrics.Total);         
            Assert.Equal(2, metrics.MaxNesting); 
            Assert.Equal(2, metrics.RepeatCount);   
        }

    } 

}