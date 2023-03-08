using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandStillState : AbstractBotState
{
    public StandStillState(AutoPlayCharacter bot) : base(bot) { }
    public override void OnEnter()
    {
        bot.NavDestination.ClearTargets();
        if (bot.IsMoving)
        {
            bot.Movement.StopRunning();
        }
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        if (bot.CurrentFloor != null && bot.CurrentFloor.Count(bot.Color) > 0){
            bot.ChangeState(bot.CollectState);
        }
    }
}
