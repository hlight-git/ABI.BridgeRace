using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BaseColor
{
    Blue,
    Gray,
    Green,
    Red,
    Transparent,
    Yellow
}

[CreateAssetMenu(fileName = "Base Color Dictionary", menuName = "Scriptable Object/Dictionary/Color/Base")]
public class BaseColorDictionary : ScriptableDictionarySingleton<BaseColorDictionary, BaseColor, ColorData<BaseColor>> { }