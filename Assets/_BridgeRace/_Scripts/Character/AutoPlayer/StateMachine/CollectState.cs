using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : AbstractBotState
{

    int collectTarget;
    GroundBrick targetBrick;
    public CollectState(AutoPlayCharacter stateMachine) : base(stateMachine)
    {
    }
    public override void OnEnter()
    {
        bot.NavDestination.ReachedDestinationActions += AfterCollectABrick;
        MakeTarget();
        FindAGroundBrick();
    }

    public override void OnExit()
    {
        targetBrick = null;
        bot.NavDestination.ReachedDestinationActions -= AfterCollectABrick;
    }

    public override void OnUpdate()
    {
    }
    void MakeTarget()
    {
        int floorRange = 1;
        int cellRange = Mathf.Min(bot.CurrentFloor.Count(bot.Color), 10);
        collectTarget = Random.Range(floorRange, cellRange + 1);
    }
    void FindAGroundBrick()
    {
        targetBrick = bot.CurrentFloor.GetAnActiveBrick(bot.Color);
        if (targetBrick != null)
        {
            bot.NavDestination.AddTargetPosition(targetBrick.transform.position);
            return;
        }
        if (bot.Stack.Count > 0)
        {
            bot.ChangeState(bot.BuildState);
        }
        else
        {
            bot.ChangeState(bot.StandStillState);
        }

    }
    private void AfterCollectABrick()
    {
        if (bot.Stack.Count < collectTarget)
        {
            FindAGroundBrick();
        }
        else
        {
            bot.ChangeState(bot.BuildState);
        }
    }
}
