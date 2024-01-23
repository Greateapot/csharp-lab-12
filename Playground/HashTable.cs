using Lab10Lib.Interfaces;

namespace Playground
{
    public class HashTable<T> where T : IInit, new()
    {
        private readonly HashTableElement<T>?[] table;

        public HashTable(uint size)
        {
            table = new HashTableElement<T>[size];
        }

        private int CalculateHash(T value) => Math.Abs(value.GetHashCode()) % table.Length;

        public bool Add(T value)
        {
            var hash = CalculateHash(value);
            var element = table[hash];

            if (element is null)
            {
                table[hash] = new(value);
            }
            else
            {
                for (; element.NextElement != null; element = element.NextElement)
                    if (element.Value.Equals(value))
                        return false;

                element.NextElement = new(value);
            }
            return true;
        }

        public bool Contains(T value)
        {
            var hash = CalculateHash(value);
            var element = table[hash];
            if (element is null) return false;

            var contains = false;
            do
            {
                if (element.Value.Equals(value))
                    contains = true;
                else
                    element = element.NextElement;
            } while (!contains && element is not null);
            return contains;
        }

        public bool Remove(T value)
        {
            var hash = CalculateHash(value);
            var element = table[hash];
            if (element is null) return false;

            if (element.Value.Equals(value))
            {
                table[hash] = element.NextElement;
                return true;
            }

            if (element.NextElement is null) return false;

            var removed = false;
            do
            {
                if (element.NextElement.Value.Equals(value))
                {
                    element.NextElement = element.NextElement.NextElement;
                    removed = true;
                }
                else
                    element = element.NextElement;
            } while (!removed && element.NextElement is not null);
            return removed;
        }

        public override string ToString()
        {
            var result = new string[table.Length];
            for (int index = 0; index < table.Length; index++)
                if (table[index] is null) result[index] = $"{index + 1}. _";
                else
                {
                    var next = table[index];
                    if (next is null)
                    {
                        result[index] = $"{index + 1}. _";
                    }
                    else
                    {
                        var temp = $"{index + 1}. {next}";
                        while (next.NextElement != null)
                        {
                            next = next.NextElement;
                            temp += $"\n --> {next}";
                        }
                        result[index] = temp;
                    }
                }
            return string.Join('\n', result);
        }

        public static HashTable<T> MakeTable(uint size, uint count, bool isRandom = false)
        {
            var table = new HashTable<T>(size);
            for (int counter = 1; counter <= count; counter++)
            {
                T value = new();
                if (isRandom) value.RandomInit();
                else value.Init();
                if (!table.Add(value)) counter--;
            }
            return table;
        }
    }
}