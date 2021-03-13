using SolutionForNNTC.ConsoleCmd;
using System;
using System.Threading.Tasks;

namespace SolutionForNNTC
{
	static class InputHandler
    {
        public static string CmdFailMessage  => "Команда не распознана. Попробуйте ввести команду заново.\nДля отоброжения доступных команд введите help и нажмите enter";
		public static string ArgsFailMessage => "Аргумент команды не распознан, или команда не принимает аргументы. Попробуйте ввести команду заново.\nДля отоброжения доступных команд введите help и нажмите enter";
		public static ConsoleColor FailMessageColor => ConsoleColor.Yellow;
		public static ConsoleColor ErrorMessageColor => ConsoleColor.Red;

		public static event Action<string, ConsoleColor> PrintFailMessage;
        private static string separator = " -";
        public static async Task CommandHandlerAsync()
        {
            await Task.Run(() => CommandHandler(Console.ReadLine().ToLower()));
        }
        private static void CommandHandler(string cmdLine)
        {
            if (TryParseArgs(cmdLine, out string command, out string arg))
            {
                if (ApplicationCore.AvailableCommands.TryGetValue(command, out IConsoleCommand cmd))
                {
                    try
                    {
                        cmd.Execute(arg);
                    }
                    catch (Exception e)
                    {
                        PrintFailMessage?.Invoke(e.Message, ErrorMessageColor);
                    }
                }
                else
                {
                    PrintFailMessage?.Invoke(CmdFailMessage, FailMessageColor);
                }
            }
            else
            {
                PrintFailMessage?.Invoke(CmdFailMessage, FailMessageColor);
            }
        }
        private static bool TryParseArgs(string cmdLine, out string command, out string args)
        {
            cmdLine = cmdLine.TrimEnd(' ');
            command = string.Empty;
            args = string.Empty;
            var separateStr = cmdLine.Split(separator, StringSplitOptions.None);
            if (separateStr.Length > 2)
            {
                return false;
            }
            if (separateStr.Length == 2)
            {
                command = separateStr[0];
                args = separateStr[1];
                return true;
            }
            command = separateStr[0];
            return true;
        }
    }
}