using System.Text.RegularExpressions;

namespace starterCode
{
    internal class Program
    {
        static string fileName = "sherlock.txt"; //file to read
        static string outputFileName = "unique_words.txt"; //file to write unique words
        static string[]? linesInFile;
        static void Main(string[] args)
        {
            readDisplayFileWords();
        }

        static void readDisplayFileWords()
        {
            linesInFile = File.ReadAllLines(fileName);
            int lineNumber = 0;
            int numberWords = 0;

            //3. Store the number of occurrences of each word in a dictionary
            LinkedList wordList = new LinkedList();

            char[] delimiters = { ' ', ',', '"', ':', ';', '?', '!', '-', '.', '\'', '*' };
            foreach (string line in linesInFile)
            {
                lineNumber++;
                string[] wordsInLine = line.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
                Console.Write(lineNumber + ":");
                foreach (string word in wordsInLine)
                {
                    if (isWord(word))
                    {
                        numberWords++;
                        wordList.AddWord(word);
                        Console.Write(word.ToLower() + ",");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine(fileName + " contains " + numberWords + " words.");

            //1.  Write unique words to file
            File.WriteAllLines(outputFileName, wordList.GetUniqueWords());
            Console.WriteLine($"Unique words have been saved to {outputFileName}");

            //2.  Display number of unique words
            Console.WriteLine("Number of unique words: " + wordList.Count);

            //4.  Display word frequency
            Console.WriteLine("\nWord frequencies:");
            foreach (var (word, freq) in wordList.GetWordsOrderedBy(false))
            {
                Console.WriteLine($"{word}: {freq}");
            }

             // 5a Display word frequency ascending
            Console.WriteLine("\nWord frequencies ascending:");
            foreach (var (word, freq) in wordList.GetWordsOrderedBy(true))
            {
                Console.WriteLine($"{word}: {freq}");
            }

            // 5b Display word frequency descending
            Console.WriteLine("\nWord frequencies descending:");
            foreach (var (word, freq) in wordList.GetWordsOrderedBy(false))
            {
                Console.WriteLine($"{word}: {freq}");
            }

            //6 Display longest word and its frequency
            var orderedWords = wordList.GetUniqueWords();
            var longestWord = orderedWords.OrderByDescending(w => w.Length).First();
            Console.WriteLine($"\nLongest word: {longestWord} ({longestWord.Length} characters, frequency: {wordList.GetWordFrequency(longestWord)})");

            //7 Display most frequent word and its frequency
            var mostFrequent = wordList.GetWordsOrderedBy(false).First();
            Console.WriteLine($"\nMost frequent word: {mostFrequent.word} ({mostFrequent.frequency} times)");

            //8 Display the file line number where a specific word is found
            string searchWord = "Sherlock";
            findLineNumbersOfWord(searchWord);

            // 9 Display the frequency of a specific word
            string specificWord = "Sherlock";
            int frequency = wordList.GetWordFrequency(specificWord);
            if (frequency > 0)
                Console.WriteLine($"\nFrequency of {specificWord}: {frequency}");
            else
                Console.WriteLine($"\n{specificWord} not found in {fileName}");
        }

        static Boolean isWord(string str)
        {
            return Regex.IsMatch(str, @"\b(?:[a-z]{2,}|[ai])\b", RegexOptions.IgnoreCase);
        }

        static void findLineNumbersOfWord(string searchWord)
        {
            searchWord = searchWord.ToLower();
            Console.WriteLine($"\nSearching for '{searchWord}' in {fileName}:");
            
            char[] delimiters = { ' ', ',', '"', ':', ';', '?', '!', '-', '.', '\'', '*' };
            bool found = false;
            
            for (int i = 0; i < linesInFile?.Length; i++)
            {
                string[] wordsInLine = linesInFile[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in wordsInLine)
                {
                    if (isWord(word) && word.ToLower() == searchWord)
                    {
                        found = true;
                        Console.WriteLine($"Found at line {i + 1}: {linesInFile[i].Trim()}");
                    }
                }
            }
            
            if (!found)
            {
                Console.WriteLine($"Word '{searchWord}' not found in the file.");
            }
        }
    }
}