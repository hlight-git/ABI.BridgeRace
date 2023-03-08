using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAnimatedCharacter :
    AbstractBaseCharecter,
    IAnimationChangeable<CharacterAnimState>,
    IColorChangeable<BaseColor>
{
    [SerializeField] protected CharacterAnimationDictionary animDict;
    [SerializeField] protected SkinnedMeshRenderer charRenderer;

    public BaseColor Color;
    public Animator Animator { get; private set; }
    public CharacterAnimState CurrentAnimState { get; private set; }
    public bool IsIdle => CurrentAnimState == CharacterAnimState.Idle;
    public bool IsMoving => CurrentAnimState == CharacterAnimState.Run;
    public bool IsFalling => CurrentAnimState == CharacterAnimState.Fly;

    protected override void OnInit()
    {
        Animator = GetComponent<Animator>();
        ChangeAnim(CharacterAnimState.Idle);
        ChangeColor(Color);
    }
    public override void Dance()
    {
        ChangeAnim(CharacterAnimState.Dance);
    }
    public override IEnumerator Fall()
    {
        ChangeAnim(CharacterAnimState.Fly);
        yield return new WaitForSeconds(animDict.GetData(CharacterAnimState.Fly).Duration);
        Idle();
    }
    public override void Idle()
    {
        ChangeAnim(CharacterAnimState.Idle);
    }
    public override void Run()
    {
        ChangeAnim(CharacterAnimState.Run);
    }

    public void ChangeAnim(CharacterAnimState state)
    {
        if (state == CurrentAnimState)
        {
            return;
        }
        string currentAnimStateTriggerName = animDict.GetData(CurrentAnimState).Trigger;
        string stateTriggerName = animDict.GetData(state).Trigger;

        Animator.ResetTrigger(currentAnimStateTriggerName);
        CurrentAnimState = state;
        Animator.SetTrigger(stateTriggerName);
    }

    public void ChangeColor(BaseColor color)
    {
        charRenderer.material = BaseColorDictionary.Instance.GetData(color).Material;
        this.Color = color;
    }
}
