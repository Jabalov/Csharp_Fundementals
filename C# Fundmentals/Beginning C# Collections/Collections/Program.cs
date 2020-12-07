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

            Dictionary<string, List<Country>> countries = reader.ReadAllCountries();

            // foreach (var country in countries.Take(20))
            // {
            //     Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");

            // }

            foreach (string region in countries.Keys)
                Console.WriteLine(region);

            foreach (Country country in countries["Asia"])
                Console.WriteLine(country.Name);
        }
    }
}
