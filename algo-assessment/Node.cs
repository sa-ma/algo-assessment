public class Node
{
    public string Word { get; set; }
    public int Frequency { get; set; }
    public Node? Next { get; set; }

    public Node(string word)
    {
        Word = word;
        Frequency = 1;
        Next = null;
    }
}