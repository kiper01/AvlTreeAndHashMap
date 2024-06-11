using System;

public class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
{
    private class Node
    {
        public TKey Key;
        public TValue Value;
        public Node Left;
        public Node Right;
        public int Height;

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
            node.Left = Add(node.Left, key, value);
        else if (cmp > 0)
            node.Right = Add(node.Right, key, value);
        else
            node.Value = value;

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
            (node.Left, removed) = Remove(node.Left, key);
        else if (cmp > 0)
            (node.Right, removed) = Remove(node.Right, key);
        else
        {
            removed = true;
            if (node.Left == null || node.Right == null)
            {
                node = (node.Left ?? node.Right);
            }
            else
            {
                var min = GetMin(node.Right);
                node.Key = min.Key;
                node.Value = min.Value;
                (node.Right, _) = Remove(node.Right, min.Key);
            }
        }

        return (node == null ? null : Balance(node), removed);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var node = GetNode(root, key);
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
        while (node != null)
        {
            int cmp = key.CompareTo(node.Key);
            if (cmp < 0)
                node = node.Left;
            else if (cmp > 0)
                node = node.Right;
            else
                return node;
        }

        return null;
    }

    private Node GetMin(Node node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    private Node Balance(Node node)
    {
        UpdateHeight(node);

        int balance = GetBalance(node);

        if (balance > 1)
        {
            if (GetBalance(node.Left) < 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        if (balance < -1)
        {
            if (GetBalance(node.Right) > 0)
                node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    private Node RotateRight(Node y)
    {
        var x = y.Left;
        y.Left = x.Right;
        x.Right = y;

        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private Node RotateLeft(Node x)
    {
        var y = x.Right;
        x.Right = y.Left;
        y.Left = x;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }

    private void UpdateHeight(Node node)
    {
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    }

    private int GetHeight(Node node)
    {
        return node?.Height ?? 0;
    }

    private int GetBalance(Node node)
    {
        return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
    }
}
