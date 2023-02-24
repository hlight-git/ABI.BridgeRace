using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMovableCharacter : AbstractAnimatedCharacter, IMovable
{
    private Rigidbody rb;
    private float moveSpeed;
    public bool IsMoving => currentAnimState == CharacterAnimState.Run;

    protected virtual void FixedUpdate()
    {
        if (IsMoving)
        {
            Moving();
        }
    }
    protected override void OnInit()
    {
        base.OnInit();
        rb = GetComponent<Rigidbody>();
    }

    protected void Moving()
    {
        if (CanMoveForward())
        {
            Run();
            Vector3 moveDirection = DetectMovingPlatform().rotation * transform.forward;
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            StopMoving();
        }
    }
    Transform DetectMovingPlatform()
    {
        return (Physics.Raycast(
            transform.position + Vector3.up / 2,
            Vector3.down,
            out RaycastHit hit,
            1f, 1 << LayerMask.NameToLayer("Platform"))
        ) ? hit.transform : null;
    }

    protected bool CanMoveForward()
    {
        float raycastLength = GameConstant.Character.HEIGHT + 0.5f;
        Vector3 origin = (
            transform.position +
            transform.forward / 3 +
            Vector3.up * GameConstant.Character.HEIGHT
        );
        return Physics.Raycast(origin, Vector3.down, raycastLength);
    }

    public void Move(Vector3 direction, float moveSpeedCoefficient)
    {
        transform.forward = direction;
        if (DetectMovingPlatform() != null)
        {
            moveSpeed = moveSpeedCoefficient * GameConfiguration.Instance.Character.baseMoveSpeed;
            animator.speed = moveSpeedCoefficient;
            Moving();
        }
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
        animator.speed = 1;
        Idle();
    }
}
