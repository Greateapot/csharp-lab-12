namespace Lab12
{
    public interface ICollectionExtension<T> : ICollection<T>, ICloneable, IDisposable
    {
        public void AddAll(params T[] items);
        public bool[] RemoveAll(params T[] items);
        public ICollectionExtension<T> ShallowCopy();
    }
}