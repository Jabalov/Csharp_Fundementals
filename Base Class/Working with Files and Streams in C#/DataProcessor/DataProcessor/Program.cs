using System;
using static System.Console;
namespace DataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Parsing Command line Options");
            var command = args[0];

            if (command == "--file")
            {
                var path = args[1];
                WriteLine($"Single file being Processed: {path}");
                ProcessSingleFile(path);
            }
            else if (command == "--dir")
            {
                var directoryPath = args[1];
                var fileType = args[2];
                WriteLine($"Directory {directoryPath} selected for {fileType} files");
                ProcessDirectory(directoryPath, fileType);
            }
            else
            {
                WriteLine("Invalid command line options");
            }

            WriteLine("Press enter to quit.");
            ReadLine();
        }

        private static void ProcessDirectory(string directoryPath, string fileType)
        {
            
        }

        private static void ProcessSingleFile(string path)
        {
            var fileProcessor = new FileProcessor(path);
            fileProcessor.Process();
        }
    }
}
