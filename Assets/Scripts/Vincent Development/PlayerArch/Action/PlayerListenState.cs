using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListenState : PlayerActionState
{
    public PlayerListenState(Player player, StateMachine<PlayerActionState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift)) StateMachine.ChangeState(Player.AttackState);
    }
}
