using System;

namespace SolutionForNNTC.Tests
{
    class BornInThe21CenturyCreatePersonTest : Test
    {
        public override void RunTest()
        {
            var creator = new PersonCreator();
            CreatePerson_Test(creator);
        }
        private void CreatePerson_Test(PersonCreator creator)
        {
            _testItem = "тест метода: " + nameof(creator.CreateEntity);
            if (creator.CreateEntity() == null
                || (creator.CreateEntity() as BornInThe21stCentury) == null)
            {
                throw new Exception(_testItem + _errorMessage);
            }
            PrintMessage(_testItem + _successMessage);
        }
    }
}
