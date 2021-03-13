using System;
using System.Collections.Generic;
using System.Linq;

namespace SolutionForNNTC.ConsoleCmd
{
	class FiltrCmd : IConsoleCommand
    {
        private ApplicationCore _application;        
        private Dictionary<string, Func<BornInThe21stCentury, int, bool>> _condition;
        private Func<BornInThe21stCentury, int, bool> _evenFiltr = (x, index) => index % 2 != 0;
        private Func<BornInThe21stCentury, int, bool> _oddFiltr = (x, index) => index % 2 == 0;

		public static event Action<string, ConsoleColor> Print;

		public string Description => ": фильтрация списка согласно выбранного условия, см. пример ниже:\n" +
                                         $"{"filtr -even фильтрует список по четным строкам",68}\n" +
                                         $"{"filtr -odd фильтрует список по не четным строкам",70}";

        public string CommandWord => "filtr -<условие>";

        public FiltrCmd(ApplicationCore application)
        {
            ThrowHelper.Throw_IfArgumentNull(nameof(application), application);
            _application = application;
            InitConditionList();
        }

        public void Execute(string args)
        {
            Func<BornInThe21stCentury, int, bool> filtr;

            if (_condition.TryGetValue(args, out filtr))
            {
                if (_application.ItemList.Count > 1)
                {
                    _application.ItemList = _application.ItemList.Where(filtr).ToList();                  
                }
                foreach (var item in _application.ItemList)
                {
                    Print?.Invoke(item.ToString(), ConsoleColor.White);
                }
            }
            else
            {
				Print?.Invoke(InputHandler.ArgsFailMessage, InputHandler.FailMessageColor);
			}
        }

        private void InitConditionList()
        {
            _condition = new Dictionary<string, Func<BornInThe21stCentury, int, bool>>()
            {
                {"even", _evenFiltr},
                {"odd", _oddFiltr}
            };
        }
    }
}
