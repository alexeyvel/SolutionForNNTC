using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionForNNTC.Tests
{
	abstract class Test
	{
		protected static string _testItem = string.Empty;
		protected static string _errorMessage = " завершился с ошибкой.";
		protected static string _successMessage = " завершился успешно.";

		public abstract void RunTest();
		protected void PrintMessage(string message, ConsoleColor color = ConsoleColor.Green)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}
	}
}
