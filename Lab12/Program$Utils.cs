using ConsoleDialogLib;
using ConsoleIOLib;

namespace Lab12
{
    public partial class Program
    {
        private static int InputCapacity() => ConsoleIO.Input<int>(
            Messages.InputCapacity,
            v => v < 1
                ? Messages.InputCapacityValueLessThanOne
                : null
        );

        private static int InputSize() => ConsoleIO.Input<int>(
            Messages.InputSize,
            v => v < 1
                ? Messages.InputSizeValueLessThanOne
                : null
        );

        private static int InputCount(int capacity = 0) => ConsoleIO.Input<int>(
            Messages.InputCount,
            v => v < 1
                ? Messages.InputCountValueLessThanOne
                : capacity > 0 && v > capacity
                    ? Messages.InputCountValueLessThanCapacity
                    : null
        );

        private static bool InputBoolean(string title)
            => (bool)ConsoleDialog.ShowDialog(BooleanDialog(title))!;
    }
}