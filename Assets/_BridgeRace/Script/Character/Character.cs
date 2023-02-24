using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbstractMovableCharacter
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case GameConstant.Tag.ENEMY:
                OnHit(collision.transform);
                break;
            case GameConstant.Tag.BRICK:
                TakeBrick();
                break;
            case GameConstant.Tag.UNBRICK:
                DropBrick();
                break;
            default: //Debug.Log(collision.gameObject.name);
                break;
        }
    }
    protected override void DropBrick()
    {
        throw new System.NotImplementedException();

    }

    protected override void TakeBrick()
    {
        throw new System.NotImplementedException();
    }
}
