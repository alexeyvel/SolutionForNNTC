using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SolutionForNNTC.Tests
{
	class TestsPool : IEnumerable
	{
		private Test[] _testsPool;
		public TestsPool()
		{
			_testsPool = new Test[]
			{
				new BornInThe21CenturyCreatePersonTest(),
				new CheckBirthDayTest()
			};
		}
		public IEnumerator GetEnumerator()
		{
			return _testsPool.GetEnumerator();
		}
	}
}
