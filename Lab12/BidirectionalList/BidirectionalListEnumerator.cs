using System.Collections;

namespace Lab12.BidirectionalList
{
    public class BidirectionalListEnumerator<T> : IEnumerator<T> where T : notnull, new()
    {
        public T Current
        {
            get
            {
                if (!IsValid)
                    throw new ArgumentException();
                return values[position];
            }
        }

        object IEnumerator.Current => Current;

        private int position = -1;

        private readonly BidirectionalList<T> values;

        private bool IsValid => position > -1 && position < values.Count;

        public BidirectionalListEnumerator(BidirectionalList<T> values) => this.values = values;

        public void Dispose() => GC.SuppressFinalize(this);

        public bool MoveNext()
        {
            if (position < values.Count)
                position++;
            return IsValid;
        }

        public void Reset() => position = -1;
    }
}