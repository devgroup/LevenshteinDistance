using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace LevenshteinDistance
{
    public class Helper
    {
        public void GenerateSampleFile()
        {
            Random rand = new Random();
            int TestCaseCount = rand.Next(15, 31);
            List<string> wordDictionary = System.IO.File.ReadLines("WordsEn.txt").ToList();

            int DictLength = wordDictionary.Count;
            List<string> TestCases = new List<string>();
            for (int i = 0; i < TestCaseCount; i++)
            {
                TestCases.Add(wordDictionary[rand.Next(0, DictLength)]);
            }

            //Write to file InputFile.txt
            using (StreamWriter sw = new StreamWriter("InputFile.txt", append: false))
            {
                foreach (string testCase in TestCases)
                {
                    sw.WriteLine(testCase);
                }
                sw.WriteLine("END OF INPUT");
                foreach (string line in wordDictionary)
                {
                    sw.WriteLine(line);
                }
            }

        }
        
       
    }
}
