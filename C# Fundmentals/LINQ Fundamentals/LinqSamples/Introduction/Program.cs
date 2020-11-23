using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows";

            ShowLargeFilesWithoutLinq(path);

            Console.WriteLine("***");

            ShowLargeFilesWithLinq(path); 
        }

        private static void ShowLargeFilesWithLinq(string path)
        {
            /*
            var query = from file in new DirectoryInfo(path).GetFiles() 
                        orderby file.Length descending
                        select file;
            */

            var query = new DirectoryInfo(path).GetFiles()
                        .OrderByDescending(file => file.Length)
                        .Take(5);

            foreach(var file in query.Take(5))
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            }
        }


        private static void ShowLargeFilesWithoutLinq(string path)
        {
            var Directory = new DirectoryInfo(path);
            FileInfo[] files =  Directory.GetFiles();

            Array.Sort(files, new FileInfoComparer());

            int N = 0;
            foreach(FileInfo file in files)
            {
                if (N == 5) break;
                Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");
                N++;
            }
        }

        public class FileInfoComparer : IComparer<FileInfo>
        {
            public int Compare(FileInfo x, FileInfo y)
            {
                return y.Length.CompareTo(x.Length);
            }
        }
    }
}
