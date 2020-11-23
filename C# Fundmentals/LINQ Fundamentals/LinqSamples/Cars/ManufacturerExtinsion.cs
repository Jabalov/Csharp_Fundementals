using System;
using System.Collections.Generic;
using System.Text;

namespace Cars
{
    public static class ManufacturerExtinson
    {
        public static IEnumerable<Manufacturer> ToManufacturer(this IEnumerable<string> file)
        {
            foreach (var line in file)
            {
                var columns = line.Split(',');
                yield return new Manufacturer
                {
                    Name = columns[0],
                    Headquarters = columns[1],
                    Year = int.Parse(columns[2])
                };
            }
        }
    }
}
