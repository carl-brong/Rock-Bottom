using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCrouchState : PlayerSecondaryMovementState
{

    public PlayerCrouchState(Player player, StateMachine<PlayerSecondaryMovementState> stateMachine) : base(player, stateMachine)
    {
        Player.input.CrouchCancelEvent += HandleCrouchCancel;
        Player.input.JumpEvent += HandleJump;
    }

    public override void EnterState()
    {
        Player.Bc.offset = new Vector2(Player.Bc.offset.x, Player.Bc.offset.y - 0.25f);
        Player.Bc.size = new Vector2(Player.Bc.size.x, Player.Bc.size.y * 0.5f);
        //Player.Anim.SetBool("isCrouching", true);
    }

    public override void ExitState()
    {
        Player.Bc.offset = new Vector2(Player.Bc.offset.x, Player.Bc.offset.y + 0.25f);
        Player.Bc.size = new Vector2(Player.Bc.size.x, Player.Bc.size.y * 2f);
        //Player.Anim.SetBool("isCrouching", false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        if (!Player.OnGround())
        {
            StateMachine.ChangeState(StateMachine.PreviousState);
        }
    }

    private void HandleCrouchCancel()
    {
        if (StateMachine.CurrentState == Player.CrouchState)
        {
            StateMachine.ChangeState(Player.SecondaryIdleState);
        }
    }

    private void HandleJump()
    {
        if (StateMachine.CurrentState == Player.CrouchState)
        {
            StateMachine.ChangeState(Player.JumpState);
        }
    }
    
}
