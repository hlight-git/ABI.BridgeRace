using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ScriptableDictionary <TKey, TData>:
    ScriptableObject
    where TKey : Enum
    where TData : GetByEnumData<TKey>
{
    [SerializeField] protected List<TData> dataset;
    public TData GetData(TKey key)
    {
        TData data = dataset.Find(data => data.Key.Equals(key));
        if (data == null)
        {
            throw new NullReferenceException($"Couldn't find any items with matching key \"{key}\"");
        }
        return data;
    }
}

public class ScriptableDictionarySingleton<TSingleton, TKey, TData> :
    ScriptableDictionary<TKey, TData>
    where TSingleton : ScriptableDictionarySingleton<TSingleton, TKey, TData>
    where TKey : Enum
    where TData : GetByEnumData<TKey>
{
    public static TSingleton Instance { get; private set; }
    protected ScriptableDictionarySingleton() => Instance = this as TSingleton;
}

[Serializable]
public class GetByEnumData<TKey> where TKey : Enum
{
    public TKey Key;
}