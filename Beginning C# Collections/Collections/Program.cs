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

            List<Country> countries = reader.ReadAllCountries();

            foreach (var country in countries.Take(20).Where(x => !x.Name.Contains(',')))
            {
                Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");

            }
        }
    }
}
