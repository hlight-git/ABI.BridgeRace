using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum CharacterAnimState
{
    Dance,
    Fly,
    Idle,
    Run,
}

[CreateAssetMenu(fileName = "Character Animation Collection", menuName = "Scriptable Collection/Character Animation")]
public class CharacterAnimationCollection
    : AbstractAnimationCollection<CharacterAnimationCollection, CharacterAnimState>{ }