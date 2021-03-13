namespace SolutionForNNTC
{
	interface IEntityCreator<T> where T: class, new()
	{
		 T CreateEntity();
	}
}
