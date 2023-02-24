using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public abstract class AbstractScriptableCollection <T, K, V> :
    ScriptableSingleton <T>
    where T : AbstractScriptableCollection<T, K, V>
    where K : Enum
{
    [SerializeField] protected List<V> collection;

    public virtual V GetElement(K key)
    {
        return collection[(int)(object)key];
    }
}