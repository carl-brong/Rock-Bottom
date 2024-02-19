using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerSecondaryMovementState
{
    private bool _jumpReleased;
    
    public PlayerJumpState(Player player, StateMachine<PlayerSecondaryMovementState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Jump");
        _jumpReleased = false;
        Player.Rb.velocity = new Vector2(Player.Rb.velocity.x, Player.jumpModifier);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_jumpReleased)
        {
            Player.Rb.velocity = new Vector2(Player.Rb.velocity.x, Player.Rb.velocity.y * 0.5f);
            _jumpReleased = false;
        }
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Space)) _jumpReleased = true; 
        if (Player.Rb.velocity.y < Mathf.Epsilon) StateMachine.ChangeState(Player.FallState);
    }
}
