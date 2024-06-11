public class Visualizer
{
    public static void VisualizeAVLTree<TKey, TValue>(AVLTree<TKey, TValue>.Node root, int depth = 0, string indent = "") where TKey : IComparable<TKey>
    {
        if (root == null || depth > 4)
        {
            Console.WriteLine(indent + "...");
            return;
        }

        if (depth == 0)
        {
            Console.WriteLine("Корень: " + root.Key);
        }
        else
        {
            Console.WriteLine(indent + "+-- " + root.Key);
        }

        indent += depth == 0 ? "    " : "|   ";

        if (root.Left != null)
        {
            Console.Write(indent);
            Console.WriteLine("L:");
            VisualizeAVLTree(root.Left, depth + 1, indent);
        }
        else
        {
            Console.Write(indent);
            Console.WriteLine("L: null");
        }

        if (root.Right != null)
        {
            Console.Write(indent);
            Console.WriteLine("R:");
            VisualizeAVLTree(root.Right, depth + 1, indent);
        }
        else
        {
            Console.Write(indent);
            Console.WriteLine("R: null");
        }
    }

    public static void VisualizeHashMap<TKey, TValue>(HashMap<TKey, TValue> hashMap)
    {
        for (int i = 0; i < hashMap.Size; i++)
        {
            Console.Write($"Bucket {i}: ");
            foreach (var pair in hashMap.GetBucket(i))
            {
                Console.Write($"[{pair.Key}, {pair.Value}] ");
            }
            Console.WriteLine();
        }
    }
}
