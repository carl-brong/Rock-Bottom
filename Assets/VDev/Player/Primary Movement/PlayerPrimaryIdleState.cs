using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class PlayerPrimaryIdleState : PlayerPrimaryMovementState
{
    public PlayerPrimaryIdleState(Player player, StateMachine<PlayerPrimaryMovementState> stateMachine) : base(player, stateMachine)
    {
        Player.input.MoveEvent += GetInput;
    }

    private bool _doMove = false;
    
    public override void Update()
    {
        if (_doMove)
        {
            StateMachine.ChangeState(Player.MoveState);
        }
    }

    private void GetInput(float val)
    {
        _doMove = val != 0;
    }
}
