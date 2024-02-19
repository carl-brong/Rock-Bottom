using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMoveState : PlayerPrimaryMovementState
{
    private float _moveX;
    private bool _isFacingRight;
    
    public PlayerMoveState(Player player, StateMachine<PlayerPrimaryMovementState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Move");
        _moveX = 0f;
        _isFacingRight = true;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        float speed = (Player.SecondaryMovementStateMachine.CurrentState == Player.CrouchState) ? Player.maxHorizontalSpeed * 0.25f : Player.maxHorizontalSpeed;
        float targetSpeed = speed * _moveX;
        float force;

        // Conserve more momentum while moving in the air
        if (Player.OnGround()) force = Mathf.Abs(targetSpeed) > 0.01f ? Player.accelerationForce : Player.deccelerationForce;
        else force = Mathf.Abs(targetSpeed) > 0.01f ? Player.airAccelerationForce : Player.airDeccelerationForce;

        float speedDiff = targetSpeed - Player.Rb.velocity.x;

        // Reduce speeds while crouching
        if (Player.SecondaryMovementStateMachine.CurrentState == Player.CrouchState)
            Player.Rb.velocity = new Vector2(Player.Rb.velocity.x + (Time.deltaTime * force * speedDiff), Player.Rb.velocity.y);
        else Player.Rb.velocity = new Vector2(Player.Rb.velocity.x + (Time.deltaTime * force * speedDiff), Player.Rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        _moveX = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Player.Rb.velocity.x) < 0.01f && _moveX == 0) StateMachine.ChangeState(Player.PrimaryIdleState);

        Flip();
    }

    private void Flip()
    {
        if (_moveX > 0 && !_isFacingRight)
        {
            Player.transform.localScale = new Vector3(1, Player.transform.localScale.y, Player.transform.localScale.z);
            _isFacingRight = !_isFacingRight;
        }
        else if (_moveX < 0 && _isFacingRight)
        {
            Player.transform.localScale = new Vector3(-1, Player.transform.localScale.y, Player.transform.localScale.z);
            _isFacingRight = !_isFacingRight;
        }
    }
}
