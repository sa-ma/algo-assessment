# Algo Assessment - Word Analyzer

This C# console application analyzes text files to extract unique words, calculate word frequencies, and provide various statistics about the text content. It uses a Binary Search Tree to efficiently store and manage the words found in the input file.

## How to Run

1. **Clone the repository or download the source code.**
2. **Ensure you have the .NET SDK installed** (The project targets .NET 9.0, but should be compatible with recent versions).
3. **Place the input text file** (e.g., `mobydick.txt`) in the build output directory (e.g., `bin/Debug/net9.0/`). If you want to use a different file, change the `fileName` variable in `Program.cs`.
4. **Open a terminal or command prompt** in the project's root directory (where the `.sln` file is).
5. **Build the project:**

    ```bash
    dotnet build
    ```

6. **Run the application:**

    ```bash
    dotnet run --project algo-assessment/algo-assessment.csproj
    ```

    Alternatively, navigate to the output directory (e.g., `algo-assessment/bin/Debug/net9.0/`) and run the executable directly:

    ```bash
    ./algo-assessment 
    ```

7. **Follow the on-screen menu options** to explore the word analysis features. The unique words will be automatically saved to `unique_words.txt` in the same directory upon startup.

## Input/Output Files

* **Input:** By default, the program reads from `mobydick.txt` located in the execution directory. This can be changed in `Program.cs`.
* **Output:** The program writes the list of unique words found (one per line, alphabetically sorted) to `unique_words.txt` in the execution directory.
