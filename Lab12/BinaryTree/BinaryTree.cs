using System.Collections;

namespace Lab12.BinaryTree
{
    public partial class BinaryTree<T> : ICollection<T>, ICloneable, IDisposable where T : notnull, IComparable<T>, new()
    {
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        public bool IsReadOnly { get; set; }
        private bool IsDisposed = false;

        private BinaryTreeNode<T>? Root { get; set; }

        public BinaryTree() => Capacity = -1;

        public BinaryTree(int capacity) => Capacity = capacity;

        public BinaryTree(BinaryTree<T> collection)
        {
            Capacity = collection.Capacity;
            foreach (var item in collection)
                Add(item);
        }

        ~BinaryTree() => Dispose(false);

        public override string ToString()
        {
            return $"{Count}:\n" + (Root != null ? Root.Print() : "Empty");
        }

        public void Add(T item)
        {
            if (IsReadOnly) throw new Exception("Tree is read-only");
            if (Capacity >= 0 && Count >= Capacity) throw new Exception("Tree is full");

            Root = Add(Root, item);
        }

        private BinaryTreeNode<T> Add(BinaryTreeNode<T>? node, T item)
        {
            if (node == null)
            {
                Count++;
                return new(item);
            }
            var r = item.CompareTo(node.Value);
            if (r < 0)
            {
                node.Left = Add(node.Left, item);
                node = Balance(node);
            }
            else if (r > 0)
            {
                node.Right = Add(node.Right, item);
                node = Balance(node);
            }
            return node;
        }

        public void AddAll(params T[] items)
        {
            foreach (var item in items)
                Add(item);
        }

        public bool Remove(T item)
        {
            if (IsReadOnly) throw new Exception("Tree is read-only");
            if (Root == null) throw new Exception("Tree is empty");

            var (newRoot, result) = Remove(Root, item);
            if (result)
            {
                Root = newRoot;
                Count--;
            }
            return result;
        }

        private (BinaryTreeNode<T>?, bool) Remove(BinaryTreeNode<T> node, T item)
        {
            var r = item.CompareTo(node.Value);
            if (r < 0)
            {
                if (node.Left == null)
                    return (null, false);

                var (newNode, result) = Remove(node.Left, item);
                node.Left = newNode;

                if (node.Right != null && BalanceFactor(node) < -1)
                    node = BalanceFactor(node.Right) < 1
                        ? RotateRR(node)
                        : RotateRL(node);

                return (node, result);
            }
            else if (r > 0)
            {
                if (node.Right == null)
                    return (null, false);

                var (newNode, result) = Remove(node.Right, item);
                node.Right = newNode;

                if (node.Left != null && BalanceFactor(node) > 1)
                    node = BalanceFactor(node.Left) > -1
                        ? RotateLL(node)
                        : RotateLR(node);

                return (node, result);
            }
            else
            {
                if (node.Right == null)
                    return (node.Left, true);

                var leaf = node.Right;
                while (leaf.Left != null)
                    leaf = leaf.Left;
                node.Value = leaf.Value;
                var (newNode, result) = Remove(node.Right, leaf.Value);
                node.Right = newNode;

                if (node.Left != null && BalanceFactor(node) > 1)
                    node = BalanceFactor(node.Left) > -1
                        ? RotateLL(node)
                        : RotateLR(node);

                return (node, result);
            }
        }

        public bool[] RemoveAll(params T[] items)
        {
            var result = new bool[items.Length];
            for (int i = 0; i < items.Length; i++)
                result[i] = Remove(items[i]);
            return result;
        }

        public void Clear()
        {
            while (Root != null)
                Remove(Root.Value);
        }

        public object Clone() => new BinaryTree<T>(this);

        public BinaryTree<T> ShallowCopy() => (BinaryTree<T>)MemberwiseClone();

        public bool Contains(T item)
        {
            foreach (var _item in this)
                if (_item.Equals(item))
                    return true;
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in this)
                array[arrayIndex++] = item;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            if (disposing)
            {
                Clear();
                Console.WriteLine($"BinaryTree.Dispose({disposing}) called. Count: {Count}");
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            IsDisposed = true;
        }

        public int GetLeafCount() => Root == null
            ? 0
            : InOrderTraverse(Root, e => e.Left == null && e.Right == null ? 1 : 0).Sum();


        private static IEnumerable<R> InOrderTraverse<R>(BinaryTreeNode<T> node, Func<BinaryTreeNode<T>, R> func)
        {
            if (node.Left != null)
                foreach (var item in InOrderTraverse(node.Left, func))
                    yield return item;
            yield return func(node);
            if (node.Right != null)
                foreach (var item in InOrderTraverse(node.Right, func))
                    yield return item;
        }

        public IEnumerator<T> GetEnumerator() => Root == null
            ? throw new Exception("Tree is empty")
            : InOrderTraverse(Root, e => e.Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}