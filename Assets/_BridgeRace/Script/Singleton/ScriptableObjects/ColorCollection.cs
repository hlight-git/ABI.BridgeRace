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

[CreateAssetMenu(fileName = "Color Collection", menuName = "Scriptable Collection/Color")]
public class ColorCollection :
    AbstractScriptableCollection<ColorCollection, BaseColor, Material>
{
    public static void ChangeColor(Renderer rend, BaseColor color)
    {
        rend.material = instance.GetElement(color);
    }
}
