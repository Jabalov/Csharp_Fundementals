using System;


namespace GradeBook
{
    class Program
    {
        // static void OnGradeAdded(object sender, EventArgs e)
        // {
        //     Console.WriteLine("A Grade Added");
        // }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter The Book's Name:");
            string name = Console.ReadLine();
            Book book = new InDiskBook(name);

            // book.GradeAdded += OnGradeAdded; // invoking the Event function
            // Looping to get the data, An Example on 
            EnteringGrades(book);

            Statistics s = book.getStatistics();
            Console.WriteLine($"The Letter is: {s.Letter}");
        }

        private static void EnteringGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or inter 'q' to quit");
                var input = Console.ReadLine();

                if (input == "q")
                    break;

                try
                {
                    book.AddGrade(double.Parse(input));
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
            }
        }
    }
}
