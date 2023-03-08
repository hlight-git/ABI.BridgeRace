using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayCharacterMovement : AbstractCharacterMovement
{
    public override void StopRunning()
    {
        character.Rigidbody.velocity = Vector3.zero;
        if (character.IsMoving)
        {
            character.Idle();
        }
    }
}
