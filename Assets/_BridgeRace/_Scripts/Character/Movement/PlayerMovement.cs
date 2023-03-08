using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AbstractCharacterMovement, IJoystickControllable
{
    private float moveSpeed;
    void FixedUpdate()
    {
        if (character.IsMoving)
        {
            Moving();
        }
    }
    public override void StopRunning()
    {
        if (character.IsFalling)
        {
            return;
        }
        SetSpeedToZero();
        character.Idle();
    }
    private void Moving()
    {
        if (CanMoveToward(transform.forward, out Vector3 moveDirection))
        {
            StepForward(moveDirection);
        }
        else
        {
            SetSpeedToZero();
        }
    }
    private void SetSpeedToZero()
    {
        character.Rigidbody.velocity = Vector3.zero;
        character.Animator.speed = 1;
    }
    private void StepForward(Vector3 moveDirection)
    {
        character.Rigidbody.velocity = moveDirection * moveSpeed;
    }
    public void MoveToward(Vector3 direction, float moveSpeedCoefficient)
    {
        if (character.IsFalling)
        {
            return;
        }
        character.Run();
        transform.forward = direction;
        character.Animator.speed = moveSpeedCoefficient;
        moveSpeed = moveSpeedCoefficient * GameConfiguration.Instance.SpeedConfig.characterMovement;
        Moving();
    }
    public void StopMoving()
    {
        StopRunning();
    }
}
