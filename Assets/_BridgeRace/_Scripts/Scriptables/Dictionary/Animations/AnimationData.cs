using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AnimationData<TKey> : GetByEnumData<TKey> where TKey : Enum
{
    public AnimationClip Clip;
    public string Trigger => Key.ToString();
    public float Duration => Clip.averageDuration;
}