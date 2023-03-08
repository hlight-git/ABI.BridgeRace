using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractBaseCharecter : MonoBehaviour
{
    void Awake() => OnInit();
    protected abstract void OnInit();
    protected abstract void OnHit(Transform otherTransform);
    protected abstract void TakeBrick(GroundBrick groundBrick);
    protected abstract void TakeBrick(CollectedBrick collectedBrick);
    protected abstract void DropBrick(UnBrick unBrick);
    public abstract void OnGameOver(Character winner);
    public abstract void Dance();
    public abstract IEnumerator Fall();
    public abstract void Idle();
    public abstract void Run();
}
