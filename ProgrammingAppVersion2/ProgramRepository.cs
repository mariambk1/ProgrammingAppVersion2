using ProgrammingApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingApp
{
    public class ProgramRepository
    {
        public List<CommandsProgram> BasicPrograms { get; private set; }
        public List<CommandsProgram> AdvancedPrograms { get; private set; }
        public List<CommandsProgram> ExpertPrograms { get; private set; }

        public ProgramRepository()
        {
            BasicPrograms = new List<CommandsProgram>
             {
                new CommandsProgram("Basic Program 1",
                    new List<Command> {
                        new Move(2),
                        new Turn("Left")
                    }),
                new CommandsProgram("Basic Program 2",
                    new List<Command> {
                        new Move(3),
                        new Turn("Right")
                    }),
                new CommandsProgram("Basic Program 3",
                    new List<Command> {
                        new Move(1),
                        new Move(1),
                        new Turn("Left")
                })

             };

            AdvancedPrograms = new List<CommandsProgram>
            {
                new CommandsProgram("Advanced Program 1",
                    new List<Command> {
                        new Repeat(3, new List<Command> {
                            new Move(1)
                        })
                    }),
                new CommandsProgram("Advanced Program 2",
                    new List<Command> {
                        new Move(2),
                        new Repeat(2, new List<Command> {
                            new Turn("Right"),
                            new Move(1)
                        })
                    }),
                new CommandsProgram("Advanced Program 3",
                    new List<Command> {
                        new Repeat(4, new List<Command> {
                            new Move(1),
                            new Turn("Right")
                        })
                    })
            };

            ExpertPrograms = new List<CommandsProgram>
            {
                new CommandsProgram("Expert Program 1",
                    new List<Command> {
                        new Move(1),
                        new Repeat(2, new List<Command> {
                            new Turn("Right"),
                            new Move(2)
                        })
                    }),
                new CommandsProgram("Expert Program 2",
                    new List<Command> {
                        new Repeat(2, new List<Command> {
                            new Turn("Left"),
                            new Move(2),
                            new Repeat(3, new List<Command> {
                                new Move(1)
                            })
                        })
                    }),
                new CommandsProgram("Expert Program 3",
                    new List<Command> {
                        new Repeat(3, new List<Command> {
                            new Turn("Right"),
                            new Move(1)
                        }),
                        new Turn("Left"),
                        new Move(2)
                    })
            };
        }
    }
}
