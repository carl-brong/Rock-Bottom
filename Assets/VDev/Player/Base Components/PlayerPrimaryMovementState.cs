using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class PlayerPrimaryMovementState : BaseState
{
    protected Player Player;
    protected StateMachine<PlayerPrimaryMovementState> StateMachine;

    public PlayerPrimaryMovementState(Player player, StateMachine<PlayerPrimaryMovementState> stateMachine)
    {
        this.Player = player;
        this.StateMachine = stateMachine;
    }
}
