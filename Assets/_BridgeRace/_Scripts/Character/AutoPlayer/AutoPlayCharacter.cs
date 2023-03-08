using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayCharacter : Character
{
    public NavDestination NavDestination;
    public AbstractBotState CurrentState { get; private set; }
    public StandStillState StandStillState { get; private set; }
    public CollectState CollectState { get; private set; }
    public BuildState BuildState { get; private set; }

    protected virtual void Start()
    {
        CurrentState = StandStillState;
        CurrentState.OnEnter();
    }
    protected virtual void FixedUpdate()
    {
        if (IsFalling)
        {
            return;
        }
        CurrentState.OnUpdate();
    }
    protected override void OnInit()
    {
        base.OnInit();

        StandStillState = new StandStillState(this);
        CollectState = new CollectState(this);
        BuildState = new BuildState(this);

        NavDestination.MoveActions += Run;
        NavDestination.StandStillActions += () => ChangeState(StandStillState);
    }
    protected override void OnHit(Transform otherTransform)
    {
        base.OnHit(otherTransform);
        if (IsFalling)
        {
            ChangeState(StandStillState);
        }
    }
    public override void OnGameOver(Character winner)
    {
        base.OnGameOver(winner);
        this.enabled = false;
    }
    public void ChangeState(AbstractBotState state)
    {
        CurrentState.OnExit();
        CurrentState = state;
        CurrentState.OnEnter();
    }
}
