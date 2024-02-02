using System.Collections;
using Lab12.Exceptions;

namespace Lab12.BinaryTree
{
    public partial class BinaryTree<T> : ICollectionExtension<T> where T : notnull, IComparable<T>, new()
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

        public void Add(T item)
        {
            if (IsReadOnly) throw new CollectionIsReadOnlyException();
            if (Capacity > 0 && Count >= Capacity) throw new CollectionIsFullException();

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
            if (IsReadOnly) throw new CollectionIsReadOnlyException();
            if (Root == null) return false;

            var (newRoot, result) = BinaryTree<T>.Remove(Root, item);
            if (result)
            {
                Root = newRoot;
                Count--;
            }
            return result;
        }

        // сложно вернуть бульк, если используется рекурсивный метод
        private static (BinaryTreeNode<T>?, bool) Remove(BinaryTreeNode<T> node, T item)
        {
            var r = item.CompareTo(node.Value);
            if (r < 0)
            {
                if (node.Left == null)
                    return (null, false);

                var (newNode, result) = BinaryTree<T>.Remove(node.Left, item);
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

                var (newNode, result) = BinaryTree<T>.Remove(node.Right, item);
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
                var (newNode, result) = BinaryTree<T>.Remove(node.Right, leaf.Value);
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

        ICollectionExtension<T> ICollectionExtension<T>.ShallowCopy() => ShallowCopy();

        public bool Contains(T item)
        {
            var node = Root;
            if (node == null) return false;
            var flag = false;
            do
            {
                var r = item.CompareTo(node.Value);
                if (r > 0) node = node.Right;
                else if (r < 0) node = node.Left;
                else flag = true;
            } while (node != null && !flag);
            return flag;
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
                IsReadOnly = false;
                Clear();
                Console.WriteLine("BinaryTree.Dispose called.");
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            IsDisposed = true;
        }

        public override string ToString()
            => Root == null ? "Empty tree" : BinaryTree<T>.ToString(Root);

        private static string ToString(BinaryTreeNode<T> node, int level = 0, bool isLeft = false)
        {
            var formatString = $"{{0,{level * 2}}} {{1}}\n";
            var result = level > 0
                ? string.Format(formatString, isLeft ? "/" : "\\", node.Value)
                : $"{node.Value}\n";
            if (node.Left is not null) result += BinaryTree<T>.ToString(node.Left, level + 1, true);
            if (node.Right is not null) result += BinaryTree<T>.ToString(node.Right, level + 1);
            return result;
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

        public IEnumerator<T> GetEnumerator() => (
            Root == null
                ? Enumerable.Empty<T>() // Костыльно, зато правильно
                : InOrderTraverse(Root, e => e.Value)
        ).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override int GetHashCode()
        {
            var hashCode = 0;
            foreach (var item in this)
                hashCode += item.GetHashCode();
            return hashCode;
        }
    }
}