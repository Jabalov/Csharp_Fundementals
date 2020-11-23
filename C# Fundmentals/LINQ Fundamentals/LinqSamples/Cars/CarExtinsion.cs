using System;
using System.Collections.Generic;
using System.Text;

namespace Cars
{
    public static class CarExtinsion
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> file)
        {
            foreach (var line in file)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
