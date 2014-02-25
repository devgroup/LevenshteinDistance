using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LevenshteinDistance
{
    class LevenshteinDistance
    {
        
        
        private string pathToFileName;
        const string SENTINEL = "END OF INPUT";

        public bool IsLD1(string word, string dictWord)
        {
          
            int dist = 0;
            if (word.Length == dictWord.Length)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] != dictWord[i])
                    {
                        dist++;
                        if (dist > 1)
                        {
                            return false;
                        }
                    }
                }
                if (dist == 1)
                {
                    return true;
                }
                
            }
            string longerWord = string.Empty;
            string shorterWord = string.Empty;
            if (word.Length == dictWord.Length + 1) 
            {
                shorterWord = word;
                longerWord = dictWord;
            }
            if (word.Length == dictWord.Length - 1)
            {
                shorterWord = dictWord;
                longerWord = word;
            }

            if (shorterWord != null && longerWord != null)
            {
                for (int i = 0; i < longerWord.Length; i++)
                {
                    if (longerWord.Remove(i) == shorterWord)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;

        }
        public LevenshteinDistance(string PathToFileName)
        {
            //wordList = new List<Tuple<int, List<string>>>();
            this.pathToFileName = PathToFileName;
            this.loadDictionary();
        }

        public void ProcessInput()
        {
          //  using (StreamWriter sw = new StreamWriter("results.txt"))
            using (StreamReader sr = new StreamReader(pathToFileName))
            {
                string line = String.Empty;
                while ((line = sr.ReadLine()) != SENTINEL)
                {
                    int count = this.GenerateList(line);
                  //  sw.WriteLine(count.ToString());
                    Console.WriteLine("{0}", count);

                }
            }
        }

        private List<Tuple<int, string>> wordDictionary;
        private void loadDictionary()
        {
            wordDictionary = new List<Tuple<int, string>>();
            string line = String.Empty;
            bool sentinelReached = false;
            using (StreamReader sr = new StreamReader(pathToFileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == SENTINEL)
                    {
                        sentinelReached = true;
                    }
                    if (sentinelReached)
                    {
                        wordDictionary.Add(new Tuple<int, string>(line.Length,line));
                    }
                }

            }
        }
        private List<string> getWordsFromDict(int wordCount)
        {
            return wordDictionary
                .Where(e => e.Item1 == wordCount)
                .Select(e => e.Item2).ToList();
        }
        private void removeWordfromDict(string word)
        {
            wordDictionary.Remove(wordDictionary.Where(e => e.Item2 == word).FirstOrDefault());
        }
        public int GenerateList(string testWord)
        {
            this.loadDictionary();
            removeWordfromDict(testWord);
            List<WordListItem> results = new List<WordListItem>();
            WordListItem wordListItem = new WordListItem()
            {
                FoundFriends = false,
                Word = testWord,
                WordLength = testWord.Length
            };

            results.Add(wordListItem);
           
          
            //Load dictionary with words of same length
          
            #region commentedOut
             /*
            while((wordListItem = results
                                    .Where(e => !e.FoundFriends).FirstOrDefault()
                                        ) != null)
            {

                results.AddRange(
                    this.wordDictionary
                    .Where(e => !results.Select(f => f.Word).Contains(e.Item2))
                    
                    .Where( e=> (Math.Abs(e.Item2.Length - wordListItem.WordLength) <= 1)
                    
                    )
                    .Where(e => this.IsLD1(wordListItem.Word, e.Item2)).Select(e =>
                        new WordListItem()
                        {
                            Word = e.Item2,
                            FoundFriends = false,
                            WordLength = e.Item2.Length
                        }
                        ).ToList()
                );
                wordListItem.FoundFriends = true;
                */
            #endregion

             

            while ((wordListItem = results
                                   .Where(e => !e.FoundFriends).FirstOrDefault()
                                       ) != null)
          {
            #region foreach
            
                foreach (string dictionaryWord in getWordsFromDict(wordListItem.WordLength))
                {

                    if (IsLevenshteinDistanceOneSameWordLength(wordListItem.Word, dictionaryWord))
                    {
                        results.Add(new WordListItem()
                            {
                                FoundFriends = false,
                                Word = dictionaryWord,
                                WordLength = dictionaryWord.Length
                            });

                        Console.WriteLine(dictionaryWord);
                        removeWordfromDict(dictionaryWord);

                    }
                }
             #endregion
                #region foreach
                foreach (string dictionaryWord in
                    getWordsFromDict(wordListItem.WordLength - 1).Union(getWordsFromDict(wordListItem.WordLength + 1)))
                {
                    string shorterWord = string.Empty;
                    string longerWord = string.Empty;

                    if (dictionaryWord.Length < wordListItem.WordLength)
                    {
                        shorterWord = dictionaryWord;
                        longerWord = wordListItem.Word;
                    }
                    else
                    {
                        shorterWord = wordListItem.Word;
                        longerWord = dictionaryWord;
                    }
                    if (IsLevenshteinDistanceOneDifferentWordLength(shorterWord, longerWord))
                    {
                        results.Add(new WordListItem()
                        {
                            FoundFriends = false,
                            Word = dictionaryWord,
                            WordLength = dictionaryWord.Length
                        });

                        Console.WriteLine(dictionaryWord);
                        removeWordfromDict(dictionaryWord);
                    }
                }

                #endregion

                wordListItem.FoundFriends = true;
            }//while
           

            return results.Count;
        }
        
        /*
        private List<Tuple<int, List<string>>> wordList;
        private List<string> getWordList(int wordLength)
        {
            if (wordLength < 1)
                return null;

            var result = wordList
                           .Where(e => e.Item1 == wordLength)
                           .FirstOrDefault();
            if (result == null)
            {

                List<string> addWordList =  this.loadWordList(wordLength);
                wordList.Add(new Tuple<int, List<string>>
                (wordLength, addWordList));
                Console.WriteLine("Adding {0} word length to dictionary", wordLength);
                Console.ReadLine();

               return addWordList;
            }

            return result.Item2;
        }
         * */
        //private void RemoveWordFromWordList(string wordToRemove)
        //{
        //    //getWordList(wordToRemove.Length).Remove(wordToRemove);
        //}
        private List<string> loadWordList(int wordLength)
        {
            string line;
            bool SentinelReached = false;
            List<string> result = new List<string>();
            using (StreamReader sr = new StreamReader(pathToFileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == SENTINEL)
                    {
                        SentinelReached = true;
                    }
                    if (SentinelReached && (line.Length == wordLength))
                    {
                        result.Add(line);
                    }
                }

            }
            return result;

        }

        private bool IsLevenshteinDistanceOneDifferentWordLength(string shorterWord, string longerWord)
        {
            for (int i = 0; i < longerWord.Length; i++)
            {
                if (longerWord.Remove(i) == shorterWord)
                {
                    return true;
                }
            }
            return false;

        }
        private bool IsLevenshteinDistanceOneSameWordLength(string testWord, string dictionaryWord)
        {
            int distCost = 0;
            for (int i = 0; i < testWord.Length; i++)
            {
              
                if (testWord[i] != dictionaryWord[i])
                {
                    distCost++;
                }
                if (distCost > 1)
                {
                    return false;
                }
            }
            return (distCost == 1);
        }

    }

    public class WordListItem
    {
        internal string Word { get; set; }
        internal bool FoundFriends { get; set; }
        internal int WordLength { get; set; }
    }
}
