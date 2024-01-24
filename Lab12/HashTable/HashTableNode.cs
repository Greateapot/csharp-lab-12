namespace Lab12.HashTable
{
    public class HashTableNode<T> where T : notnull, new()
    {
        public T Value { get; set; }
        public HashTableNode<T>? Next { get; set; }

        public HashTableNode(T value) => Value = value;
        public HashTableNode() : this(new T()) { }
        public HashTableNode(HashTableNode<T> node) : this(node.Value) { }

        ~HashTableNode()
        {
            Console.WriteLine($"~HashTableNode called. obj: {this}");
        }


        public override string ToString() => $"HashTableNode(value: {Value}, "
            + $"next: {(Next == null ? -1 : Next.Value)})";
    }
}