using System.Collections;

namespace Lab12.HashTable
{
    public class HashTableEnumerator<T> : IEnumerator<T> where T : notnull, IComparable<T>, new()
    {
        private readonly Func<int, HashTableNode<T>?> getter;
        private HashTableNode<T>? node;
        private readonly HashTable<T> values;

        public T Current
        {
            get
            {
                if (node == null) throw new ArgumentNullException();
                else return node.Value;
            }
        }

        object IEnumerator.Current => Current;

        private int position = -1;

        private bool IsValid => position > -1 && position < values.TableSize;

        public HashTableEnumerator(HashTable<T> values, Func<int, HashTableNode<T>?> getter)
        {
            this.values = values;
            this.getter = getter;
        }

        public void Dispose() => GC.SuppressFinalize(this);

        public bool MoveNext()
        {
            if (node != null && node.Next != null)
                node = node.Next;
            else
            {
                do
                {
                    position++;
                    if (position < values.TableSize)
                        node = getter(position);
                    else
                        node = null;
                } while (node == null && position < values.TableSize);
            }

            return IsValid;
        }

        public void Reset()
        {
            position = -1;
            node = null;
        }


    }
}