using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryIdleState : PlayerPrimaryMovementState
{
    public PlayerPrimaryIdleState(Player player, StateMachine<PlayerPrimaryMovementState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void Update()
    {
        if (Player.Controls.actions["Move"].IsPressed())
        {
            StateMachine.ChangeState(Player.MoveState);
        }
    }
}
