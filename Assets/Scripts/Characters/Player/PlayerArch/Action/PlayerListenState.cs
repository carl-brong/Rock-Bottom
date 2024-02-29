using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListenState : PlayerActionState
{
    public PlayerListenState(Player player, StateMachine<PlayerActionState> stateMachine) : base(player, stateMachine)
    {
        Player.input.AttackEvent += HandleAttack;
    }

    private void HandleAttack(Vector2 _)
    {
        if (StateMachine.CurrentState == this)
        {
            StateMachine.ChangeState(Player.AttackState);
        }
    }
}
