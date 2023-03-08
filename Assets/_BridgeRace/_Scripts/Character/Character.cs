using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbstractAnimatedCharacter
{
    [SerializeField] private Transform spawnPoint;

    private CapsuleCollider interactiveCollider;

    public CharacterStack Stack;
    public Floor CurrentFloor;

    public Rigidbody Rigidbody { get; private set; }
    public AbstractCharacterMovement Movement { get; private set; }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case GameConstant.Tag.CHARACTER:
                OnHit(collision.transform);
                break;
            case GameConstant.Tag.COLLECTED_BRICK:
                CollectedBrick collectedBrick = collision.transform.GetComponent<CollectedBrick>();
                TakeBrick(collectedBrick);
                break;
            default:
                break;
        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case GameConstant.Tag.GROUND_BRICK:
                GroundBrick groundBrick = other.GetComponent<GroundBrick>();
                if (CanTakeBrick(groundBrick))
                {
                    TakeBrick(groundBrick);
                }
                break;
            case GameConstant.Tag.UNBRICK:
                UnBrick unBrick = other.GetComponent<UnBrick>();
                if (CanDropBrick(unBrick))
                {
                    DropBrick(unBrick);
                }
                break;
            default:
                break;
        }
    }
    protected override void OnInit()
    {
        base.OnInit();
        transform.position = spawnPoint.position;
        interactiveCollider = GetComponent<CapsuleCollider>();
        Rigidbody = GetComponent<Rigidbody>();
        Movement = GetComponent<AbstractCharacterMovement>();

        LevelManager.Instance.OnGameOverEvents += OnGameOver;
    }
    protected override void OnHit(Transform otherTransform)
    {
        bool IsMovingOnAnUnBrick()
        {
            return Physics.Raycast(transform.position + transform.up, Vector3.down, 2f, 1 << LayerMask.NameToLayer(GameConstant.Layer.UNBRICK));
        }
        if (otherTransform.GetComponent<Character>().Stack.Count < Stack.Count)
        {
            if (IsIdle)
            {
                Rigidbody.velocity = Vector3.zero;
            }
            return;
        }
        if (IsFalling || IsMovingOnAnUnBrick())
        {
            return;
        }

        transform.forward = otherTransform.position - transform.position;
        StartCoroutine(Fall());
    }
    protected bool CanTakeBrick(GroundBrick brick)
    {
        return brick.Color == Color;
    }
    protected bool CanDropBrick(UnBrick unBrick)
    {
        return unBrick.Color != Color;
    }
    protected override void DropBrick(UnBrick unBrick)
    {
        if (Stack.Count == 0)
        {
            return;
        }
        Stack.PopBrick();
        unBrick.Filled(this);
    }

    protected override void TakeBrick(GroundBrick groundBrick)
    {
        CollectedBrick brick = SimplePool.Spawn<CollectedBrick>(PoolType.CollectedBrick, Stack.transform);
        brick.ChangeColor(Color);
        Stack.PushBrick(brick, groundBrick.transform.position);
    }

    protected override void TakeBrick(CollectedBrick collectedBrick)
    {
        collectedBrick.ChangeColor(Color);
        Stack.PushBrick(collectedBrick, collectedBrick.transform.position);
    }
    public override void OnGameOver(Character winner)
    {
        if (winner == this)
        {
            Movement.StopRunning();
            Stack.Clear();
            SetInteractable(false);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Dance();
        }
    }
    public override IEnumerator Fall()
    {
        Movement.StopRunning();
        SetInteractable(false);
        Stack.Collapse();
        BePushedBack();
        yield return base.Fall();
        SetInteractable(true);
    }
    private void BePushedBack()
    {
        if (Movement.CanMoveToward(-transform.forward, out Vector3 pushedDirection))
        {
            transform.position += pushedDirection * 0.2f;
        }
    }
    protected void SetInteractable(bool isTrue)
    {
        Rigidbody.isKinematic = !isTrue;
        interactiveCollider.enabled = isTrue;
    }
    private void OnDestroy()
    {
        LevelManager.Instance.OnGameOverEvents -= OnGameOver;
    }
}
