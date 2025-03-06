namespace algo_assessment
{
    internal class Program
    {
        static string fileName = "sherlock.txt"; //file to read
        static string outputFileName = "unique_words.txt"; //file to write unique words

        static void Main(string[] args)
        {
            WordAnalyzer analyzer = new WordAnalyzer(fileName, outputFileName);
            analyzer.WriteUniqueWordsToFile();
            analyzer.DisplayUniqueWordCount();
            analyzer.DisplayWordFrequencies();
            analyzer.DisplayWordFrequenciesAscending();
            analyzer.DisplayWordFrequenciesDescending();
            analyzer.DisplayLongestWord();
            analyzer.DisplayMostFrequentWord();
            analyzer.DisplayWordLineNumbers("Sherlock");
            analyzer.DisplaySpecificWordFrequency("Sherlock");
        }
    }
}