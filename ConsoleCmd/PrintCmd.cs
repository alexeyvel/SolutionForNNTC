using System;

namespace SolutionForNNTC.ConsoleCmd
{
	class PrintCmd : IConsoleCommand
	{
		private ApplicationCore _application;
		
		public static event Action<string, ConsoleColor> Print;
		public string Description => ": вывод сгенерированного списка на экран";

		public string CommandWord => "print";

		public PrintCmd(ApplicationCore application)
		{
			ThrowHelper.Throw_IfArgumentNull(nameof(application), application);
			_application = application;
		}
		public void Execute(string args)
		{
			if(string.IsNullOrWhiteSpace(args))
			{
				foreach(var item in _application.ItemList)
				{
					Console.WriteLine(item);
				}
			}
			else
			{
				Print?.Invoke(InputHandler.ArgsFailMessage, InputHandler.FailMessageColor);
			}
		}
	}
}
