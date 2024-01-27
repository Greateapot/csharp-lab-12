using System.Collections;
using Lab12.Exceptions;

namespace Lab12.HashTable
{
    public class HashTable<T> : ICollection<T>, ICloneable, IDisposable where T : notnull, new()
    {
        public const int DefaultTableSize = 10;

        public int Count { get; private set; }
        public int Capacity { get; private set; }
        public bool IsReadOnly { get; set; }
        private bool IsDisposed = false;

        private readonly HashTableNode<T>?[] table;

        public HashTable(int size = DefaultTableSize)
        {
            Capacity = -1;
            table = new HashTableNode<T>[size];
        }

        public HashTable(int capacity, int size = DefaultTableSize)
        {
            Capacity = capacity;
            table = new HashTableNode<T>[size];
        }

        public HashTable(HashTable<T> collection, int size = DefaultTableSize)
        {
            Capacity = collection.Capacity;
            table = new HashTableNode<T>[size];
            foreach (var item in collection)
                Add(item);
        }

        ~HashTable() => Dispose(false);

        private int CalculateHash(T item) => Math.Abs(item.GetHashCode()) % TableSize;

        public int TableSize => table.Length;

        private HashTableNode<T>? NodeAt(int index)
            => index < 0 || index >= TableSize
                ? throw new IndexOutOfRangeException()
                : table[index];

        public void Add(T item)
        {
            if (IsReadOnly) throw new CollectionIsReadOnlyException();
            if (Capacity >= 0 && Count >= Capacity) throw new CollectionIsFullException();

            var hash = CalculateHash(item);
            var node = table[hash];

            if (node == null)
            {
                table[hash] = new(item);
                Count++;
            }
            else
            {
                var exists = false;
                for (; !exists && node.Next != null; node = node.Next)
                    exists = node.Value.Equals(item);

                if (!exists)
                {
                    node.Next = new(item);
                    Count++;
                }
            }
        }

        public void AddAll(params T[] items)
        {
            foreach (var item in items)
                Add(item);
        }

        public bool Remove(T item)
        {
            if (IsReadOnly) throw new CollectionIsReadOnlyException();
            if (Count == 0) throw new CollectionIsEmptyException();

            var hash = CalculateHash(item);
            var node = table[hash];
            if (node == null) return false;

            if (node.Value.Equals(item))
            {
                table[hash] = node.Next;
                Count--;
                return true;
            }

            if (node.Next == null) return false;

            var removed = false;
            do
            {
                if (node.Next.Value.Equals(item))
                {
                    node.Next = node.Next.Next;
                    Count--;
                    removed = true;
                }
                else
                    node = node.Next;
            } while (!removed && node.Next != null);
            return removed;
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
            for (int index = 0; index < TableSize; index++)
                table[index] = null;
            // т.к. список однонаправ., удаление ссылки на первый элемент приведет к удалению всех элементов.
        }

        public object Clone() => new HashTable<T>(this);

        public HashTable<T> ShallowCopy() => (HashTable<T>)MemberwiseClone();

        public bool Contains(T item)
        {
            var hash = CalculateHash(item);
            var node = table[hash];
            if (node is null) return false;

            var contains = false;
            do
            {
                if (node.Value.Equals(item))
                    contains = true;
                else
                    node = node.Next;
            } while (!contains && node is not null);
            return contains;
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
                Console.WriteLine($"HashTable.Dispose called.");
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            IsDisposed = true;
        }

        public IEnumerator<T> GetEnumerator() => new HashTableEnumerator<T>(this, NodeAt);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            var result = new string[TableSize];
            for (int index = 0; index < TableSize; index++)
                if (table[index] is null) result[index] = $"{index + 1}. _";
                else
                {
                    var node = table[index];
                    if (node is null)
                    {
                        result[index] = $"{index + 1}. _";
                    }
                    else
                    {
                        var temp = $"{index + 1}. {node.Value}";
                        while (node.Next != null)
                        {
                            node = node.Next;
                            temp += $" --> {node.Value}";
                        }
                        result[index] = temp;
                    }
                }
            return string.Join('\n', result);
        }
    }
}