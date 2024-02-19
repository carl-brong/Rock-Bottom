using System.Collections;
using UnityEngine;



public class PlayerSecondaryIdleState : PlayerSecondaryMovementState
{
    private float _coyoteCounter;

    public PlayerSecondaryIdleState(Player player, StateMachine<PlayerSecondaryMovementState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Secondary Idle");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        _coyoteCounter -= Time.deltaTime;
        if (Player.OnGround()) _coyoteCounter = Player.coyoteTime;
        if (Input.GetKeyDown(KeyCode.Space) && _coyoteCounter > 0) StateMachine.ChangeState(Player.JumpState);
        if (Input.GetKey(KeyCode.S) && Player.OnGround()) StateMachine.ChangeState(Player.CrouchState);
        if (!Player.OnGround() && Player.Rb.velocity.y < Mathf.Epsilon) StateMachine.ChangeState(Player.FallState);
    }
}
