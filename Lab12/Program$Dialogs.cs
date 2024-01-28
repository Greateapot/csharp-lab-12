using ConsoleDialogLib;
using Lab10Lib.Entities;
using Lab12.BinaryTree;

namespace Lab12
{
    public partial class Program
    {

        private static ConsoleDialog MainDialog() => new(
            "main",
            [
                new ConsoleDialogOption("t1", _ => Task1Process(), pauseAfterExecuted: true),
                new ConsoleDialogOption("t2", _ => Task2Process(), pauseAfterExecuted: true),
                new ConsoleDialogOption("t3", _ => Task3Process(), pauseAfterExecuted: true),
                new ConsoleDialogOption("t4", _ => Task4Process()),
            ]
        );

        private static ConsoleDialog BooleanDialog(string title) => new(
            title,
            [
                new ConsoleDialogOption("yes", _ => true, true, false, true),
                new ConsoleDialogOption("no", _ => false, true, false, true),
            ],
            false,
            true,
            false
        );
    }
}