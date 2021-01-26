using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankHolidays
{
    class Program
    {
        static void Main(string[] args)
        {

            var bankHols1 = new List<DateTime>
            {
                new DateTime(2021, 1, 1),
                new DateTime(2021, 4, 2),
                new DateTime(2021, 4, 5),
                new DateTime(2021, 5, 3),
                new DateTime(2021, 5, 31),
                new DateTime(2021, 8, 30),
                new DateTime(2021, 12, 27),
                new DateTime(2021, 12, 28),
            };
            bankHols1.Add(new DateTime(2021, 1, 1));

            List<DateTime> bankHols2 = new List<DateTime>()
            {
                new DateTime(2021, 1, 1),
                new DateTime(2021, 4, 2),
                new DateTime(2021, 4, 5),
                new DateTime(2021, 5, 3),
                new DateTime(2021, 5, 31),
                new DateTime(2021, 8, 30),
                new DateTime(2021, 12, 27),
                new DateTime(2021, 12, 28),
            };

            Console.WriteLine($"bn1==bh2? {bankHols1.SequenceEqual(bankHols2)}");
        }
    }
}
