using System.Text.RegularExpressions;

namespace algo_assessment
{
    public class WordAnalyzer
    {
        private string fileName;
        private string outputFileName;
        private string[] linesInFile;
        private BinarySearchTree wordTree; // added declaration

        public WordAnalyzer(string fileName, string outputFileName)
        {
            this.fileName = fileName;
            this.outputFileName = outputFileName;
            linesInFile = File.ReadAllLines(fileName);
            wordTree = new BinarySearchTree();
            ProcessWords();
        }

        // ProcessWords builds the word tree from the file content.
        private void ProcessWords()
        {
            char[] delimiters = { ' ', ',', '"', ':', ';', '?', '!', '-', '.', '\'', '*' };
            foreach (string line in linesInFile)
            {
                string[] wordsInLine = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in wordsInLine)
                {
                    if (IsWord(word))
                        wordTree.AddWord(word);
                }
            }
        }

        private bool IsWord(string str)
        {
            return Regex.IsMatch(str, @"\b(?:[a-z]{2,}|[ai])\b", RegexOptions.IgnoreCase);
        }

        public void WriteUniqueWordsToFile()
        {
            File.WriteAllLines(outputFileName, wordTree.GetUniqueWords());
            Console.WriteLine($"Unique words have been saved to {outputFileName}");
        }

        public void DisplayUniqueWordCount()
        {
            Console.WriteLine("Number of unique words: " + wordTree.Count);
        }

        public void DisplayWordFrequencies()
        {
            Console.WriteLine("\nWord frequencies:");
            foreach (var (word, frequency) in wordTree.GetWordsOrderedBy(false))
                Console.WriteLine($"{word}: {frequency}");
        }

        public void DisplayWordFrequenciesAscending()
        {
            Console.WriteLine("\nWord frequencies ascending:");
            foreach (var (word, frequency) in wordTree.GetWordsOrderedBy(true))
                Console.WriteLine($"{word}: {frequency}");
        }

        public void DisplayWordFrequenciesDescending()
        {
            Console.WriteLine("\nWord frequencies descending:");
            foreach (var (word, frequency) in wordTree.GetWordsOrderedBy(false))
                Console.WriteLine($"{word}: {frequency}");
        }

        public void DisplayLongestWord()
        {
            var uniqueWords = wordTree.GetUniqueWords();
            var longestWord = uniqueWords.OrderByDescending(w => w.Length).First();
            Console.WriteLine($"\nLongest word: {longestWord} ({longestWord.Length} characters, frequency: {wordTree.GetWordFrequency(longestWord)})");
        }

        public void DisplayMostFrequentWord()
        {
            var mostFrequent = wordTree.GetWordsOrderedBy(false).First();
            Console.WriteLine($"\nMost frequent word: {mostFrequent.word} ({mostFrequent.frequency} times)");
        }

        public void DisplayWordLineNumbers(string searchWord)
        {
            searchWord = searchWord.ToLower();
            Console.WriteLine($"\nSearching for '{searchWord}' in {fileName}:");
            char[] delimiters = { ' ', ',', '"', ':', ';', '?', '!', '-', '.', '\'', '*' };
            bool found = false;

            for (int i = 0; i < linesInFile.Length; i++)
            {
                string[] wordsInLine = linesInFile[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in wordsInLine)
                {
                    if (IsWord(word) && word.ToLower() == searchWord)
                    {
                        found = true;
                        Console.WriteLine($"Found at line {i + 1}: {linesInFile[i].Trim()}");
                    }
                }
            }
            if (!found)
                Console.WriteLine($"Word '{searchWord}' not found in the file.");
        }

        public void DisplaySpecificWordFrequency(string specificWord)
        {
            int frequency = wordTree.GetWordFrequency(specificWord);
            if (frequency > 0)
                Console.WriteLine($"\nFrequency of {specificWord}: {frequency}");
            else
                Console.WriteLine($"\n{specificWord} not found in {fileName}");
        }
    }
}
