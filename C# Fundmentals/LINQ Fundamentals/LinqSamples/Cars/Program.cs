﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());
            InsertData();
            QueryData();
        }

        private static void QueryData()
        {
            var db = new CarDb();
            //db.Database.Log = Console.WriteLine;

            var query = db.Cars.GroupBy(c => c.Manufacturer)
                                .Select(g => new
                                {
                                    Name = g.Key,
                                    Cars = g.OrderByDescending(c => c.Combined).Take(2)
                                });

            var query2 = from car in db.Cars
                         group car by car.Manufacturer into manufacturer
                         select new
                         {
                             Name = manufacturer.Key,
                             Cars = (from car in manufacturer
                                     orderby car.Combined descending
                                     select car).ToList()
                         };

            foreach (var group in query)
            {
                Console.WriteLine($"{group.Name}");
                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name}: {car.Combined}");
                }
            }

        }

        private static void InsertData()
        {
            var db = new CarDb();
            var cars = ProcessCar("fuel.csv");

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }

        }

        private static void QueryXml()
        {
            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
            var document = XDocument.Load("fuel.xml");

            var query =
                from element in document.Element(ns + "Cars")?.Elements(ex + "Car") ?? Enumerable.Empty<XElement>()
                where element.Attribute("Manufacturer")?.Value == "BMW"
                select element.Attribute("Name").Value;

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static void CreateXml()
        {
            var records = ProcessCar("fuel.csv");

            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
            var document = new XDocument();

            var cars = new XElement(ns + "Cars",
                from record in records
                select new XElement(ex + "Car",
                                new XAttribute("Name", record.Name),
                                new XAttribute("Combined", record.Combined),
                                new XAttribute("Manufacturer", record.Manufacturer))
            );

            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));

            document.Add(cars);
            document.Save("fuel.xml");
        }

        private static List<Manufacturer> ProcessManufacturer(string path)
        {
            var query = File.ReadAllLines(path)
                            .Where(l => l.Length > 1)
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
