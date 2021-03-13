using System;

namespace SolutionForNNTC.ConsoleCmd
{
	class HelpCmd : IConsoleCommand
    {
		public static event Action<string, ConsoleColor> Print;
		public string Description => ": вывод списка доступных команд";
        public string CommandWord => "help";

        public void Execute(string args)
        {
			if(string.IsNullOrWhiteSpace(args))
			{
				foreach(var cmd in ApplicationCore.AvailableCommands)
				{
					PrintNote(cmd.Value.CommandWord, cmd.Value.Description, ConsoleColor.Cyan);
				}
			}
			else
			{
				Print?.Invoke(InputHandler.ArgsFailMessage, InputHandler.FailMessageColor);
			}
		}
        private static void PrintNote(string cmdWord, string description, ConsoleColor cmdColor)
        {
            Console.ForegroundColor = cmdColor;
            Console.Write($"{cmdWord,-20}");
            Console.ResetColor();
            Console.WriteLine($"{description}");
        }
    }
}
