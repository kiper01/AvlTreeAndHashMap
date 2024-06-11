public class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
{
    public class Node
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Height { get; set; }

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Height = 1;
        }
    }

    private Node root;

    public void Add(TKey key, TValue value)
    {
        root = Add(root, key, value);
    }

    private Node Add(Node node, TKey key, TValue value)
    {
        if (node == null)
            return new Node(key, value);

        int cmp = key.CompareTo(node.Key);
        if (cmp < 0)
        {
            node.Left = Add(node.Left, key, value);
        }
        else if (cmp > 0)
        {
            node.Right = Add(node.Right, key, value);
        }
        else
        {
            node.Value = value;
            return node;
        }

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        return Balance(node);
    }

    public bool Remove(TKey key)
    {
        bool removed;
        (root, removed) = Remove(root, key);
        return removed;
    }

    private (Node, bool) Remove(Node node, TKey key)
    {
        if (node == null)
            return (null, false);

        bool removed;
        int cmp = key.CompareTo(node.Key);
        if (cmp < 0)
        {
            (node.Left, removed) = Remove(node.Left, key);
        }
        else if (cmp > 0)
        {
            (node.Right, removed) = Remove(node.Right, key);
        }
        else
        {
            removed = true;
            if (node.Left == null) return (node.Right, true);
            if (node.Right == null) return (node.Left, true);

            Node temp = node;
            node = Min(temp.Right);
            node.Right = RemoveMin(temp.Right);
            node.Left = temp.Left;
        }

        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        return (Balance(node), removed);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        Node node = GetNode(root, key);
        if (node != null)
        {
            value = node.Value;
            return true;
        }
        value = default(TValue);
        return false;
    }

    private Node GetNode(Node node, TKey key)
    {
        if (node == null)
            return null;

        int cmp = key.CompareTo(node.Key);
        if (cmp < 0)
            return GetNode(node.Left, key);
        else if (cmp > 0)
            return GetNode(node.Right, key);
        else
            return node;
    }

    private Node Min(Node node)
    {
        if (node.Left == null) return node;
        else return Min(node.Left);
    }

    private Node RemoveMin(Node node)
    {
        if (node.Left == null)
            return node.Right;
        node.Left = RemoveMin(node.Left);
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        return Balance(node);
    }

    private int Height(Node node)
    {
        return node == null ? 0 : node.Height;
    }

    private Node Balance(Node node)
    {
        if (BalanceFactor(node) > 1)
        {
            if (BalanceFactor(node.Right) < 0)
                node.Right = RotateRight(node.Right);
            node = RotateLeft(node);
        }
        else if (BalanceFactor(node) < -1)
        {
            if (BalanceFactor(node.Left) > 0)
                node.Left = RotateLeft(node.Left);
            node = RotateRight(node);
        }
        return node;
    }

    private int BalanceFactor(Node node)
    {
        return node == null ? 0 : Height(node.Right) - Height(node.Left);
    }

    private Node RotateRight(Node node)
    {
        Node temp = node.Left;
        node.Left = temp.Right;
        temp.Right = node;
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        temp.Height = Math.Max(Height(temp.Left), Height(temp.Right)) + 1;
        return temp;
    }

    private Node RotateLeft(Node node)
    {
        Node temp = node.Right;
        node.Right = temp.Left;
        temp.Left = node;
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        temp.Height = Math.Max(Height(temp.Left), Height(temp.Right)) + 1;
        return temp;
    }

    public Node GetRoot()
    {
        return root;
    }
}