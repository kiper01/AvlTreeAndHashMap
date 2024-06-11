using System.Diagnostics;

public class DataTester
{
    private readonly int numberOfOperations;
    private readonly Random random;

    public DataTester(int numberOfOperations)
    {
        this.numberOfOperations = numberOfOperations;
        this.random = new Random();
    }

    public void RunTests()
    {
        var avlTree = new AVLTree<int, string>();
        var hashMap = new HashMap<int, string>(numberOfOperations);

        var keys = new int[numberOfOperations];
        var values = new string[numberOfOperations];

        for (int i = 0; i < numberOfOperations; i++)
        {
            keys[i] = random.Next();
            values[i] = Guid.NewGuid().ToString();
        }

        Console.WriteLine("Тестирование добавления:");
        TestAddition(avlTree, hashMap, keys, values);

        Console.WriteLine("Тестирование поиска:");
        TestSearch(avlTree, hashMap, keys);

        Console.WriteLine("Тестирование удаления:");
        TestDeletion(avlTree, hashMap, keys);
    }

    private void TestAddition(AVLTree<int, string> avlTree, HashMap<int, string> hashMap, int[] keys, string[] values)
    {
        var avlTreeTimer = Stopwatch.StartNew();
        for (int i = 0; i < numberOfOperations; i++)
        {
            avlTree.Add(keys[i], values[i]);
        }
        avlTreeTimer.Stop();
        Console.WriteLine($"АВЛ-дерево: {avlTreeTimer.ElapsedMilliseconds} мс");

        var hashMapTimer = Stopwatch.StartNew();
        for (int i = 0; i < numberOfOperations; i++)
        {
            hashMap.Add(keys[i], values[i]);
        }
        hashMapTimer.Stop();
        Console.WriteLine($"Хеш-таблица: {hashMapTimer.ElapsedMilliseconds} мс");
    }

    private void TestSearch(AVLTree<int, string> avlTree, HashMap<int, string> hashMap, int[] keys)
    {
        var avlTreeTimer = Stopwatch.StartNew();
        for (int i = 0; i < numberOfOperations; i++)
        {
            avlTree.TryGetValue(keys[i], out _);
        }
        avlTreeTimer.Stop();
        Console.WriteLine($"АВЛ-дерево: {avlTreeTimer.ElapsedMilliseconds} мс");

        var hashMapTimer = Stopwatch.StartNew();
        for (int i = 0; i < numberOfOperations; i++)
        {
            hashMap.TryGetValue(keys[i], out _);
        }
        hashMapTimer.Stop();
        Console.WriteLine($"Хеш-таблица: {hashMapTimer.ElapsedMilliseconds} мс");
    }

    private void TestDeletion(AVLTree<int, string> avlTree, HashMap<int, string> hashMap, int[] keys)
    {
        var avlTreeTimer = Stopwatch.StartNew();
        for (int i = 0; i < numberOfOperations; i++)
        {
            avlTree.Remove(keys[i]);
        }
        avlTreeTimer.Stop();
        Console.WriteLine($"АВЛ-дерево: {avlTreeTimer.ElapsedMilliseconds} мс");

        var hashMapTimer = Stopwatch.StartNew();
        for (int i = 0; i < numberOfOperations; i++)
        {
            hashMap.Remove(keys[i]);
        }
        hashMapTimer.Stop();
        Console.WriteLine($"Хеш-таблица: {hashMapTimer.ElapsedMilliseconds} мс");
    }
}
