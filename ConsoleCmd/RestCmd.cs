using SolutionForNNTC.ConsoleCmd;
using System;
using System.Collections.Generic;

namespace SolutionForNNTC
{
    internal class RestCmd : IConsoleCommand
    {
        private ApplicationCore _application;

		public static event Action<string, ConsoleColor> Print;

        public RestCmd(ApplicationCore application)
        {
            ThrowHelper.Throw_IfArgumentNull(nameof(application), application);
            _application = application;
        }

        public string Description => $": восстанавливает последний сгенерированный список, отменяя операции фильтрации и сортировки";

        public string CommandWord => "restore";

        public void Execute(string args)
        {
			if(string.IsNullOrWhiteSpace(args))
			{
				_application.ItemList = new List<BornInThe21stCentury>(_application.CopyItemList);
				foreach(var item in _application.ItemList)
				{
					Print?.Invoke(item.ToString(), ConsoleColor.White);
				}
			}
			else
			{
				Print?.Invoke(InputHandler.ArgsFailMessage, InputHandler.FailMessageColor);
			}
            
        }
    }
}