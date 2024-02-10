using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryIdleState : PlayerPrimaryMovementState
{
    public PlayerPrimaryIdleState(Player player, StateMachine<PlayerPrimaryMovementState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Primary Idle");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) StateMachine.ChangeState(Player.MoveState);
    }
}
