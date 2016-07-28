using System;
using System.IO;

namespace IcebergsExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Change with YOUR path
            string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.json");
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.html");

            Extractor extractor = new Extractor();
            extractor.Compute(inputPath, outputPath);

            Console.WriteLine("Press a key...");
            Console.ReadKey();

            //Open the webpage
            System.Diagnostics.Process.Start(outputPath);
        }
    }
}
