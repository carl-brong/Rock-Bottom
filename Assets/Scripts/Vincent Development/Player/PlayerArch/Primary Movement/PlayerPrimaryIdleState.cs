using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryIdleState : PlayerPrimaryMovementState
{
    public PlayerPrimaryIdleState(Player player, StateMachine<PlayerPrimaryMovementState> stateMachine) : base(player, stateMachine)
    {
        Player.input.MoveEvent += GetInput;
    }

    private bool recvIn = false;
    
    public override void Update()
    {
        if (recvIn)
        {
            StateMachine.ChangeState(Player.MoveState);
        }
    }

    private void GetInput(float val)
    {
        recvIn = val != 0;
    }
}
