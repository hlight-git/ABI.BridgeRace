using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseCharecter : MonoBehaviour
{
    [SerializeField] protected Transform stack;
    [SerializeField] protected Animator animator;

    protected virtual void Awake()
    {
        OnInit();
    }
    protected abstract void OnInit();
    protected abstract void TakeBrick();
    protected abstract void DropBrick();
    protected abstract void Dance();
    protected abstract void Idle();
    protected abstract void Run();
    protected abstract void OnHit(Transform otherTransform);
}
