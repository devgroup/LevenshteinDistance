using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace LevenshteinDistance
{
    class Program
    {
        /// <summary>
        /// https://www.codeeval.com/public_sc/58/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string pathToFilename = "InputFile.txt";
            if (args[0] == null)
            {
                pathToFilename = args[0];
            }
            //Run the commented code to generate a sample file.

            //Helper helper = new Helper();
            //helper.GenerateSampleFile();
            //return;
            //main program
            LevenshteinDistance ld = new LevenshteinDistance(pathToFilename);
          //  ld.ProcessInput();

          

            
            
            //Use the commented code below to run interactively at the console.
            Console.WriteLine("Find all words one Levenshtein distance away");
            string input = string.Empty;
            Console.WriteLine("Enter your word or Q to quit");
            while ((input = Console.ReadLine()).ToLower() != "q")
            {


                Console.WriteLine("The count is {0}", ld.GenerateList(input));
                Console.WriteLine("Enter your word or Q to quit");
            }
            Console.ReadLine();

            
        }
    }
}
