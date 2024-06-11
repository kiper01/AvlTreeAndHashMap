namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            var avlTree = new AVLTree<int, string>();
            var hashMap = new HashMap<int, string>(10);

            while (true)
            {
                Console.WriteLine("Выберите опцию:");
                Console.WriteLine("1. Управление АВЛ-деревом");
                Console.WriteLine("2. Управление хеш-таблицей");
                Console.WriteLine("3. Тестирование производительности");
                Console.WriteLine("4. Визуализация структур данных");
                Console.WriteLine("5. Выход");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageAVLTree(avlTree);
                        break;
                    case "2":
                        ManageHashMap(hashMap);
                        break;
                    case "3":
                        Console.WriteLine("Введите количество операций для тестирования:");
                        int numberOfOperations = int.Parse(Console.ReadLine());
                        var tester = new DataTester(numberOfOperations);
                        tester.RunTests();
                        break;
                    case "4":
                        VisualizeDataStructures(avlTree, hashMap);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void ManageAVLTree(AVLTree<int, string> avlTree)
        {
            while (true)
            {
                Console.WriteLine("Управление АВЛ-деревом:");
                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Удалить элемент");
                Console.WriteLine("3. Найти элемент");
                Console.WriteLine("4. Вернуться назад");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введите ключ:");
                        int key = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите значение:");
                        string newValue = Console.ReadLine();
                        avlTree.Add(key, newValue);
                        Console.WriteLine("Элемент добавлен.");
                        break;
                    case "2":
                        Console.WriteLine("Введите ключ для удаления:");
                        key = int.Parse(Console.ReadLine());
                        if (avlTree.Remove(key))
                        {
                            Console.WriteLine("Элемент удален.");
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Введите ключ для поиска:");
                        key = int.Parse(Console.ReadLine());
                        if (avlTree.TryGetValue(key, out string value))
                        {
                            Console.WriteLine($"Найдено значение: {value}");
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден.");
                        }
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void ManageHashMap(HashMap<int, string> hashMap)
        {
            while (true)
            {
                Console.WriteLine("Управление хеш-таблицей:");
                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Удалить элемент");
                Console.WriteLine("3. Найти элемент");
                Console.WriteLine("4. Вернуться назад");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введите ключ:");
                        int key = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите значение:");
                        string newValue = Console.ReadLine();
                        hashMap.Add(key, newValue);
                        Console.WriteLine("Элемент добавлен.");
                        break;
                    case "2":
                        Console.WriteLine("Введите ключ для удаления:");
                        key = int.Parse(Console.ReadLine());
                        if (hashMap.Remove(key))
                        {
                            Console.WriteLine("Элемент удален.");
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Введите ключ для поиска:");
                        key = int.Parse(Console.ReadLine());
                        if (hashMap.TryGetValue(key, out string value))
                        {
                            Console.WriteLine($"Найдено значение: {value}");
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден.");
                        }
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void VisualizeDataStructures(AVLTree<int, string> avlTree, HashMap<int, string> hashMap)
        {
            Console.WriteLine("Визуализация АВЛ-дерева:");
            Visualizer.VisualizeAVLTree(avlTree.GetRoot());

            Console.WriteLine("Визуализация хеш-таблицы:");
            Visualizer.VisualizeHashMap(hashMap);
        }
    }
}
