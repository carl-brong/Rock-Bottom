using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerFallState : PlayerSecondaryMovementState
{
    private float _jumpBufferCounter;
    private float _fallSpeed;

    public PlayerFallState(Player player, StateMachine<PlayerSecondaryMovementState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        Player.Rb.gravityScale *= Player.fallModifier;
        _fallSpeed = -Player.maxVerticalSpeed;
        Player.Anim.SetBool("isFalling", true);
    }

    public override void ExitState()
    {
        Player.Rb.gravityScale /= Player.fallModifier;
        Player.Anim.SetBool("isFalling", false);
    }

    public override void FixedUpdate()
    {
        Player.Rb.velocity = new Vector2(Player.Rb.velocity.x, Mathf.Max(Player.Rb.velocity.y, _fallSpeed));
    }

    public override void Update()
    {
        _jumpBufferCounter -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) _jumpBufferCounter = Player.jumpBuffer;
        if (_jumpBufferCounter > 0 && Player.OnGround())
        {
            StateMachine.ChangeState(StateMachine.PreviousState);
            _jumpBufferCounter = 0;
        }
        else if (Player.OnGround()) StateMachine.ChangeState(Player.SecondaryIdleState);

        if (Input.GetKey(KeyCode.S)) _fallSpeed = Player.maxVerticalSpeed * -2f;
        if (Input.GetKeyUp(KeyCode.S)) _fallSpeed = -Player.maxVerticalSpeed;

        if (Input.GetKey(KeyCode.W)) _fallSpeed = Player.maxVerticalSpeed * -0.5f;
        if (Input.GetKeyUp(KeyCode.W)) _fallSpeed = -Player.maxVerticalSpeed;

    }
}
