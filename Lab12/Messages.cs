namespace Lab12
{
    public static class Messages
    {
        public const string MainDialogTitle = "Выберите задание:";
        public const string MainDialogOptionTask1 = "Двунаправленный список";
        public const string MainDialogOptionTask2 = "Бинарное дерево";
        public const string MainDialogOptionTask3 = "Хеш-таблица";
        public const string MainDialogOptionTask4 = "Обобщенная коллекция (бинарное дерево)";

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

        public const string InputSize = "Введите размер хеш-таблицы: ";
        public const string InputSizeValueLessThanOne = "Размер не может быть меньше 1";

        public const string InputCount = "Введите количество элементов: ";
        public const string InputCountValueLessThanOne = "Количество не может быть меньше 1";
        public const string InputCountValueLessThanCapacity = "Количество не может быть больше макс. допустимого кол-ва элементов в коллекции";

        public const string InputGenerateRandomPersons = "Сгенерировать случайных персон? (В противном случае, придется вводить данные персон вручную)";
        public const string InputGenerateRandomPerson = "Сгенерировать одну случайную персону? (В противном случае, придется вводить данные персоны вручную)";
        public const string InputAfterPerson = "Введите данные персоны, после которого надо вставить новый элемент.";
        public const string InputSearchPerson = "Введите данные искомой персоны, чтобы проверить наличие его в коллекции.";

        public const string PersonNotFound = "Персона не найдена.";
        public const string PersonFound = "Персона найдена.";


        public const string CollectionIsEmptyException = "Ошибка: коллекция пуста.";
        public const string CollectionIsFullException = "Ошибка: коллекция переполнена.";
        public const string CollectionIsReadOnlyException = "Ошибка: коллекция доступна только для чтения.";

        public const string PrintList = "Двунаправленный список:\n{0}\n";

        public const string PrintTree = "Бинарное дерево:\n{0}\n";
        public const string PrintTreeLeafCount = "Количество листьев: {0}";

        public const string PrintHashTable = "Хеш-таблица:\n{0}\n";
        public const string PrintHashTableRemovingResult = "Результат удаления элемента: {0}";
        public const string PrintHashTableContainsResult = "Содержит ли коллекция элемент: {0}";

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