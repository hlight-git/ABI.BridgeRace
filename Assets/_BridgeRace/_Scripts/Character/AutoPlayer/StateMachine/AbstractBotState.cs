using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBotState
{
    protected AutoPlayCharacter bot;

    public AbstractBotState(AutoPlayCharacter bot)
    {
        this.bot = bot;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
