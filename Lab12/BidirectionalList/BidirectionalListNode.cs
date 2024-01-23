namespace Lab12.BidirectionalList
{
    public class BidirectionalListNode<T> where T : notnull, new()
    {
        public T Value { get; set; }
        public BidirectionalListNode<T>? Next { get; set; }
        public BidirectionalListNode<T>? Previous { get; set; }

        public BidirectionalListNode(T value) => Value = value;
        public BidirectionalListNode() : this(new T()) { }
        public BidirectionalListNode(BidirectionalListNode<T> node) : this(node.Value) { }

        ~BidirectionalListNode()
        {
            Console.WriteLine($"~BidirectionalListNode called. obj: {this}");
        }

        public override string ToString() => $"BidirectionalListNode(value: {Value}, "
            + $"next: {(Next == null ? -1 : Next.Value)}, prev: {(Previous == null ? -1 : Previous.Value)})";
    }
}