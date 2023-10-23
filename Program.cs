using System.Collections.Generic;

namespace HomeWorkLINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var newList = string.Join(",", list.Top(30));
            Console.WriteLine(newList);
            var personList = new List<Person>()
            {
                new Person(1),
                new Person(2),
                new Person(3),
                new Person(4),
                new Person(5),
                new Person(6),
                new Person(7),
                new Person(8),
                new Person(9)
            };
            var newPersonList = string.Join(",", personList.Top(30, person => person.Age).Select(person=> person.Age));
            Console.WriteLine(newPersonList);
        }
    }

    public class Person
    {
        public Person(int age)
        {
            Age = age;
        }

        public int Age { get; set; }
    }
    public static class EnumerableExtension
    {
        public static IEnumerable<T> Top<T>(this IEnumerable<T> source, int percent) => Top(source, percent, x => x);
       public static IEnumerable<T> Top<T, TK>(this IEnumerable<T> source, int percent, Func<T, TK> predicate)
        {
           if (source is null)
               throw new ArgumentNullException(nameof(source));
           if (percent is < 1 or > 100)
               throw new ArgumentOutOfRangeException(nameof(percent), percent, "Параметр должен быть в диапазоне от 1 до 100.");

           var elementsCount = (int)Math.Ceiling(source.Count() / 100.0 * percent);
           return source.OrderByDescending(predicate).Take(elementsCount);
        }
    }
}