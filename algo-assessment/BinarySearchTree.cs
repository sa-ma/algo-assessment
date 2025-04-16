namespace algo_assessment
{
    public class BinarySearchTree
    {
        // Private node class for each element in the tree
        private class Node
        {
            // storing the word in lower case for comparison purposes
            public string Word { get; set; }
            public int Frequency { get; set; }
            public List<int> LineNumbers { get; private set; } // Store line numbers
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            // Creates a new node with the given word and initial line number
            public Node(string word, int lineNumber)
            {
                Word = word.ToLower();
                Frequency = 1;
                LineNumbers = new List<int> { lineNumber }; // Initialize with the first line number
                Left = null;
                Right = null;
            }
        }

        private Node? root;
        
        // Tracks the number of unique words in the tree
        public int Count { get; private set; }

        // Adds a word and its line number to the binary search tree
        public void AddWord(string word, int lineNumber)
        {
            word = word.ToLower();
            
            if (root == null)
            {
                root = new Node(word, lineNumber); 
                Count++;
                return;
            }

            AddWordRecursive(root, word, lineNumber); 
        }

        // recursive method to add a word and its line number
        private void AddWordRecursive(Node node, string word, int lineNumber)
        {
            int comparison = string.Compare(word, node.Word, StringComparison.OrdinalIgnoreCase);

            if (comparison == 0)
            {
                node.Frequency++;
                // Add line number if it's not already recorded for this word
                if (!node.LineNumbers.Contains(lineNumber))
                {
                    node.LineNumbers.Add(lineNumber);
                    node.LineNumbers.Sort(); // Keep line numbers sorted
                }
            }
            else if (comparison < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new Node(word, lineNumber); 
                    Count++;
                }
                else
                {
                    AddWordRecursive(node.Left, word, lineNumber); 
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new Node(word, lineNumber); 
                    Count++;
                }
                else
                {
                    AddWordRecursive(node.Right, word, lineNumber); // Pass line number
                }
            }
        }

        // Returns an array of all unique words in the tree, sorted alphabetically
        public string[] GetUniqueWords()
        {
            List<string> words = new List<string>();

            // this visit nodes in alphabetical order
            InOrderTraversal(root, words);
            return [.. words];
        }

        // Performs in-order traversal to collect words in alphabetical order
        static private void InOrderTraversal(Node? node, List<string> words)
        {
            if (node == null) return; 

            // Recursively traverse left subtree
            InOrderTraversal(node.Left, words);
            
            // Visit current node
            words.Add(node.Word);
            
            // Recursively traverse right subtree
            InOrderTraversal(node.Right, words);
        }

        // Returns all words and their frequencies, sorted by frequency
        public IEnumerable<(string word, int frequency)> GetWordsOrderedBy(bool ascending)
        {
            // collect all word-frequency pairs
            List<(string word, int frequency)> wordFrequencies = new List<(string, int)>();
            CollectWordFrequencies(root, wordFrequencies);

            // sorting for ascending or descending order
            return ascending
                ? wordFrequencies.OrderBy(x => x.frequency) 
                : wordFrequencies.OrderByDescending(x => x.frequency);
        }


        static private void CollectWordFrequencies(Node? node, List<(string word, int frequency)> wordFrequencies)
        {
            if (node == null) return;

            CollectWordFrequencies(node.Left, wordFrequencies);

            wordFrequencies.Add((node.Word, node.Frequency));

            CollectWordFrequencies(node.Right, wordFrequencies);
        }

        // get the frequency of a specific word
        public int GetWordFrequency(string word)
        {
            return GetWordFrequencyRecursive(root, word.ToLower());
        }

        // use recursion to find the frequency of a word
        static private int GetWordFrequencyRecursive(Node? node, string word)
        {
            if (node == null) return 0;

            int comparison = string.Compare(word, node.Word, StringComparison.OrdinalIgnoreCase);

            if (comparison == 0)
                return node.Frequency;
            else if (comparison < 0)
                return GetWordFrequencyRecursive(node.Left, word);
            else
                return GetWordFrequencyRecursive(node.Right, word);
        }

        private Node? FindNodeRecursive(Node? node, string word)
        {
            if (node == null) return null;

            int comparison = string.Compare(word, node.Word, StringComparison.OrdinalIgnoreCase);

            if (comparison == 0)
                return node;
            else if (comparison < 0)
                return FindNodeRecursive(node.Left, word);
            else
                return FindNodeRecursive(node.Right, word);
        }

        // Returns the list of line numbers for a specific word
        public List<int> GetLineNumbersForWord(string word)
        {
            Node? node = FindNodeRecursive(root, word.ToLower());
            return node?.LineNumbers ?? new List<int>(); // Return empty list if word not found
        }
    }
}