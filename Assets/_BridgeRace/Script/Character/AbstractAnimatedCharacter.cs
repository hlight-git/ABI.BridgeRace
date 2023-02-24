using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAnimatedCharacter :
    AbstractBaseCharecter,
    IAnimationChangeable<CharacterAnimState>
{
    [SerializeField] protected SkinnedMeshRenderer charRenderer;
    protected CharacterAnimState currentAnimState;

    protected override void OnInit()
    {
        currentAnimState = CharacterAnimState.Idle;
    }
    protected override void Dance()
    {
        ChangeAnim(CharacterAnimState.Dance);
    }
    protected override void Idle()
    {
        ChangeAnim(CharacterAnimState.Idle);
    }
    protected override void Run()
    {
        ChangeAnim(CharacterAnimState.Run);
    }
    protected override void OnHit(Transform otherTransform)
    {
        ChangeAnim(CharacterAnimState.Fly);
        Invoke(nameof(Idle), CharacterAnimationCollection.instance.GetDuration(CharacterAnimState.Fly));
    }

    public void ChangeAnim(CharacterAnimState state)
    {
        if (state == currentAnimState)
        {
            return;
        }
        string currentAnimStateTriggerName = CharacterAnimationCollection.instance.GetTriggerName(currentAnimState);
        string stateTriggerName = CharacterAnimationCollection.instance.GetTriggerName(state);

        animator.ResetTrigger(currentAnimStateTriggerName);
        currentAnimState = state;
        animator.SetTrigger(stateTriggerName);
    }
}
