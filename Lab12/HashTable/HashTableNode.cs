namespace Lab12.HashTable
{
    public class HashTableNode<T>(T value) where T : notnull, new()
    {
        public T Value { get; set; } = value;
        public HashTableNode<T>? Next { get; set; }

        public HashTableNode() : this(new T()) { }
        public HashTableNode(HashTableNode<T> node) : this(node.Value) { }

        public override string ToString() => $"HashTableNode(value: {Value})";
    }
}