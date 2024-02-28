using System.Collections;
using UnityEngine;



public class PlayerSecondaryIdleState : PlayerSecondaryMovementState
{
    private float _coyoteCounter;

    public PlayerSecondaryIdleState(Player player, StateMachine<PlayerSecondaryMovementState> stateMachine) : base(player, stateMachine)
    {
        Player.input.JumpEvent += HandleJump;
        Player.input.CrouchEvent += HandleCrouch;
    }

    public override void Update()
    {
        _coyoteCounter -= Time.deltaTime;
        if (Player.OnGround()) _coyoteCounter = Player.Data.coyoteTime;
        if (_coyoteCounter < 0 && Player.Rb.velocity.y < Mathf.Epsilon) StateMachine.ChangeState(Player.FallState);
    }

    private void HandleJump()
    {
        if (StateMachine.CurrentState == this && _coyoteCounter > 0)
        {
            _coyoteCounter = 0;
            StateMachine.ChangeState(Player.JumpState);
            
        }
    }

    private void HandleCrouch()
    {
        if (Player.OnGround())
        {
            StateMachine.ChangeState(Player.CrouchState);
        }
    }
    
}
