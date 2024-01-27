using System.Collections;

namespace Lab12.BidirectionalList
{
    public class BidirectionalListEnumerator<T> : IEnumerator<T> where T : notnull, new()
    {
        public T Current
        {
            get
            {
                if (node == null)
                    throw new ArgumentException();
                return node.Value;
            }
        }

        object IEnumerator.Current => Current;

        private bool InProcess = false;

        private BidirectionalListNode<T>? node;

        private readonly BidirectionalListNode<T>? root;

        public BidirectionalListEnumerator(BidirectionalListNode<T>? root) => this.root = root;

        public void Dispose() => GC.SuppressFinalize(this);

        public bool MoveNext()
        {
            if (!InProcess)
            {
                InProcess = true;
                node = root;
            }
            else if (node != null)
                node = node.Next;

            return node != null;
        }

        public void Reset()
        {
            InProcess = false;
            node = null;
        }
    }
}