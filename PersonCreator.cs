using System;
using System.Collections.Generic;
using System.Linq;

namespace SolutionForNNTC
{
	class PersonCreator : IEntityCreator<BornInThe21stCentury>
	{
		private static Random _rand;
		private int _randomKey = 1;
		private long _ticksPerDay = 864_000_000_000; //24 * 60 * 60 * 1000 * 1000 *10;
		private string _name = string.Empty;
		private DateTime _birthday = DateTime.Today;

		public BornInThe21stCentury CreateEntity()
		{
			GenerateRandKey(DateTime.Now.Millisecond);
			_rand = new Random(_randomKey);

			int limit = NamesStore.names.Length;
			long ticksNow = DateTime.Now.Ticks;
			long ticksInTheBeginning = new DateTime(2000, 1, 1).Ticks;
			long dateSpan = (ticksNow - ticksInTheBeginning) / _ticksPerDay;
			long birthdaySpan = _rand.Next((int)dateSpan) * _ticksPerDay;

			_name = PersonDataSelector(NamesStore.names, limit);
			_birthday = new DateTime(ticksInTheBeginning + birthdaySpan);
			return new BornInThe21stCentury { Name = _name, Birthday = _birthday };
		}

		private static T PersonDataSelector<T>(IEnumerable<T> persons, int limit) where T : IComparable
		{
			int index = _rand.Next(limit);
			return persons.ElementAt(index);
		}

		private void GenerateRandKey(int initValue)
		{
			int minValue = 1;
			try
			{
				_randomKey = checked(minValue +_randomKey * initValue);
			}
			catch
			{
				_randomKey = minValue;
			}
		}
	}
}
