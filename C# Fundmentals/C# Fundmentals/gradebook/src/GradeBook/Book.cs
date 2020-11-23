using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);
    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public String Name { get; set; }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics getStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        protected Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics getStatistics();
    }

    public class InDiskBook : Book
    {
        public InDiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                if (grade <= 100 && grade >= 0)
                {
                    writer.WriteLine(grade);
                    if (GradeAdded != null)
                        GradeAdded(this, new EventArgs());
                }
                else
                    throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public override Statistics getStatistics()
        {
            var result = new Statistics();

            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);

                    line = reader.ReadLine(); //iterator 
                }
            }

            return result;
        }
    }
    public class InMemoryBook : Book
    {
        public override event GradeAddedDelegate GradeAdded;

        private List<double> grades;
        public readonly string description;
        public const string CATEGORY = "";



        public InMemoryBook(string name) : base(name)
        {
            description = "";
            grades = new List<double>();
            Name = name;
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                    GradeAdded(this, new EventArgs());
            }
            else
                throw new ArgumentException($"Invalid {nameof(grade)}");
        }

        public override Statistics getStatistics()
        {
            var res = new Statistics();

            foreach (var grade in grades)
            {
                res.Add(grade);
            }

            return res;
        }
    }
}