using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStack : MonoBehaviour
{
    private Stack<CollectedBrick> bricks;
    public int Count => bricks.Count;
    void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        bricks = new Stack<CollectedBrick>();
    }

    public void PushBrick(CollectedBrick brick, Vector3 collectPos)
    {
        Vector3 newCollectedBrickLocalPos = (GameConstant.Brick.THICHNESS + 0.1f) * bricks.Count * Vector3.up;

        bricks.Push(brick);
        if (brick.transform.parent == null)
        {
            brick.transform.parent = this.transform;
        }
        StartCoroutine(brick.BeCollected(collectPos, newCollectedBrickLocalPos));
    }

    public void PopBrick()
    {
        SimplePool.Despawn(bricks.Pop());
    }
    public void Clear()
    {
        while (Count > 0)
        {
            PopBrick();
        }
    }
    public void Collapse()
    {
        while (Count > 0)
        {
            bricks.Pop().FallDown();
        }
    }
}
