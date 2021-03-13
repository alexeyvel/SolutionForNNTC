using SolutionForNNTC.ConsoleCmd;
using System;

namespace SolutionForNNTC
{
	internal class ExitCmd : IConsoleCommand
	{
		private ApplicationCore _application;
		
		public static event Action<string, ConsoleColor> Print;
		public string Description => ": выход из приложения";
		public string CommandWord => "exit";
		public ExitCmd(ApplicationCore application)
		{
			ThrowHelper.Throw_IfArgumentNull(nameof(application), application);
			_application = application;
		}
		public void Execute(string args)
		{
			if(string.IsNullOrWhiteSpace(args))
			{
				_application.IsRun = false;
			}
			else
			{
				Print?.Invoke(InputHandler.ArgsFailMessage, InputHandler.FailMessageColor);
			}
		}
	}
}