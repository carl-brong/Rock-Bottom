using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class PlayerFallState : PlayerSecondaryMovementState
{
    private float _jumpBufferCounter;
    private float _fallSpeed;

    public PlayerFallState(Player player, StateMachine<PlayerSecondaryMovementState> stateMachine) : base(player, stateMachine)
    {
        Player.input.JumpEvent += HandleJump;
    }

    public override void EnterState()
    {
        Player.Rb.gravityScale *= Player.Data.fallModifier;
        _fallSpeed = -Player.Data.maxVerticalSpeed;
        Player.Anim.SetBool("isFalling", true);
    }

    public override void ExitState()
    {
        Player.Rb.gravityScale /= Player.Data.fallModifier;
        Player.Anim.SetBool("isFalling", false);
    }

    public override void FixedUpdate()
    {
        Player.Rb.velocity = new Vector2(Player.Rb.velocity.x, Mathf.Max(Player.Rb.velocity.y, _fallSpeed));
    }

    public override void Update()
    {
        _jumpBufferCounter -= Time.deltaTime;
        if (_jumpBufferCounter > 0 && Player.OnGround())
        {
            _jumpBufferCounter = 0;
            StateMachine.ChangeState(StateMachine.PreviousState);
        }
        else if (Player.OnGround())
        {
            StateMachine.ChangeState(Player.SecondaryIdleState);
        }

        // if (Input.GetKey(KeyCode.S)) _fallSpeed = Player.Data.maxVerticalSpeed * -2f;
        // if (Input.GetKeyUp(KeyCode.S)) _fallSpeed = -Player.Data.maxVerticalSpeed;
        //
        // if (Input.GetKey(KeyCode.W)) _fallSpeed = Player.Data.maxVerticalSpeed * -0.5f;
        // if (Input.GetKeyUp(KeyCode.W)) _fallSpeed = -Player.Data.maxVerticalSpeed;

    }

    private void HandleJump()
    {
        if (StateMachine.CurrentState == this)
        {
            _jumpBufferCounter = Player.Data.jumpBuffer;
        }
    }
    
    public void OnDestroy()
    {
        Player.input.JumpEvent -= HandleJump;
    }
}
