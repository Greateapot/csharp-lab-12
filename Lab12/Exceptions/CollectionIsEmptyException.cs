namespace Lab12.Exceptions
{
    [Serializable]
    public class CollectionIsEmptyException : Exception
    {
        public CollectionIsEmptyException() { }
        public CollectionIsEmptyException(string message) : base(message) { }
        public CollectionIsEmptyException(string message, Exception inner) : base(message, inner) { }
    }
}