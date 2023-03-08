using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAdapter : MonoBehaviour
{
    public Floor Floor;
    private void OnTriggerEnter(Collider other)
    {
        if (Floor == null)
        {
            return;
        }
        if (other.CompareTag(GameConstant.Tag.CHARACTER))
        {
            Character character = other.gameObject.GetComponent<Character>();
            if (character.CurrentFloor == Floor)
            {
                return;
            }
            if (character.CurrentFloor != null)
            {
                character.CurrentFloor.CollectBricks(character.Color);
            }
            character.CurrentFloor = Floor;
            Floor.MassSpawn(character.Color);
        }
    }
}
