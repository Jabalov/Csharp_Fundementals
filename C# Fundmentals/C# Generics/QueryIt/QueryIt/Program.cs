using System;
using System.Data.Entity;

namespace QueryIt
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<EmployeeDb>());

            using (IRepository<Employee> repository = 
                    new SqlRepository<Employee>(new EmployeeDb()))
            {
                AddEmployees(repository);
                CountEmpoyees(repository);
                QueryEmployee(repository);
                DumpPeople(repository);
                AddManagers(repository);
            }
        }

        private static void AddManagers(IWriteOnlyRepository<Manager> repository)
        {
            repository.Add(new Manager() { Name = "Hala" });
        }

        private static void DumpPeople(IReadOnlyRepository<Person> repository)
        {
            var query = repository.FindAll();
            foreach(var employee in query)
            {
                Console.WriteLine(employee.Name);
            }
        }

        private static void QueryEmployee(IRepository<Employee> repository)
        {
            Console.WriteLine(repository.FindById(1).Name);
        }

        private static void CountEmpoyees(IRepository<Employee> repository)
        {
            Console.WriteLine(repository.FindAll().ToListAsync().Result.Count);
        }

        private static void AddEmployees(IRepository<Employee> repository)
        {
            repository.Add(new Employee() { Name = "Muhammed"});
            repository.Add(new Employee() { Name = "Sami"});
            repository.Add(new Employee() { Name = "Samer"});
            repository.Commit();

        }
    }
}
