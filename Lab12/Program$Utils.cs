using ConsoleDialogLib;
using ConsoleIOLib;

namespace Lab12
{
    public partial class Program
    {
        private static int InputCapacity() => ConsoleIO.Input<int>(
            "Input capacity: ",
            v => v < 1
                ? "capacity can't be less than one"
                : null
        );

        private static int InputSize() => ConsoleIO.Input<int>(
            "Input size: ",
            v => v < 1
                ? "size can't be less than one"
                : null
        );

        private static int InputCount(int capacity = 0) => ConsoleIO.Input<int>(
            "Input count: ",
            v => v < 1
                ? "count can't less than one"
                : capacity > 0 && v > capacity
                    ? "count can't be greater than capacity"
                    : null
        );

        private static bool InputBoolean(string title)
            => (bool)ConsoleDialog.ShowDialog(BooleanDialog(title))!;
    }
}