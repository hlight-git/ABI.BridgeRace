using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBrick : MonoBehaviour, IColorChangeable<BaseColor>
{
    [SerializeField] private MeshRenderer rend;

    public BaseColor Color;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.Tag.CHARACTER))
        {
            Character character = other.GetComponent<Character>();
            if (character.Color == Color)
            {
                character.CurrentFloor.DespawnBrick(this);
            }
        }
    }
    public void ChangeColor(BaseColor color)
    {
        rend.material = BaseColorDictionary.Instance.GetData(color).Material;
        this.Color = color;
    }
}
