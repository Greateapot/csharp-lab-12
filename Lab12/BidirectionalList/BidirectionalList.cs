using System.Collections;

namespace Lab12.BidirectionalList
{
    public class BidirectionalList<T> : ICollection<T>, ICloneable, IDisposable where T : notnull, new()
    {
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        public bool IsReadOnly { get; set; }
        private bool IsDisposed = false;

        private BidirectionalListNode<T>? First { get; set; }
        private BidirectionalListNode<T>? Last { get; set; }

        public BidirectionalList() => Capacity = -1;

        public BidirectionalList(int capacity) => Capacity = capacity;

        public BidirectionalList(BidirectionalList<T> collection)
        {
            Capacity = collection.Capacity;
            foreach (var item in collection)
                Add(item);
        }

        ~BidirectionalList() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            if (disposing)
            {
                Clear();
                Console.WriteLine($"BidirectionalList.Dispose({disposing}) called. Count: {Count}");
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            IsDisposed = true;
        }

        public T this[int index]
        {
            get => NodeAt(index).Value;
            set => NodeAt(index).Value = value;
        }

        private BidirectionalListNode<T> NodeAt(int index)
        {
            var _index = 0;
            var node = First;
            while (_index < index && node != null) { node = node.Next; _index++; }
            if (node == null)
                throw new ArgumentOutOfRangeException(nameof(index), $"No node at index {index}.");
            return node;
        }

        public void Add(T item)
        {
            if (IsReadOnly) throw new Exception("List is read-only");
            if (Capacity >= 0 && Count >= Capacity) throw new Exception("List is full");
            var newNode = new BidirectionalListNode<T>(item);
            if (Last == null)
            {
                First = newNode;
                Last = newNode;
            }
            else
            {
                Last.Next = newNode;
                newNode.Previous = Last;
                Last = newNode;
            }
            Count++;
        }

        public void AddAll(params T[] items)
        {
            foreach (var item in items)
                Add(item);
        }

        public void AddAfter(T after, T item)
        {
            if (IsReadOnly) throw new Exception("List is read-only");
            if (Capacity >= 0 && Count >= Capacity) throw new Exception("List is full");

            var node = First ?? throw new ArgumentException("List is empty");

            var inserted = false;
            do
            {
                if (node.Value.Equals(after))
                {
                    var newNode = new BidirectionalListNode<T>(item)
                    {
                        Next = node.Next,
                        Previous = node
                    };
                    if (node.Next != null)
                        node.Next.Previous = newNode;
                    node.Next = newNode;
                    Count++;
                    inserted = true;
                }
                node = node.Next;
            } while (!inserted && node != null);
            if (!inserted) throw new ArgumentException($"No node with value {after} founded");
        }

        public void Clear()
        {
            while (First != null)
                Remove(First.Value);
        }

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

        public bool Remove(T item)
        {
            if (IsReadOnly) throw new Exception("List is read-only");

            var node = First;
            if (node == null) return false;

            var removed = false;
            do
            {
                if (node.Value.Equals(item))
                {
                    if (node == First) First = node.Next;
                    if (node == Last) Last = node.Previous;

                    if (node.Previous != null) node.Previous.Next = node.Next;
                    if (node.Next != null) node.Next.Previous = node.Previous;

                    node.Next = null;
                    node.Previous = null;

                    removed = true;
                    Count--;
                }
                node = node.Next;
            } while (!removed && node != null);
            return removed;
        }

        public bool[] RemoveAll(params T[] items)
        {
            var result = new bool[items.Length];
            for (int index = 0; index < items.Length; index++)
                result[index] = Remove(items[index]);
            return result;
        }

        public IEnumerator<T> GetEnumerator() => new BidirectionalListEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public object Clone() => new BidirectionalList<T>(this);

        public BidirectionalList<T> ShallowCopy() => (BidirectionalList<T>)MemberwiseClone();
    }
}