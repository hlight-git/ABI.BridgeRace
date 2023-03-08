using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VictoryCupAnimState
{
    None,
    Victory,
    Rotating,
}
[CreateAssetMenu(fileName = "Victory Cup Animation Dictionary", menuName = "Scriptable Object/Dictionary/Animation/Victory Cup")]
public class VictoryCupAnimationDictionary : ScriptableDictionary<VictoryCupAnimState, AnimationData<VictoryCupAnimState>> { }