namespace Lab12.BinaryTree
{
    public class BinaryTreeNode<T> where T : notnull, IComparable<T>, new()
    {
        public T Value { get; set; }
        public BinaryTreeNode<T>? Left { get; set; }
        public BinaryTreeNode<T>? Right { get; set; }

        public BinaryTreeNode(T value) => Value = value;
        public BinaryTreeNode() : this(new T()) { }
        public BinaryTreeNode(BinaryTreeNode<T> node) : this(node.Value) { }

        ~BinaryTreeNode()
        {
            Console.WriteLine($"~BinaryTreeNode called. obj: {this}");
        }

        public string Print(int level = 0, bool left = false, bool right = false)
        {
            var s = left ? "/" : right ? "\\" : "-";
            var result = (level == 0 ? "" : new string('\t', level - 1))
                                    + (level == 0 ? "" : $"{s} - - - ")
                                    + Value.ToString()
                                    + '\n';
            if (Right != null) result += Right.Print(level + 1, right: true);
            if (Left != null) result += Left.Print(level + 1, left: true);
            return result;
        }

        public override string ToString() => $"BinaryTreeNode(value: {Value}, "
            + $"left: {(Left == null ? -1 : Left.Value)}, right: {(Right == null ? -1 : Right.Value)})";
    }
}