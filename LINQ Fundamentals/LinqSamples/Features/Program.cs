using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> Square = x => x * x ;

            Func<int, int, int> Add = (x, y) => x + y;

            Action<int> write = (x => Console.WriteLine(x));

            write(Square(Add(3, 5)));

            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee {Id = 1, Name = "Muhammed"},
                new Employee {Id = 2, Name = "Samy"}
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee {Id = 1, Name = "Muhammed" }
            };

            IEnumerator<Employee> enumerator = developers.GetEnumerator();

            /*
            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            } */
            
            //Console.WriteLine($"Count: {developers.Count()}");

            foreach(Employee employee in developers.Where(e => e.Name.StartsWith("M")))
            {
                Console.WriteLine(employee.Name);
            }
            
        }
        /*
        private static bool NameStartsWithS(Employee employee)
        {
            return employee.Name.StartsWith("S");
        }
        */
    }
}
