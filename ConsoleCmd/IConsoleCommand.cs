namespace SolutionForNNTC.ConsoleCmd
{
	interface IConsoleCommand
    {
        string Description { get; }
        string CommandWord { get; }
        void Execute(string args);
    }
}
