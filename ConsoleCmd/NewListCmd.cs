using System;
using System.Collections.Generic;

namespace SolutionForNNTC.ConsoleCmd
{
	class NewListCmd : IConsoleCommand
    {
		private ApplicationCore _application;
        private int defaultSize = 10;
        private int maxSize = 15;

        public event Action<List<BornInThe21stCentury>> CreateCopy;
        public static event Action<string, ConsoleColor> Print;
        public string Description => $": создает новый список с указанным числом строк, но не более {maxSize} (по умолчанию число строк равно 10)\n"+
                                            $"{"пример: new -50 создаст список из 50 строк, new -10 или new список из 10 строк", 100}";
        public string CommandWord => "new -<число строк>";
        public NewListCmd(ApplicationCore application)
        {
            ThrowHelper.Throw_IfArgumentNull(nameof(application), application);
            _application = application;
        }
        public void Execute(string args)
        {
            int currentSize;
			if(string.IsNullOrWhiteSpace(args))
			{
				currentSize = defaultSize;
				GetNewList(currentSize);
				CreateCopy?.Invoke(_application.ItemList);
			}
			else if(int.TryParse(args, out currentSize))
			{
				ThrowHelper.Throw_IfOutOfRange("размер списка", currentSize, 1, maxSize);
				GetNewList(currentSize);
				CreateCopy?.Invoke(_application.ItemList);
			}
			else
			{
				Print?.Invoke(InputHandler.ArgsFailMessage, InputHandler.FailMessageColor);
			}
        }
        private void GetNewList(int count)
        {
            _application.ItemList = new List<BornInThe21stCentury>();
            for (int i = 0; i < count; i++)
            {
                var item = _application.EntityCreator.CreateEntity();
                _application.ItemList.Add(item);
                Print?.Invoke(item.ToString(), ConsoleColor.White);
            }
        }
    }
}
