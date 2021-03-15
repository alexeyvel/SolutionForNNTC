using SolutionForNNTC.ConsoleCmd;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionForNNTC.Tests
{
	class CheckBirthDayTest : Test
	{
		public override void RunTest()
		{
			var persons = ApplicationCore.CreateApp().ItemList;
			CheckBirthDay_Test(persons);
		}
		private void CheckBirthDay_Test(IEnumerable<BornInThe21stCentury> persons)
		{
			_testItem = "тест корректности создания данных в списке: ";
			bool result = false;
			foreach(var person in persons)
			{
				result |= (person.Birthday < new DateTime(2000, 1, 1)
					|| person.Birthday > new DateTime(2099, 12, 31));				
			}
			if(result)
			{
				throw new Exception(_testItem + _errorMessage);
			}
			PrintMessage(_testItem + _successMessage);
		}
	}
}
