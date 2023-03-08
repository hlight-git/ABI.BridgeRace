using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    protected override void OnInit()
    {
        base.OnInit();
        LevelManager.Instance.SetPlayer(this);
    }
}
