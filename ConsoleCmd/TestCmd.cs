using System;

namespace SolutionForNNTC.ConsoleCmd
{
	class TestCmd : IConsoleCommand
    {
        private ApplicationCore _application;

		public static event Action<string, ConsoleColor> Print;
		public string Description => ": запуск тестирования приложения";

        public string CommandWord => "test";

        public TestCmd(ApplicationCore application)
        {
            ThrowHelper.Throw_IfArgumentNull(nameof(application), application);
            _application = application;
        }
        public void Execute(string args)
        {
			if(string.IsNullOrWhiteSpace(args))
			{
				throw new NotImplementedException("сорян!(\nинструмент тестирования находится в стадии разработки");
			}
			else
			{
				Print?.Invoke(InputHandler.ArgsFailMessage, InputHandler.FailMessageColor);
			}			
        }
    }
}
