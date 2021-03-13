using System;
using System.Collections.Generic;

namespace SolutionForNNTC.ConsoleCmd
{
	class SortCmd : IConsoleCommand
    {
        private ApplicationCore _application;
        private static string _inputFailMessage = "Команда не распознана. Попробуйте ввести команду заново.\nДля отоброжения доступных команд введите help и нажмите enter";
        public static event Action<string, ConsoleColor> Print;
        private Dictionary<string, Comparison<BornInThe21stCentury>> _condition;

        private Comparison<BornInThe21stCentury> _descSortDate = (x, y) => y.Birthday.CompareTo(x.Birthday);

        private Comparison<BornInThe21stCentury> _ascSortDate = (x, y) => x.Birthday.CompareTo(y.Birthday);

        private Comparison<BornInThe21stCentury> _descSortName = (x, y) => y.Name.CompareTo(x.Name);

        private Comparison<BornInThe21stCentury> _ascSortName = (x, y) => x.Name.CompareTo(y.Name);

        public string Description => ": сортировка списка согласно выбранного условия, см. пример ниже:\n" +
                                         $"{"sort -ascd сортирует список по возрастанию даты рождения",78}\n" +
                                         $"{"sort -descd сортирует список по убыванию даты рождения",76}\n" +
                                         $"{"sort -ascn сортирует список по имени в алфавитном порядке",79}\n" +
                                         $"{"sort -descn сортирует список по имени в обратном алфавитном порядке",89}";

        public string CommandWord => "sort -<условие>";

        public SortCmd(ApplicationCore application)
        {
            ThrowHelper.Throw_IfArgumentNull(nameof(application), application);
            _application = application;
            InitConditionList();
        }

        public void Execute(string args)
        {
            Comparison<BornInThe21stCentury> sort;

            if (_condition.TryGetValue(args, out sort))
            {
                if (_application.ItemList.Count > 1)
                {
                    _application.ItemList.Sort(sort);
                }
                foreach (var item in _application.ItemList)
                {
                    Print?.Invoke(item.ToString(), ConsoleColor.White);
                }
            }
            else
            {
                Print?.Invoke(_inputFailMessage, ConsoleColor.Yellow);
            }
        }

        private void InitConditionList()
        {
            _condition = new Dictionary<string, Comparison<BornInThe21stCentury>>()
            {
                {"ascd", _ascSortDate},
                {"descd", _descSortDate},
                {"ascn", _ascSortName},
                {"descn", _descSortName}
            };
        }
    }
}
