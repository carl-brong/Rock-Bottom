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
        base.EnterState();
        Debug.Log("Attack");
        Collider2D[] hits = Physics2D.OverlapCircleAll(Player.transform.GetChild(1).position, 0.2f, Player._groundLayer);
        foreach (Collider2D hit in hits)
        {
            Debug.Log(hit);
        }
        StateMachine.ChangeState(Player.ListenState);
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
