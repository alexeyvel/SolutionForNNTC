namespace SolutionForNNTC
{
	interface IValidate<T>
    {
        bool TryValidate(T arg);
    }
}
