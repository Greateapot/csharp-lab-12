namespace Lab12
{
    public static class Messages
    {
        public const string BooleanDialogYes = "Да";
        public const string BooleanDialogNo = "Нет";

        public const string Task4DialogTitle = "Работа с обобщенной коллекцией:";
        public const string Task4DialogOptionInitUnlimited = "Создать пустую неограниченную коллекцию";
        public const string Task4DialogOptionInitLimited = "Создать пустую ограниченную коллекцию";
        public const string Task4DialogOptionMakeReadOnly = "Сделать коллекцию доступной только для чтения/для чтения и записи";
        public const string Task4DialogOptionAdd = "Добавить элемент в коллекцию";
        public const string Task4DialogOptionRemove = "Удалить элемент из коллекции";
        public const string Task4DialogOptionClear = "Очистка коллекции";
        public const string Task4DialogOptionContains = "Поиск элемента по значению";
        public const string Task4DialogOptionCompareWithCopies = "Сравнить коллекцию с копиями";
        public const string Task4DialogOptionDispose = "Удалить коллекцию из памяти";
        public const string Task4DialogOptionExit = "Выход";

        public const string InputCapacity = "Введите максимально допустимое количество элементов в коллекции: ";
        public const string InputCapacityValueLessThanOne = "Количество не может быть меньше 1";

        public const string InputCount = "Введите количество элементов: ";
        public const string InputCountValueLessThanOne = "Количество не может быть меньше 1";
        public const string InputCountValueLessThanCapacity = "Количество не может быть больше макс. допустимого кол-ва элементов в коллекции";


        public const string PersonNotFound = "Персона не найдена.";
        public const string PersonFound = "Персона найдена.";

        public const string CollectionIsFullException = "Ошибка: коллекция переполнена.";
        public const string CollectionIsReadOnlyException = "Ошибка: коллекция доступна только для чтения.";


        public const string PrintTree = "Бинарное дерево:\n{0}\n";

        public const string Task4ProcessTreeAlreadyExists = "Дерево уже существует.";
        public const string Task4ProcessTreeNotExists = "Дерево не существует.";
        public const string Task4ProcessTreeDeleted = "Дерево удалено успешно.";
        public const string Task4ProcessItemAdded = "Элемент успешно добавлен.";
        public const string Task4ProcessItemRemoved = "Элемент успешно удален.";
        public const string Task4ProcessTreeCleared = "Дерево успешно очищено.";
        public const string Task4ProcessMakeTreeReadOnly = "Сделать дерево доступным только для чтения?";
        public const string Task4ProcessTreeIsReadOnly = "Теперь дерево доступно только для чтения.";
        public const string Task4ProcessTreeIsNotReadOnly = "Теперь дерево доступно для чтения и записи.";
        public const string Task4ProcessIsTreeCloneEquals = "Эквивалентен ли клон дерева дереву: {0}";
        public const string Task4ProcessIsTreeCopyEquals = "Эквивалентна ли копия дерева дереву: {0}";
    }
}