using System;
using System.ComponentModel;

namespace SolutionForNNTC
{
    /// <summary>
	/// Статический класс вызова стандартных исключений
	/// </summary>
    static class ThrowHelper
    {
		/// <summary>
		///	Выдать исключение, если перечисления имеют значение по умолчанию
		/// </summary>
		/// <param name="paramName">Имя аргумента</param>
		/// <param name="arg">Значение</param>
		/// <param name="message">Строка сообщения</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="InvalidEnumArgumentException"></exception>
		public static void Throw_IfDefaultEnum<T>(string paramName, T arg, string message = null) where T : struct
		{
			Type t = typeof(T);
			if (!t.IsEnum)
			{
				throw new ArgumentException("Argument is not enum", nameof(arg));
			}
			if (arg.Equals(default(T)))
			{
				throw new InvalidEnumArgumentException("Value " + default(T) + " is not supported", Convert.ToInt32(arg), t);
			}
		}

		/// <summary>
		/// Выдать исключение, если аргумент не задан
		/// </summary>
		/// <param name="paramName">Имя аргумента</param>
		/// <param name="arg">Значение</param>
		/// <param name="message">Строка сообщения</param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void Throw_IfArgumentNull(string paramName, object arg, string message = "Cannot be NULL")
		{
			if (arg is null)
			{
				throw new ArgumentNullException(paramName, message);
			}
		}

		/// <summary>
		/// Выдать исключение, если аргумент не задан
		/// </summary>
		/// <param name="paramName">Имя аргумента</param>
		/// <param name="arg">Значение</param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void Throw_IfArgumentNull(string paramName, object arg)
		{
			Throw_IfArgumentNull(paramName, arg, null);
		}

		/// <summary>
		/// Выдать исключение, если аргумент вне определенных границ
		/// </summary>
		/// <typeparam name="T">Тип аргумента</typeparam>
		/// <param name="paramName">Имя аргумента</param>
		/// <param name="arg">Значение</param>
		/// <param name="minLimit">Нижняя граница</param>
		/// <param name="maxLimit">Верхняя граница</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static void Throw_IfOutOfRange<T>(string paramName, T arg, T minLimit, T maxLimit) where T : IComparable<T>
		{
			Throw_IfTooSmall(paramName, arg, minLimit);
			Throw_IfTooBig(paramName, arg, maxLimit);
		}

		/// <summary>
		/// Выдать исключение, если аргумент меньше границы
		/// </summary>
		/// <typeparam name="T">Тип аргумента</typeparam>
		/// <param name="paramName">Имя аргумента</param>
		/// <param name="arg">Значение</param>
		/// <param name="minLimit">Нижняя граница</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static void Throw_IfTooSmall<T>(string paramName, T arg, T minLimit) where T : IComparable<T>
		{
			if (arg.CompareTo(minLimit) < 0)
			{
				throw new ArgumentOutOfRangeException(paramName, arg, "Argument lower then " + minLimit);
			}
		}

		/// <summary>
		/// Выдать исключение, если аргумент больше границы
		/// </summary>
		/// <typeparam name="T">Тип аргумента</typeparam>
		/// <param name="paramName">Имя аргумента</param>
		/// <param name="arg">Значение</param>
		/// <param name="maxLimit">Верхняя граница</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static void Throw_IfTooBig<T>(string paramName, T arg, T maxLimit) where T : IComparable<T>
		{
			if (arg.CompareTo(maxLimit) > 0)
			{
				throw new ArgumentOutOfRangeException(paramName, arg, "Argument bigger then " + maxLimit);
			}
		}

		/// <summary>
		/// Выдать исключение, если аргумент не число (NaN)
		/// </summary>
		/// <param name="valueName">Имя параметра</param>
		/// <param name="value">Значение</param>
		public static void Throw_IfNaN(string valueName, float value)
		{
			if (float.IsNaN(value))
			{
				throw new ArgumentException("Cannot be NaN", valueName);
			}
		}

		/// <summary>
		///	Выдать исключение, если аргумент не число (NaN)
		/// </summary>
		/// <param name="valueName">Имя параметра</param>
		/// <param name="value">Значение</param>
		public static void Throw_IfNaN(string valueName, double value)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException("Cannot be NaN", valueName);
			}
		}
	}
}
