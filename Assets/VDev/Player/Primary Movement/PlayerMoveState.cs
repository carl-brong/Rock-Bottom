using System.Collections;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class PlayerMoveState : PlayerPrimaryMovementState
{
    private bool _isFacingRight = true;
    private Vector2 _vel;
    private float _moveX;
    
    public PlayerMoveState(Player player, StateMachine<PlayerPrimaryMovementState> stateMachine) : base(player, stateMachine)
    {
        Player.input.MoveEvent += HandleInput;
    }

    public override void EnterState()
    {
        Player.Anim.SetBool("isMoving", true);
        _vel = Player.Rb.velocity;
    }

    public override void ExitState()
    {
        Player.Anim.SetBool("isMoving", false);
    }

    public override void FixedUpdate()
    {
        Player.Rb.velocity = _vel;
    }

    public override void Update()
    {
        _vel = CalculateVelocity();
        Flip(_moveX);
        if (NotMoving())
        {
            Player.Rb.velocity = new Vector2(0, Player.Rb.velocity.y);
            StateMachine.ChangeState(Player.PrimaryIdleState);
        }


    }

    private void Flip(float moveX)
    {
        if (_isFacingRight && moveX < 0)
        {
            Player.transform.localScale = new Vector3(-1, 1, 1);
            _isFacingRight = !_isFacingRight;
        }

        if (!_isFacingRight && moveX > 0)
        {
            Player.transform.localScale = new Vector3(1, 1, 1);
            _isFacingRight = !_isFacingRight;
        }
    }

    private void HandleInput(float moveX)
    {
        _moveX = moveX;
    }

    private Vector2 CalculateVelocity()
    {
        var target = _moveX * Player.Data.maxHorizontalSpeed;
        var accel = Player.OnGround() ? Player.Data.accelerationForce : Player.Data.airAccelerationForce;
        var decel = Player.OnGround() ? 999 : Player.Data.airDecelerationForce;
        float clampedForce;
        var velocity = Player.Rb.velocity;

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            velocity = velocity * 2;
                }
        switch (_moveX)
        {
            case > 0:
                clampedForce = Mathf.Min(velocity.x + accel * Time.deltaTime, target);
                clampedForce = clampedForce < 0 && Player.OnGround() ? -clampedForce : clampedForce;
                velocity = new Vector2(clampedForce, velocity.y);
                break;
            case < 0:
                clampedForce = Mathf.Max(velocity.x - accel * Time.deltaTime, target);
                clampedForce = clampedForce > 0 && Player.OnGround() ? -clampedForce : clampedForce;
                velocity = new Vector2(clampedForce, velocity.y);
                break;
            default:
                clampedForce = velocity.x < 0
                    ? Mathf.Min(velocity.x + decel * Time.deltaTime, 0)
                    : Mathf.Max(velocity.x - decel * Time.deltaTime, 0);
                velocity = new Vector2(clampedForce, velocity.y);
                break;
        }

        return velocity;
    }

    private bool NotMoving()
    {
        return Mathf.Abs(Player.Rb.velocity.x) < 0.01f && _moveX == 0;
    }

    public void OnDestroy()
    {
        Player.input.MoveEvent -= HandleInput;
    }
}
