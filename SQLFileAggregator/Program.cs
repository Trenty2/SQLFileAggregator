using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var scriptPaths = args[0];
            var outputFilePath = args[1];
            var pathArray = scriptPaths.Split('|');
            const string sqlExtension = "*.sql";
            var fileStack = new Stack<string>();

            if (args.Length < 2 || args.Length == 0)
            {
                Console.WriteLine("## Issue with input ##");
                Console.WriteLine("Expecting an input as shown in examples below:");
                Console.WriteLine("SQLFileAggregator \"C:\\Users\\SomeUser\\Desktop\\locationOne\" \"C:\\Users\\SomeUser\\Desktop\\output\\combined.sql\"");
                Console.WriteLine("SQLFileAggregator \"C:\\Users\\SomeUser\\Desktop\\locationOne|C:\\Users\\Trent\\Desktop\\test\\locationTwo\" \"C:\\Users\\SomeUser\\Desktop\\output\\combined.sql\"");

                return;
            }

            if (!File.Exists(outputFilePath))
            {
                Console.WriteLine("Please create the output file");
                return;
            }


            foreach (var path in pathArray)
            {
                var files = Directory.GetFiles(path, sqlExtension);

                if (files.Length == 0)
                {
                    Console.WriteLine("No SQL files to operate on");
                    return;
                }

                foreach (var file in files)
                {
                    fileStack.Push(file);
                }

            }
            foreach (var filePath in fileStack)
            {
                var fileContent = File.ReadAllLines(filePath);
                File.AppendAllLines(outputFilePath, fileContent);
            }


        }


    }
}
