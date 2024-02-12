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
        base.EnterState();
        Debug.Log("Listen");
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
        if (Input.GetKeyDown(KeyCode.RightShift)) StateMachine.ChangeState(Player.AttackState);
    }
}
