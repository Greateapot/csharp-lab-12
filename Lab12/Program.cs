using Lab12.HashTable;
using Lab12.BidirectionalList;
using Lab12.BinaryTree;

namespace Lab12
{
    public class Program
    {
        public static void Main()
        {
            var random = new Random();
            var tree = new BinaryTree<int>();
            for (int i = 0; i < 40; i++)
            {
                var item = random.Next(1000);
                // item.RandomInit();
                Console.WriteLine($"Add item: {item}");
                tree.Add(item);
            }

            Console.WriteLine(tree);
            Console.WriteLine(tree.GetLeafCount());
            tree.Dispose();

            var table = new HashTable<int>(10);
            for (int i = 0; i < 20; i++)
            {
                table.Add(i);
            }
            Console.WriteLine(table);
            table.RemoveAll(3, 6, 7, 10, 5, 7);
            Console.WriteLine(table);

            foreach (var item in table)
            {
                Console.WriteLine(item);
            }
            table.Dispose();
        }

        public static void Task1Process()
        {
            var list = new BidirectionalList<int>();
            for (int index = 0; index < 3; index++) list.Add(index);
            Print(list);
        }

        public static void Print<T>(BidirectionalList<T> list) where T : notnull, new()
        {
            var counter = 1;
            foreach (var item in list)
            {
                Console.WriteLine($"{counter}. {item}");
                counter++;
            }
        }
    }
}