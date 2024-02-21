using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(Player player, StateMachine<PlayerActionState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        //Player.Anim.SetTrigger("Attack");
        Player.EnemyInRange();
        StateMachine.ChangeState(Player.ListenState);
    }
    
}
