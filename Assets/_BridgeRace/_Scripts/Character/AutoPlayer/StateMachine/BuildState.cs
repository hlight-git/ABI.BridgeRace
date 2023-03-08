using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildState : AbstractBotState
{
    private Bridge targetBridge;
    private int listIndexOfFillTarget;
    public BuildState(AutoPlayCharacter bot) : base(bot) { }
    public override void OnEnter()
    {
        targetBridge = BridgeGreedyChoose();
        KeepBuildingIfPossible();
    }
    public override void OnExit()
    {
        targetBridge = null;
        listIndexOfFillTarget = 0;
    }

    public override void OnUpdate()
    {

    }
    void KeepBuildingIfPossible()
    {
        if (bot.Stack.Count > 0)
        {
            if (listIndexOfFillTarget < targetBridge.Length - 1)
            {
                listIndexOfFillTarget++;
                bot.NavDestination.AddTargetPosition(targetBridge.UnBricks[listIndexOfFillTarget].transform.position, KeepBuildingIfPossible);
            }
            else
            {
                bot.NavDestination.AddTargetPosition(targetBridge.NextFloorAdapter.transform.position);
            }
        }
        else
        {
            bot.ChangeState(bot.StandStillState);
        }
    }
    int CountColorBrick(Bridge bridge, bool countSameColor)
    {
        if (countSameColor)
        {
            return bridge.UnBricks.Count(brick => brick.Color == bot.Color);
        }
        return bridge.UnBricks.Count(brick => brick.Color != bot.Color && brick.Color != BaseColor.Transparent);
    }
    Bridge BridgeGreedyChoose()
    {
        // Default is a random bridge
        Bridge bestBridge = bot.CurrentFloor.Bridges[Random.Range(0, bot.CurrentFloor.Bridges.Count - 1)];
        int bestSameColorCount = CountColorBrick(bestBridge, true);
        int bestDiffColorCount = CountColorBrick(bestBridge, false);

        // Choose the one with as many unbrick of same color as possible and unbrick of different color as few as possible  
        for (int i = 0; i < bot.CurrentFloor.Bridges.Count; i++)
        {
            int sameColorCount = CountColorBrick(bot.CurrentFloor.Bridges[i], true);
            int diffColorCount = CountColorBrick(bot.CurrentFloor.Bridges[i], false);
            if (sameColorCount > bestSameColorCount || (sameColorCount == bestSameColorCount && bestDiffColorCount > diffColorCount))
            {
                bestBridge = bot.CurrentFloor.Bridges[i];
                bestSameColorCount = sameColorCount;
                bestDiffColorCount = diffColorCount;
            }
        }
        return bestBridge;
    }
}
