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
                var isRandom = InputBoolean(Messages.InputGenerateRandomPersons);
                for (int i = 0; i < count; i++)
                {
                    var person = new Person();
                    if (isRandom)
                        person.RandomInit();
                    else
                        person.Init();
                    list.Add(person);
                }
                ConsoleIO.WriteLineFormat(Messages.PrintList, list);
            }
            {
                var after = new Person();
                ConsoleIO.WriteLine(Messages.InputAfterPerson);
                after.Init();

                var isRandom = InputBoolean(Messages.InputGenerateRandomPerson);
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
                    ConsoleIO.WriteLine(Messages.CollectionIsFullException);
                }
                finally
                {
                    ConsoleIO.WriteLineFormat(Messages.PrintList, list);
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
                var isRandom = InputBoolean(Messages.InputGenerateRandomPersons);
                for (int i = 0; i < count; i++)
                {
                    var person = new Person();
                    if (isRandom)
                        person.RandomInit();
                    else
                        person.Init();
                    tree.Add(person);
                }
                ConsoleIO.WriteLineFormat(Messages.PrintTree, tree);
                ConsoleIO.WriteLineFormat(Messages.PrintTreeLeafCount, tree.GetLeafCount());
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
                var isRandom = InputBoolean(Messages.InputGenerateRandomPersons);
                for (int i = 0; i < count; i++)
                {
                    var person = new Person();
                    if (isRandom)
                        person.RandomInit();
                    else
                        person.Init();
                    table.Add(person);
                }
                ConsoleIO.WriteLineFormat(Messages.PrintHashTable, table);
            }
            {
                Person person;
                bool contains;
                do
                {
                    person = new Person();
                    ConsoleIO.WriteLine(Messages.InputSearchPerson);
                    person.Init();
                    contains = table.Contains(person);
                    if (!contains)
                        ConsoleIO.WriteLine(Messages.PersonNotFound);
                } while (!contains);

                ConsoleIO.WriteLineFormat(Messages.PrintHashTableRemovingResult, table.Remove(person));
                ConsoleIO.WriteLineFormat(Messages.PrintHashTableContainsResult, table.Contains(person));
                ConsoleIO.WriteLineFormat(Messages.PrintHashTable, table);
            }

            table.Dispose();
        }

        private static void Task4Process()
        {

            BinaryTree<Person>? tree = null;
            var exit = false;

            ConsoleDialog dialog = new(
                Messages.Task4DialogTitle,
                [
                    new(Messages.Task4DialogOptionInitUnlimited, _ => Task4ProcessInitUnlimited(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionInitLimited, _ => Task4ProcessInitLimited(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionMakeReadOnly, _ => Task4ProcessMakeReadOnly(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionAdd, _ => Task4ProcessAdd(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionRemove, _ => Task4ProcessRemove(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionClear, _ => Task4ProcessClear(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionContains, _ => Task4ProcessContains(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionCompareWithCopies, _ => Task4ProcessCompareWithCopies(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionDispose, _ =>Task4ProcessDispose(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionExit, _ => exit = true, true, true, true),
                ],
                false
            );

            do
            {
                dialog.SubTitle = string.Format(Messages.PrintTree, tree);
                ConsoleDialog.ShowDialog(dialog);
                dialog.Reset();
            } while (!exit);

            tree?.Dispose();
        }

        private static void Task4ProcessInitUnlimited(ref BinaryTree<Person>? tree)
        {
            if (tree != null)
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeAlreadyExists);
            else
                tree = []; // net8.0 угарает
        }

        private static void Task4ProcessInitLimited(ref BinaryTree<Person>? tree)
        {
            if (tree != null)
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeAlreadyExists);
            else
                tree = new(InputCapacity());
        }

        private static void Task4ProcessMakeReadOnly(ref BinaryTree<Person>? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            tree.IsReadOnly = InputBoolean(Messages.Task4DialogOptionMakeReadOnly);
            if (tree.IsReadOnly)
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeIsReadOnly);
            else
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeIsNotReadOnly);
        }

        private static void Task4ProcessAdd(ref BinaryTree<Person>? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            try
            {
                var count = InputCount(tree.Capacity);
                var persons = new Person[count];
                for (int i = 0; i < count; i++)
                {
                    persons[i] = new();
                    persons[i].RandomInit();
                }
                tree.AddAll(persons);
                ConsoleIO.WriteLine(Messages.Task4ProcessItemAdded);
            }
            catch (CollectionIsReadOnlyException)
            {
                ConsoleIO.WriteLine(Messages.CollectionIsReadOnlyException);
            }
            catch (CollectionIsFullException)
            {
                ConsoleIO.WriteLine(Messages.CollectionIsFullException);
            }
        }

        private static void Task4ProcessRemove(ref BinaryTree<Person>? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            try
            {
                var count = InputCount(tree.Capacity);
                var persons = new Person[count];
                for (int i = 0; i < count; i++)
                {
                    persons[i] = new();
                    persons[i].Init();
                }
                tree.RemoveAll(persons);
                ConsoleIO.WriteLine(Messages.Task4ProcessItemRemoved);
            }
            catch (CollectionIsReadOnlyException)
            {
                ConsoleIO.WriteLine(Messages.CollectionIsReadOnlyException);
            }
            catch (CollectionIsEmptyException)
            {
                ConsoleIO.WriteLine(Messages.CollectionIsEmptyException);
            }
        }

        private static void Task4ProcessClear(ref BinaryTree<Person>? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            try
            {
                tree.Clear();
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeCleared);
            }
            catch (CollectionIsReadOnlyException)
            {
                ConsoleIO.WriteLine(Messages.CollectionIsReadOnlyException);
            }
        }

        private static void Task4ProcessContains(ref BinaryTree<Person>? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            var person = new Person();
            person.Init();
            if (tree.Contains(person))
                ConsoleIO.WriteLine(Messages.PersonFound);
            else
                ConsoleIO.WriteLine(Messages.PersonNotFound);
        }

        private static void Task4ProcessCompareWithCopies(ref BinaryTree<Person>? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            var clone = (BinaryTree<Person>)tree.Clone();
            var copy = tree.ShallowCopy();
            ConsoleIO.WriteLineFormat(Messages.Task4ProcessIsTreeCloneEquals, clone.Equals(tree));
            ConsoleIO.WriteLineFormat(Messages.Task4ProcessIsTreeCopyEquals, copy.Equals(tree));
        }

        private static void Task4ProcessDispose(ref BinaryTree<Person>? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            tree.Dispose();
            tree = null;
            ConsoleIO.WriteLine(Messages.Task4ProcessTreeDeleted);
        }

    }
}