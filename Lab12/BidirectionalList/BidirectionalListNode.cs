namespace Lab12.BidirectionalList
{
    public class BidirectionalListNode<T>(T value) where T : notnull, new()
    {
        public T Value { get; set; } = value;
        public BidirectionalListNode<T>? Next { get; set; }
        public BidirectionalListNode<T>? Previous { get; set; }

        public BidirectionalListNode() : this(new T()) { }
        public BidirectionalListNode(BidirectionalListNode<T> node) : this(node.Value) { }

        public override string ToString() => $"BidirectionalListNode(value: {Value})";
    }
}