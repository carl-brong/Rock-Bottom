using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpState : PlayerSecondaryMovementState
{

    
    public PlayerJumpState(Player player, StateMachine<PlayerSecondaryMovementState> stateMachine) : base(player, stateMachine)
    {
        Player.input.JumpCancelEvent += HandleJumpCancel;
    }

    public override void EnterState()
    {
        Player.Anim.SetBool("isJumping", true);
        Player.Rb.velocity = new Vector2(Player.Rb.velocity.x, Player.Data.jumpModifier);
    }

    public override void ExitState()
    {
        Player.Anim.SetBool("isJumping", false);
    }

    public override void Update()
    {
        if (Player.Rb.velocity.y < Mathf.Epsilon)
        {
            StateMachine.ChangeState(Player.FallState);
        }
    }

    private void HandleJumpCancel()
    {
        /*if (StateMachine.CurrentState == this)
        {*/
            var velocity = Player.Rb.velocity;
            velocity = new Vector2(velocity.x, velocity.y * 0.5f);
            Player.Rb.velocity = velocity;
        /*}*/
    }

}
