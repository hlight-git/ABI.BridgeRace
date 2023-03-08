using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorChangeable <ColorCollectionEnum>
{
    public void ChangeColor(ColorCollectionEnum color);
}
