public class LinkedList
{
    private Node? head;
    public int Count { get; private set; }

    public LinkedList()
    {
        head = null;
        Count = 0;
    }

    public void AddWord(string word)
    {
        word = word.ToLower();
        
        if (head == null)
        {
            head = new Node(word);
            Count++;
            return;
        }

        // Check if word exists
        Node? current = head;
        while (current != null)
        {
            if (current.Word == word)
            {
                current.Frequency++;
                return;
            }
            current = current.Next;
        }

        // Add new word
        Node newNode = new Node(word);
        newNode.Next = head;
        head = newNode;
        Count++;
    }

    public IEnumerable<(string word, int frequency)> GetWordsOrderedBy(bool ascending = true)
    {
        var words = new List<(string word, int frequency)>();
        Node? current = head;
        
        while (current != null)
        {
            words.Add((current.Word, current.Frequency));
            current = current.Next;
        }

        return ascending 
            ? words.OrderBy(x => x.frequency)
            : words.OrderByDescending(x => x.frequency);
    }

    public IEnumerable<string> GetUniqueWords()
    {
        var words = new List<string>();
        Node? current = head;
        
        while (current != null)
        {
            words.Add(current.Word);
            current = current.Next;
        }

        return words.OrderBy(w => w);
    }

    public int GetWordFrequency(string word)
    {
        word = word.ToLower();
        Node? current = head;
        
        while (current != null)
        {
            if (current.Word == word)
                return current.Frequency;
            current = current.Next;
        }

        return 0;
    }
}