using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(Player player, StateMachine<PlayerActionState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
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
    }
}
