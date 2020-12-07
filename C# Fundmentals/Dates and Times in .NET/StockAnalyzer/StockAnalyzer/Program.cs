using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace StockAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var Lines = File.ReadAllLines(@"C:\Users\user\Desktop\SelfStudy\C# Fundementals\Dates and Times in .NET\StockAnalyzer\StockAnalyzer\StockData.csv");

            foreach (var line in Lines.Skip(1))
            {
                var segments = line.Split(',');
                var tradeDate = DateTime.Parse(segments[1], CultureInfo.GetCultureInfo("en-GB"));

                Console.WriteLine(tradeDate.ToLongDateString());
            }
        }
    }
}
