using System;
using System.Collections.Generic;
using System.Text;

namespace QueryIt
{
    public class Person
    {
        public string Name { get; set; }
    }

    public class Employee : Person, IEntity
    {
        public int Id { get; set; }
        public virtual void DoWork()
        {
            Console.WriteLine("Doing Work");
        }

        public bool IsValid()
        {
            return true;
        }
    }

    public class Manager : Employee
    {
        public override void DoWork()
        {
            Console.WriteLine("Create a Meeting");
        }
    }

    public interface IEntity
    {
        bool IsValid();
    }
}
