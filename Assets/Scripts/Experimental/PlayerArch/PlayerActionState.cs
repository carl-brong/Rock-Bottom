using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionState : BaseState
{
    protected Player Player;
    protected StateMachine<PlayerActionState> StateMachine;

    public PlayerActionState(Player player, StateMachine<PlayerActionState> stateMachine)
    {
        this.Player = player;
        this.StateMachine = stateMachine;
    }
}
