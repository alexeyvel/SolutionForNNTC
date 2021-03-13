using SolutionForNNTC.ConsoleCmd;
using System;
using System.Collections.Generic;

namespace SolutionForNNTC
{
	class ApplicationCore
    {
        private const int START_SIZE_LIST = 10;
        private string firstCmd = "new";

        private static ApplicationCore _appInstance;

        private List<BornInThe21stCentury> _itemList;

        private static Dictionary<string, IConsoleCommand> _availableCommands;

        private static string StartMessage = $"«дравствуйте, данное приложение при запуске автоматически генерирует список из {START_SIZE_LIST} персон.\n" +
            $"ƒанный список можно сортировать, фильтровать либо создать новый список с произвольным числом персон.\n" +
            $"—писок доступных операций можно получить, введ€ команду: help\n" +
            $"ƒл€ выхода из приложени€ введите команду: exit\n";

        public List<BornInThe21stCentury> CopyItemList { get; private set; }
        public IEntityCreator<BornInThe21stCentury> EntityCreator { get; }
        public List<BornInThe21stCentury> ItemList
        {
            get
            {
                return _itemList;
            }
            set
            {
                ThrowHelper.Throw_IfArgumentNull(nameof(value), value);
                _itemList = value;
            }
        }
        public static Dictionary<string, IConsoleCommand> AvailableCommands
        {
            get
            {
                return _availableCommands;
            }
            set
            {
                ThrowHelper.Throw_IfArgumentNull(nameof(value), value);
                _availableCommands = value;
            }
        }

        private ApplicationCore()
        {
            IsRun = true;
            _itemList = new List<BornInThe21stCentury>(START_SIZE_LIST);
            CopyItemList = new List<BornInThe21stCentury>(START_SIZE_LIST);
            EntityCreator = new PersonCreator();
            InputHandler.PrintFailMessage += PrintMessage;
            InitCommandsList();
            SubscribeToCmdEvents();
        }

        public bool IsRun { get; set; }
        public Action Command { get; set; }

        public static ApplicationCore CreateApp()
        {
            return _appInstance ?? (_appInstance = new ApplicationCore());
        }
        public void Run()
        {
            PrintMessage(StartMessage);
            GenerateNewList($"{START_SIZE_LIST}");

            while (IsRun)
            {
                _ = InputHandler.CommandHandlerAsync();
            }
        }

        private void InitCommandsList()
        {
            _availableCommands = new Dictionary<string, IConsoleCommand>()
            {
                {"help", new HelpCmd()},
                {"exit", new ExitCmd(this)},
                {"new", new NewListCmd(this)},
                {"print", new PrintCmd(this)},
                {"filtr", new FiltrCmd(this)},
                {"sort", new SortCmd(this)},
                {"restore", new RestCmd(this)},
                {"test", new TestCmd(this)}
            };
        }
        private void SubscribeToCmdEvents()
        {
            ((NewListCmd)GetInitCmd("new")).CreateCopy += ReInitCopyList;
			HelpCmd.Print += PrintMessage;
			ExitCmd.Print += PrintMessage;
			NewListCmd.Print += PrintMessage;
            FiltrCmd.Print += PrintMessage;
            SortCmd.Print += PrintMessage;
            RestCmd.Print += PrintMessage;
			PrintCmd.Print += PrintMessage;
            TestCmd.Print += PrintMessage;
        }

        private IConsoleCommand GetInitCmd(string cmdWord)
        {
            if (!_availableCommands.TryGetValue(cmdWord, out IConsoleCommand cmd))
            {
                string message = $"команда {cmdWord} не была инициализирована";
                PrintMessage(message, ConsoleColor.DarkRed);
            }
            return cmd;
        }

        private void GenerateNewList(string arg)
        {          
            if (!_availableCommands.TryGetValue(firstCmd, out IConsoleCommand cmd))
            {
                string message = $"команда {firstCmd} не была инициализирована";
                PrintMessage(message, ConsoleColor.DarkRed);
            }
            cmd.Execute(arg);
        }

        private void ReInitCopyList(List<BornInThe21stCentury> template)
        {
            CopyItemList = new List<BornInThe21stCentury>(_itemList);
        }
        private static void PrintMessage(string message, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}