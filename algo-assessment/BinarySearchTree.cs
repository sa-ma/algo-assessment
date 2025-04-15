namespace algo_assessment
{
    public class BinarySearchTree
    {
        // Private node class for each element in the tree
        // Each node contains a word, its frequency, and references to left and right children.
        private class Node
        {
            // storing the word in lower case for comparison purposes
            public string Word { get; set; }
            public int Frequency { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            // Creates a new node with the given word and initializes frequency to 1
            // using 1 as frequency because it is a new word
            public Node(string word)
            {
                Word = word.ToLower();
                Frequency = 1;
                Left = null;
                Right = null;
            }
        }

        private Node? root;
        
        // Tracks the number of unique words in the tree
        public int Count { get; private set; }

        // Adds a word to the binary search tree or increments its frequency if it already exists
        public void AddWord(string word)
        {
            word = word.ToLower();
            
            // If tree is empty, create the root node
            if (root == null)
            {
                root = new Node(word);
                Count++;
                return;
            }

            AddWordRecursive(root, word);
        }

        // recursive method to add a word to the appropriate position in the tree
        private void AddWordRecursive(Node node, string word)
        {
            // this is where we decide where to place the new word
            int comparison = string.Compare(word, node.Word, StringComparison.OrdinalIgnoreCase);

            if (comparison == 0)
            {
                node.Frequency++;
            }

            else if (comparison < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new Node(word);
                    Count++;
                }
                else
                {
                    AddWordRecursive(node.Left, word);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new Node(word);
                    Count++;
                }
                else
                {
                    AddWordRecursive(node.Right, word);
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
            if (node == null) return;  // Base case: empty subtree

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
    }
}