using UnityEngine;

public class PlayerMoveState : PlayerPrimaryMovementState
{
    private float _moveX;
    private bool _isFacingRight = true;
    
    public PlayerMoveState(Player player, StateMachine<PlayerPrimaryMovementState> stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        Player.Anim.SetBool("isMoving", true);
    }

    public override void ExitState()
    {
        Player.Anim.SetBool("isMoving", false);
    }

    public override void FixedUpdate()
    {
        Move();
    }

    public override void Update()
    {
        GetInput();
        Flip();
        if (!isMoving())
        {
            Player.Rb.velocity = new Vector2(0, Player.Rb.velocity.y);
            StateMachine.ChangeState(Player.PrimaryIdleState);
        }
    }

    private void Flip()
    {
        if (_isFacingRight && _moveX < 0)
        {
            Player.transform.localScale = new Vector3(-1, 1, 1);
            _isFacingRight = !_isFacingRight;
        }

        if (!_isFacingRight && _moveX > 0)
        {
            Player.transform.localScale = new Vector3(1, 1, 1);
            _isFacingRight = !_isFacingRight;
        }
    }

    private void GetInput()
    {
        _moveX = Player.Controls.actions["Move"].ReadValue<float>();
    }
    
    private void Move()
    {
        var target = _moveX * Player.Data.maxHorizontalSpeed;
        var accel = Player.OnGround() ? Player.Data.accelerationForce : Player.Data.airAccelerationForce;
        var decel = Player.OnGround() ? Player.Data.decelerationForce : Player.Data.airDecelerationForce;
        float clampedForce;
        var velocity = Player.Rb.velocity;
        switch (_moveX)
        {
            case > 0:
                clampedForce = Mathf.Min(velocity.x + accel * Time.fixedDeltaTime, target);
                velocity = new Vector2(clampedForce, velocity.y);
                break;
            case < 0:
                clampedForce = Mathf.Max(velocity.x - accel * Time.fixedDeltaTime, target);
                velocity = new Vector2(clampedForce, velocity.y);
                break;
            default:
                clampedForce = velocity.x < 0
                    ? Mathf.Min(velocity.x + decel * Time.fixedDeltaTime, 0)
                    : Mathf.Max(velocity.x - decel * Time.fixedDeltaTime, 0);
                velocity = new Vector2(clampedForce, velocity.y);
                break;
        }
        Player.Rb.velocity = velocity;
    }

    private bool isMoving()
    {
        return Mathf.Abs(Player.Rb.velocity.x) > 0.01f;
    }
}
