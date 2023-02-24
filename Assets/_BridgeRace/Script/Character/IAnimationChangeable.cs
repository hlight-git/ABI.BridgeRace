using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationChangeable <EnumAnimState>
{
    public void ChangeAnim(EnumAnimState state);
}
