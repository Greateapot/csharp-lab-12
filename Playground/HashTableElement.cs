using Lab10Lib.Interfaces;

namespace Playground
{
    public class HashTableElement<T> where T : IInit, new()
    {
        public T Value { get; set; }
        public HashTableElement<T>? NextElement { get; set; }

        /// <summary>
        /// Init with value
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="nextElement">next element</param>
        public HashTableElement(
            T value,
            HashTableElement<T>? nextElement = null
        )
        {
            Value = value;
            NextElement = nextElement;
        }

        /// <summary>
        /// Init with creating new person
        /// </summary>
        /// <param name="isRandom">init random person?</param>
        /// <param name="nextElement">next element</param>
        public HashTableElement(
            bool isRandom = true,
            HashTableElement<T>? nextElement = null
        )
        {
            Value = new();
            NextElement = nextElement;

            if (isRandom)
                Value.RandomInit();
            else
                Value.Init();
        }

        public override string ToString() => $"HashTableElement#{GetHashCode()}(value: {Value})";
    }
}