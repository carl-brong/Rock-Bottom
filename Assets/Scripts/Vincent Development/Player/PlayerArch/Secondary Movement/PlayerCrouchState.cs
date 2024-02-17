using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCrouchState : PlayerSecondaryMovementState
{

    public PlayerCrouchState(Player player, StateMachine<PlayerSecondaryMovementState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        Player.Bc.size = new Vector2(Player.Bc.size.x, Player.Bc.size.y * 0.5f);
        Player.Bc.offset = new Vector2(Player.Bc.offset.x, Player.Bc.offset.y - 0.25f);
        Player.Anim.SetBool("isCrouching", true);
    }

    public override void ExitState()
    {
        Player.Bc.size = new Vector2(Player.Bc.size.x, Player.Bc.size.y * 2f);
        Player.Bc.offset = new Vector2(Player.Bc.offset.x, Player.Bc.offset.y + 0.25f);
        Player.Anim.SetBool("isCrouching", false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        if (Player.Controls.actions["Crouch"].WasReleasedThisFrame()) StateMachine.ChangeState(StateMachine.PreviousState);
        if (!Player.OnGround()) StateMachine.ChangeState(StateMachine.PreviousState);
        if (Player.Controls.actions["Jump"].WasPressedThisFrame()) StateMachine.ChangeState(Player.JumpState);
    }
    
}
