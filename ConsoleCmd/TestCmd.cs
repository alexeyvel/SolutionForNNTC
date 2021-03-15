using System;
using System.Collections.Generic;
using SolutionForNNTC.Tests;

namespace SolutionForNNTC.ConsoleCmd
{
    class TestCmd : IConsoleCommand
    {
        private ApplicationCore _application;
        private TestsPool _tests;

        public static event Action<string, ConsoleColor> Print;
        public string Description => ": запуск тестирования приложения";

        public string CommandWord => "test";

        public TestCmd(ApplicationCore application)
        {
            ThrowHelper.Throw_IfArgumentNull(nameof(application), application);
            _application = application;
            _tests = new TestsPool();
        }
        public void Execute(string args)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                foreach (Test test in _tests)
                {
                    try
                    {
                        test.RunTest();
                    }
                    catch (Exception ex)
                    {
                        Print?.Invoke(ex.Message, InputHandler.ErrorMessageColor);
                        continue;
                    }
                }
            }
            else
            {
                Print?.Invoke(InputHandler.ArgsFailMessage, InputHandler.FailMessageColor);
            }
        }
    }
}
