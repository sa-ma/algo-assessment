namespace algo_assessment
{
    public class BinarySearchTree
    {
        private class Node
        {
            public string Word { get; set; }
            public int Frequency { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public Node(string word)
            {
                Word = word.ToLower();
                Frequency = 1;
                Left = null;
                Right = null;
            }
        }

        private Node? root;
        public int Count { get; private set; }

        public void AddWord(string word)
        {
            word = word.ToLower();
            if (root == null)
            {
                root = new Node(word);
                Count++;
                return;
            }

            AddWordRecursive(root, word);
        }

        private void AddWordRecursive(Node node, string word)
        {
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

        public string[] GetUniqueWords()
        {
            List<string> words = new List<string>();
            InOrderTraversal(root, words);
            return words.ToArray();
        }

        private void InOrderTraversal(Node? node, List<string> words)
        {
            if (node == null) return;

            InOrderTraversal(node.Left, words);
            words.Add(node.Word);
            InOrderTraversal(node.Right, words);
        }

        public IEnumerable<(string word, int frequency)> GetWordsOrderedBy(bool ascending)
        {
            List<(string word, int frequency)> wordFrequencies = new List<(string, int)>();
            CollectWordFrequencies(root, wordFrequencies);

            return ascending
                ? wordFrequencies.OrderBy(x => x.frequency)
                : wordFrequencies.OrderByDescending(x => x.frequency);
        }

        private void CollectWordFrequencies(Node? node, List<(string word, int frequency)> wordFrequencies)
        {
            if (node == null) return;

            CollectWordFrequencies(node.Left, wordFrequencies);
            wordFrequencies.Add((node.Word, node.Frequency));
            CollectWordFrequencies(node.Right, wordFrequencies);
        }

        public int GetWordFrequency(string word)
        {
            return GetWordFrequencyRecursive(root, word.ToLower());
        }

        private int GetWordFrequencyRecursive(Node? node, string word)
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