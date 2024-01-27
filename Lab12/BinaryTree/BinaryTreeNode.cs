namespace Lab12.BinaryTree
{
    public class BinaryTreeNode<T>(T value) where T : notnull, IComparable<T>, new()
    {
        public T Value { get; set; } = value;
        public BinaryTreeNode<T>? Left { get; set; }
        public BinaryTreeNode<T>? Right { get; set; }

        public BinaryTreeNode() : this(new T()) { }
        public BinaryTreeNode(BinaryTreeNode<T> node) : this(node.Value) { }

        public override string ToString() => $"BinaryTreeNode(value: {Value})";
    }
}