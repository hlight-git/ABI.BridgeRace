using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractAnimationCollection<T, K> : 
    AbstractScriptableCollection<T, K, AnimationClip>
    where T : AbstractAnimationCollection<T, K>
    where K : Enum
{
    public virtual string GetTriggerName(K animState)
    {
        return animState.ToString();
    }

    public float GetDuration(K animState)
    {
        return GetElement(animState).length;
    }
}