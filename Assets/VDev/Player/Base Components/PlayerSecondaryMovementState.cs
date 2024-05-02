using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class PlayerSecondaryMovementState : BaseState
{
    protected Player Player;
    protected StateMachine<PlayerSecondaryMovementState> StateMachine;

    public PlayerSecondaryMovementState(Player player, StateMachine<PlayerSecondaryMovementState> stateMachine)
    {
        this.Player = player;
        this.StateMachine = stateMachine;
    }
}
