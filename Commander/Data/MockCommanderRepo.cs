using System.Collections.Generic;
using Models;

namespace Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kitchen" },
                new Command { Id = 1, HowTo = "Make coffee", Line = "Make coffee", Platform = "Kitchen" },
                new Command { Id = 2, HowTo = "Make my bed nice", Line = "make bed", Platform = "Bedroom" }
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command
            {
                Id = 0,
                HowTo = "Boil an egg",
                Line = "Boil water",
                Platform = "Kitchen"
            };
        }
    }
}