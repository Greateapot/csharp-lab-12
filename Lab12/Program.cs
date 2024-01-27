using Lab12.HashTable;
using Lab12.BidirectionalList;
using Lab12.BinaryTree;
using ConsoleIOLib;

namespace Lab12
{
    public class Program
    {
        public static void Main()
        {
            Task1Process();
            Task2Process();
            Task3Process();
        }

        public static void Task1Process()
        {
            var list = new BidirectionalList<int>();
            for (int index = 0; index < 20; index++) list.Add(index);
            ConsoleIO.WriteLine(list);
        }

        public static void Task2Process()
        {
            var tree = new BinaryTree<int>();
            for (int index = 0; index < 20; index++) tree.Add(index);
            ConsoleIO.WriteLine(tree);
        }

        public static void Task3Process()
        {
            var table = new HashTable<int>(10);
            for (int index = 0; index < 20; index++) table.Add(index);
            ConsoleIO.WriteLine(table);
        }
    }
}