using System;
using System.Linq;

namespace LinqToObjects
{
    class Program
    {
        static bool NameLogerThanFour(string name)
        {
            return name.Length > 4;
        }

        static void Main()
        {
            var names = new[] { "Michael", "Pam", "Jim", "Dwight", "Angela", "Kevin", "Toby", "Creed" };
            //var query = names.Where(NameLogerThanFour);
            var query = names
                .Where(name => name.Length > 4)
                .OrderBy(name => name.Length)
                .ThenBy(name => name);

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
    }
}
