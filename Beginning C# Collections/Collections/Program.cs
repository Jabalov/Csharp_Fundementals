using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class Program
    {

        static void Main(string[] args)
        {
            // Population Demo
            string file_path = @".\Pop by Largest Final.csv";
            CsvReader reader = new CsvReader(file_path);

            Country[] countries = reader.readFirstNCountries(10);

            foreach (Country c in countries)
                Console.WriteLine($"{PopulationFormatter.FormatPopulation(c.Population).PadLeft(15)} : {c.Name}");
        }
    }
}
