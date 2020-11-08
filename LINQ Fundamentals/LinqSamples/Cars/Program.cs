using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessCar("fuel.csv");
            var manufacturers = ProcessManufacturer("manufacturers.csv");

            var query = from car in cars
                        join manufacturer in manufacturers 
                            on  
                                new { car.Manufacturer, car.Year } 
                            equals 
                                new { Manufacturer = manufacturer.Name, manufacturer.Year }
                        orderby car.Combined descending, car.Name ascending
                        select new                                                  // Anonymous Type
                        {
                            manufacturer.Headquarters,
                            car.Name,
                            car.Combined
                        };

            var query2 = cars.Join(manufacturers,
                                    c => new { c.Manufacturer, c.Year },
                                    m => new { Manufacturer = m.Name, m.Year },
                                    (c, m) => new
                                    {
                                        m.Headquarters,
                                        c.Name,
                                        c.Combined
                                    });
            var query3 =
                from car in cars
                group car by car.Manufacturer into carGroup
                select new
                {
                    Name = carGroup.Key,
                    Max = carGroup.Max(c => c.Combined),
                    Min = carGroup.Min(c => c.Combined),
                    Avg = carGroup.Average(c => c.Combined)
                } into result
                orderby result.Max descending
                select result;


            var query4 =
                cars.GroupBy(c => c.Manufacturer)
                    .Select(g =>
                    {
                        var results = g.Aggregate(
                                                new CarStatistics(),
                                                (acc, c) => acc.Accumulate(c),
                                                acc => acc.Compute());
                                                return new
                                                {
                                                    Name = g.Key,
                                                    Avg = results.Average,
                                                    Min = results.Min,
                                                    Max = results.Max
                                                };
                    })
                    .OrderByDescending(r => r.Max);

            foreach (var result in query4)
            {
                Console.WriteLine($"{result.Name}");
                Console.WriteLine($"\t Max: {result.Max}");
                Console.WriteLine($"\t Min: {result.Min}");
                Console.WriteLine($"\t Avg: {result.Avg}");
            }
        }

        private static List<Manufacturer> ProcessManufacturer(string path)
        {
            var query = File.ReadAllLines(path)
                            .Where(l => l.Length > 1)
                            /*.Select(l =>
                            {
                                var columns = l.Split(',');
                                return new Manufacturer
                                {
                                    Name = columns[0],
                                    Headquarters = columns[1],
                                    Year = int.Parse(columns[2])
                                };
                            }) */
                            .ToManufacturer()
                            .ToList();

            return query;
        }

        private static List<Car> ProcessCar(string path)
        {
            var query = File.ReadAllLines(path)
                            .Skip(1)
                            .Where(l => l.Length > 1)
                            .ToCar()
                            .ToList();

            return query;
        }
    }



}
