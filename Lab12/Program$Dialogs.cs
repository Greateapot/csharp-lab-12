using ConsoleDialogLib;

namespace Lab12
{
    public partial class Program
    {
        private static ConsoleDialog MainDialog() => new(
            Messages.MainDialogTitle,
            [
                new ConsoleDialogOption(Messages.MainDialogOptionTask1, _ => Task1Process(), true, true, false),
                new ConsoleDialogOption(Messages.MainDialogOptionTask2, _ => Task2Process(), true, true, false),
                new ConsoleDialogOption(Messages.MainDialogOptionTask3, _ => Task3Process(), true, true, false),
                new ConsoleDialogOption(Messages.MainDialogOptionTask4, _ => Task4Process()),
            ]
        );

        private static ConsoleDialog BooleanDialog(string title) => new(
            title,
            [
                new ConsoleDialogOption(Messages.BooleanDialogYes, _ => true, true, false, true),
                new ConsoleDialogOption(Messages.BooleanDialogNo, _ => false, true, false, true),
            ],
            false,
            true,
            false
        );
    }
}