using System.Collections;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class PlayerSecondaryIdleState : PlayerSecondaryMovementState
{
    private float _coyoteCounter;
    private bool _grounded;

    public PlayerSecondaryIdleState(Player player, StateMachine<PlayerSecondaryMovementState> stateMachine) : base(player, stateMachine)
    {
        Player.input.JumpEvent += HandleJump;
        Player.input.CrouchEvent += HandleCrouch;
    }

    public override void Update()
    {
        _coyoteCounter -= Time.deltaTime;
        if (Player.OnGround())
        {
            _grounded = true;
            _coyoteCounter = Player.Data.coyoteTime;
        }

        if (_coyoteCounter < 0 && Player.Rb.velocity.y < Mathf.Epsilon) StateMachine.ChangeState(Player.FallState);
    }

    private void HandleJump()
    {
        if (StateMachine.CurrentState == this && _coyoteCounter > 0)
        {
            _coyoteCounter = 0;
            _grounded = false;
            StateMachine.ChangeState(Player.JumpState);
            
        }
    }

    private void HandleCrouch()
    {
        Debug.Log(_grounded);
        if (_grounded)
        {
            StateMachine.ChangeState(Player.CrouchState);
        }
    }
    
}
