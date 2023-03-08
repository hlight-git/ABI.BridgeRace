using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour, IColorChangeable<BaseColor>
{
    [SerializeField] private MeshRenderer rend;
    public BaseColor Color;

    public void Filled(Character character)
    {
        ChangeColor(character.Color);
        character.CurrentFloor.SpawnBrick(Color);
    }
    public void ChangeColor(BaseColor color)
    {
        rend.material = BaseColorDictionary.Instance.GetData(color).Material;
        Color = color;
    }
}
