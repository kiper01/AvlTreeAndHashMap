using System;
using System.Collections.Generic;

public class HashMap<TKey, TValue>
{
    private readonly int size;
    private readonly LinkedList<KeyValuePair<TKey, TValue>>[] buckets; // методом цепочек (списков) для разрешения коллизий

    public HashMap(int size)
    {
        this.size = size;
        buckets = new LinkedList<KeyValuePair<TKey, TValue>>[size];
        for (int i = 0; i < size; i++)
        {
            buckets[i] = new LinkedList<KeyValuePair<TKey, TValue>>();
        }
    }

    private int GetBucketIndex(TKey key)
    {
        return key.GetHashCode() % size;
    }
    /*
     * Эта хеш-функция является встроенной в C# и гарантирует, что одинаковые ключи будут иметь одинаковый хеш-код.
     * Значение хеш-функции быстро вычисляется для любого сообщения.
     * Невозможно найти сообщение, которое дает заданное хеш-значение.
     * 
     * Встроенная хэш функция удолетворят всем необходимым условиям, поэтому ее использование уместно.
     */

    public void Add(TKey key, TValue value)
    {
        int index = GetBucketIndex(key);
        var bucket = buckets[index];

        foreach (var pair in bucket)
        {
            if (pair.Key.Equals(key))
            {
                // Перезапись ключа
                bucket.Remove(pair);
                bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value));
                return;
            }
        }

        bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value));
    }

    public bool Remove(TKey key)
    {
        int index = GetBucketIndex(key);
        var bucket = buckets[index];

        foreach (var pair in bucket)
        {
            if (pair.Key.Equals(key))
            {
                bucket.Remove(pair);
                return true;
            }
        }

        return false;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = GetBucketIndex(key);
        var bucket = buckets[index];

        foreach (var pair in bucket)
        {
            if (pair.Key.Equals(key))
            {
                value = pair.Value;
                return true;
            }
        }

        value = default(TValue);
        return false;
    }
}
