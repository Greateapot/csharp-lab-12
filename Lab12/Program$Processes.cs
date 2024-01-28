using ConsoleIOLib;
using ConsoleDialogLib;

using Lab10Lib.Entities;

using Lab12.HashTable;
using Lab12.BidirectionalList;
using Lab12.BinaryTree;
using Lab12.Exceptions;

namespace Lab12
{
    public partial class Program
    {
        private static void Task1Process()
        {
            ConsoleIO.Clear();
            var capacity = InputCapacity();
            var list = new BidirectionalList<Person>(capacity);

            {
                ConsoleIO.Clear();
                var count = InputCount(capacity);
                var isRandom = InputBoolean("Generate random persons?");
                for (int i = 0; i < count; i++)
                {
                    var person = new Person();
                    if (isRandom)
                        person.RandomInit();
                    else
                        person.Init();
                    list.Add(person);
                }
                ConsoleIO.WriteLine($"List:\n{list}\n");
            }
            {
                var after = new Person();
                ConsoleIO.WriteLine("Input after person.");
                after.Init();

                var isRandom = InputBoolean("Generate random person?");
                var person = new Person();
                if (isRandom)
                    person.RandomInit();
                else
                    person.Init();
                try
                {
                    list.AddAfter(after, person);
                }
                catch (ArgumentException e)
                {
                    ConsoleIO.WriteLine(e.Message);
                }
                catch (CollectionIsFullException)
                {
                    ConsoleIO.WriteLine("Can't add element: collection is full.");
                }
                finally
                {
                    ConsoleIO.WriteLine($"List:\n{list}");
                }
            }

            list.Dispose();
        }

        private static void Task2Process()
        {
            ConsoleIO.Clear();
            var capacity = InputCapacity();
            var tree = new BinaryTree<Person>(capacity);

            {
                ConsoleIO.Clear();
                var count = InputCount(capacity);
                var isRandom = InputBoolean("Generate random persons?");
                for (int i = 0; i < count; i++)
                {
                    var person = new Person();
                    if (isRandom)
                        person.RandomInit();
                    else
                        person.Init();
                    tree.Add(person);
                }
                ConsoleIO.WriteLine($"Binary tree:\n{tree}\n");
                ConsoleIO.WriteLine($"Count of leafs: {tree.GetLeafCount()}");
            }

            tree.Dispose();
        }

        private static void Task3Process()
        {
            ConsoleIO.Clear();
            var size = InputSize();
            var capacity = InputCapacity();
            var table = new HashTable<Person>(capacity, size);

            {
                ConsoleIO.Clear();
                var count = InputCount();
                var isRandom = InputBoolean("Generate random persons?");
                for (int i = 0; i < count; i++)
                {
                    var person = new Person();
                    if (isRandom)
                        person.RandomInit();
                    else
                        person.Init();
                    table.Add(person);
                }
                ConsoleIO.WriteLine($"Hash table:\n{table}\n");
            }
            {
                Person person;
                bool contains;
                do
                {
                    person = new Person();
                    ConsoleIO.WriteLine("Input search person.");
                    person.Init();
                    contains = table.Contains(person);
                    if (!contains)
                        ConsoleIO.WriteLine("Person not found.");
                } while (!contains);

                var rr = table.Remove(person);
                ConsoleIO.WriteLine($"Removing result: {rr}");
                ConsoleIO.WriteLine($"Contains result: {table.Contains(person)}");
                ConsoleIO.WriteLine($"Hash table:\n{table}\n");
            }

            table.Dispose();
        }

        private static void Task4Process()
        {

            BinaryTree<Person>? tree = null;
            var exit = false;
            var dialog = new ConsoleDialog(
                "acts",
                [
                    new ConsoleDialogOption("init unlimited", _ => {
                        if (tree != null)
                            ConsoleIO.WriteLine("tree alr exist");
                        else
                            tree = new(); // net8.0 угарает
                    }, true, true, true),
                    new ConsoleDialogOption("init limited", _ => {
                        if (tree != null)
                            ConsoleIO.WriteLine("tree alr exist");
                        else
                            tree = new(InputCapacity());
                    }, true, true, true),
                    new ConsoleDialogOption("make rw/ro", _ => {
                        if (tree == null)
                        {
                            ConsoleIO.WriteLine("no tree");
                            return;
                        }
                        tree.IsReadOnly = InputBoolean("make tree read-only?");
                        if (tree.IsReadOnly)
                            ConsoleIO.WriteLine("now, tree is read-only");
                        else
                            ConsoleIO.WriteLine("now, tree is writable");
                    }, true, true, true),
                    new ConsoleDialogOption("add (all)", _ => {
                        if (tree == null)
                        {
                            ConsoleIO.WriteLine("no tree");
                            return;
                        }
                        try {
                            var count = InputCount(tree.Capacity);
                            var persons = new Person[count];
                            for (int i = 0; i < count; i++)
                            {
                                persons[i] = new();
                                persons[i].RandomInit();
                            }
                            tree.AddAll(persons);
                            ConsoleIO.WriteLine("Items added");
                        } catch (CollectionIsReadOnlyException) {
                             ConsoleIO.WriteLine("collection is read-only");
                        } catch (CollectionIsFullException) {
                            ConsoleIO.WriteLine("collection is full");
                        }
                    }, true, true, true),
                    new ConsoleDialogOption("remove (all)", _ => {
                        if (tree == null)
                        {
                            ConsoleIO.WriteLine("no tree");
                            return;
                        }
                        try {
                            var count = InputCount(tree.Capacity);
                            var persons = new Person[count];
                            for (int i = 0; i < count; i++)
                            {
                                persons[i] = new();
                                persons[i].Init();
                            }
                            tree.RemoveAll(persons);
                            ConsoleIO.WriteLine("Items removed");
                        } catch (CollectionIsReadOnlyException) {
                             ConsoleIO.WriteLine("collection is read-only");
                        } catch (CollectionIsEmptyException) {
                            ConsoleIO.WriteLine("collection is empty");
                        }
                    }, true, true, true),
                    new ConsoleDialogOption("clear", _ => {
                        if (tree == null)
                        {
                            ConsoleIO.WriteLine("no tree");
                            return;
                        }
                        try {
                            tree.Clear();
                            ConsoleIO.WriteLine("tree deleted");
                        } catch (CollectionIsReadOnlyException) {
                             ConsoleIO.WriteLine("collection is read-only");
                        }
                    }, true, true, true),
                    new ConsoleDialogOption("find by person data", _ => {
                        if (tree == null)
                        {
                            ConsoleIO.WriteLine("no tree");
                            return;
                        }
                        var person = new Person();
                        person.Init();
                        if (tree.Contains(person))
                            ConsoleIO.WriteLine("tree contains this person");
                        else
                            ConsoleIO.WriteLine("not contains");
                    }, true, true, true),
                    new ConsoleDialogOption("compare with clone and shallow copy", _ => {
                        if (tree == null)
                        {
                            ConsoleIO.WriteLine("no tree");
                            return;
                        }
                        var clone = (BinaryTree<Person>)tree.Clone();
                        var copy = tree.ShallowCopy();
                        ConsoleIO.WriteLine($"Is clone equals: {clone.Equals(tree)}");
                        ConsoleIO.WriteLine($"Is copy equals: {copy.Equals(tree)}");
                    }, true, true, true),
                    new ConsoleDialogOption("dispose", _ => {
                        if (tree == null)
                        {
                            ConsoleIO.WriteLine("no tree");
                            return;
                        }
                        tree.Dispose();
                        ConsoleIO.WriteLine("tree deleted");
                    }, true, true, true),
                    new ConsoleDialogOption("exit", _ => {
                        exit = true;
                    }, true, true, true),
                ],
                false
            );

            do
            {
                dialog.SubTitle = "\nTree:\n" + tree?.ToString();
                ConsoleDialog.ShowDialog(dialog);
                dialog.Reset();
            } while (!exit);

            tree?.Dispose();
        }
    }
}