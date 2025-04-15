namespace algo_assessment
{
    internal class Program
    {
        static string fileName = "sherlock.txt"; //file to read
        static string outputFileName = "unique_words.txt"; //file to write unique words

        static void Main(string[] args)
        {
            WordAnalyzer analyzer = new WordAnalyzer(fileName, outputFileName);
            
            // Saving the unqiue words to a file before launching the menu
            analyzer.WriteUniqueWordsToFile();
            Console.WriteLine($"Unique words saved to {outputFileName}");
            Console.WriteLine();

            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        analyzer.DisplayUniqueWordCount();
                        break;
                    case "2":
                        DisplayUniqueWords(outputFileName);
                        break;
                    case "3":
                        analyzer.DisplayWordFrequencies();
                        break;
                    case "4":
                        analyzer.DisplayWordFrequenciesAscending();
                        break;
                    case "5":
                        analyzer.DisplayWordFrequenciesDescending();
                        break;
                    case "6":
                        analyzer.DisplayLongestWord();
                        break;
                    case "7":
                        analyzer.DisplayMostFrequentWord();
                        break;
                    case "8":
                        Console.Write("Enter word to find line numbers for: ");
                        string? wordToFind = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(wordToFind))
                        {
                            analyzer.DisplayWordLineNumbers(wordToFind);
                        }
                        break;
                    case "9":
                        Console.Write("Enter word to check frequency: ");
                        string? wordToCheck = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(wordToCheck))
                        {
                            analyzer.DisplaySpecificWordFrequency(wordToCheck);
                        }
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Exiting program. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        // Displays the main menu options to the user
        static void DisplayMenu()
        {
            Console.WriteLine("===== Word Analysis Menu =====");
            Console.WriteLine("1. Display unique word count");
            Console.WriteLine("2. Display unique words");
            Console.WriteLine("3. Display word frequencies");
            Console.WriteLine("4. Display word frequencies (ascending)");
            Console.WriteLine("5. Display word frequencies (descending)");
            Console.WriteLine("6. Display longest word");
            Console.WriteLine("7. Display most frequent word");
            Console.WriteLine("8. Display line numbers for a specific word");
            Console.WriteLine("9. Display frequency for a specific word");
            Console.WriteLine("0. Exit");
            Console.Write("\nEnter your choice: ");
        }

        // Displays the unique words from the output file
        
        static void DisplayUniqueWords(string outputFile)
        {
            try
            {
                string[] uniqueWords = File.ReadAllLines(outputFile);
                Console.WriteLine($"Unique words from {outputFile}:");
                
                // Display words with pagination if there are many
                const int pageSize = 20;
                for (int i = 0; i < uniqueWords.Length; i++)
                {
                    Console.WriteLine(uniqueWords[i]);
                    
                    // Pause after each page except the last one
                    if ((i + 1) % pageSize == 0 && i + 1 < uniqueWords.Length)
                    {
                        Console.WriteLine($"\nShowing {i + 1} of {uniqueWords.Length} words. Press any key for more...");
                        Console.ReadKey();
                    }
                }
                
                Console.WriteLine($"\nTotal: {uniqueWords.Length} unique words");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading unique words file: {ex.Message}");
            }
        }
    }
}