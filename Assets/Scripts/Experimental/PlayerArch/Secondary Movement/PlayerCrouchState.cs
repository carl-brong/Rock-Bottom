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
        base.EnterState();
        Debug.Log("Crouch");
        Player.Bc.size = new Vector2(Player.Bc.size.x, Player.Bc.size.y * 0.5f);
        Player.Bc.offset = new Vector2(Player.Bc.offset.x, Player.Bc.offset.y - 0.25f);
    }

    public override void ExitState()
    {
        base.ExitState();
        Player.Bc.size = new Vector2(Player.Bc.size.x, Player.Bc.size.y * 2f);
        Player.Bc.offset = new Vector2(Player.Bc.offset.x, Player.Bc.offset.y + 0.25f);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.S)) StateMachine.ChangeState(StateMachine.PreviousState);
        if (!Player.OnGround()) StateMachine.ChangeState(StateMachine.PreviousState);
        if (Input.GetKeyDown(KeyCode.Space)) StateMachine.ChangeState(Player.JumpState);
    }
}
