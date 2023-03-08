using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformColor
{
    Black
}

[CreateAssetMenu(fileName = "Platform Color Dictionary", menuName = "Scriptable Object/Dictionary/Color/Platform")]
public class PlatformColorDictionary : ScriptableDictionary<PlatformColor, ColorData<PlatformColor>> { }