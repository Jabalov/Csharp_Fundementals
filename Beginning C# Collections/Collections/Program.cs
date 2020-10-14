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

            Dictionary<string, Country> countries = reader.ReadAllCountries();

            // foreach (Country country in countries.Values)
            // {
            //     Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
            // }
            // Console.WriteLine(countries.Count);

            Console.WriteLine("Which country code do you want to look up? ");
            string userChoice = Console.ReadLine();

            bool countryExists = countries.TryGetValue(userChoice, out Country country);
            if (!countryExists)
                Console.WriteLine($"Sorry, there is no country with code, {userChoice}");
            else
                Console.WriteLine($"{country.Name} has population {PopulationFormatter.FormatPopulation(country.Population)}");

        }
    }
}
