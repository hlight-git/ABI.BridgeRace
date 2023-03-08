using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterAnimState
{
    Dance,
    Fly,
    Idle,
    Run,
}

[CreateAssetMenu(fileName = "Character Animation Dictionary", menuName = "Scriptable Object/Dictionary/Animation/Character")]
public class CharacterAnimationDictionary : ScriptableDictionary<CharacterAnimState, AnimationData<CharacterAnimState>> { }