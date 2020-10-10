using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Collections
{
    internal class CsvReader
    {
        private string _csvFilePath;

        public CsvReader(string file_path)
        {
            this._csvFilePath = file_path;
        }

        public Country[] readFirstNCountries(int nCountries)
        {
            Country[] countries = new Country[nCountries];

            using (StreamReader sr = new StreamReader(_csvFilePath))
            {
                sr.ReadLine(); //Header

                for (int i = 0; i < nCountries; i++)
                {
                    string line = sr.ReadLine();
                    countries[i] = ParseCsvLine(line);
                }
            }
            return countries;
        }

        public Country ParseCsvLine(string csvLine)
        {
            string[] components = csvLine.Split(new char[] { ',' });

            string name = components[0];
            string code = components[1];
            string region = components[2];
            int population = int.Parse(components[3]);

            return new Country(name, code, region, population);
        }
    }
}