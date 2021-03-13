using System;

namespace SolutionForNNTC
{
	class BornInThe21stCentury : IComparable
	{
		private string _name;
		private DateTime _birthday;
		private IValidate<string> _validateName;
		public bool selectorForCompare { get; set; }
		public string Name 
		{
			get
			{
				return _name;
			}
			set
			{
				//ToDo...на данный момент никакого механизма валидации введенного имени не реализовано
				if (_validateName?.TryValidate(value) == false)
				{
					throw new ArgumentException();
				}
				_name = value;
			}
		}
		public DateTime Birthday 
		{
			get
			{
				return _birthday;
			}
			set
			{
				ThrowHelper.Throw_IfOutOfRange("Дата рождения", value, new DateTime(2000, 1, 1), new DateTime(2099, 12, 31));
				_birthday = value;
			}
		}

		public BornInThe21stCentury()
		{
			_name = NamesStore.names[0];
			_birthday = new DateTime(2000, 1, 1);
		}
		public BornInThe21stCentury(string name, DateTime birthday, IValidate<string> validateName = null)
		{
			_validateName = validateName;
			Name = name;
			Birthday = birthday;
		}
		public override string ToString()
		{
			return $"name: {Name}\t\tbirthday: {Birthday:dd.MM.yyyy}"; 
		}

        public int CompareTo(object obj)
        {
			var comparableItem = obj as BornInThe21stCentury;

			if(obj is null)
			{
				throw new InvalidCastException();
			}
			if (selectorForCompare)
			{
				return Name.CompareTo(comparableItem.Name);
			}
			else
				return Birthday.CompareTo(comparableItem.Birthday);
        }
    }
}
