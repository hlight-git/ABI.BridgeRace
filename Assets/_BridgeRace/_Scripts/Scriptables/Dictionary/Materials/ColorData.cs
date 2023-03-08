using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ColorData<TKey> : GetByEnumData<TKey> where TKey : Enum
{
    public Material Material;
}