using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IDamageable
{
    #region State Machine Variables

    public StateMachine<PlayerPrimaryMovementState> PrimaryMovementStateMachine { get; private set; }
    public StateMachine<PlayerSecondaryMovementState> SecondaryMovementStateMachine { get; private set; }
    public StateMachine<PlayerActionState> ActionStateMachine { get; private set; }
    public PlayerPrimaryIdleState PrimaryIdleState { get; private set; }
    public PlayerSecondaryIdleState SecondaryIdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerListenState ListenState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerCrouchState CrouchState { get; private set; }
    
    #endregion

    #region GameObject Components

    public Rigidbody2D Rb { get; private set; }
    public BoxCollider2D Bc { get; private set; }
    public Transform GroundCheck { get; set; }
    public PlayerInput Controls { get; private set; }
    public Animator Anim { get; private set; }

    #endregion

    #region Primary Movement Variables

    [Header("Primary Movement Variables")]
    public float maxHorizontalSpeed;
    public float maxVerticalSpeed;
    public float accelerationTime;
    public float airAccelerationTime;
    [HideInInspector] public float accelerationForce;
    [HideInInspector] public float airAccelerationForce;
    public float decelerationTIme;
    public float airDecelerationTIme;
    [HideInInspector] public float decelerationForce;
    [HideInInspector] public float airDecelerationForce;

    #endregion

    #region Secondary Movement Variables

    [Header("Secondary Movement Variables")]
    public float jumpModifier;
    public float jumpBuffer = 0.2f;
    public float coyoteTime = 0.2f;
    public float fallModifier;

    #endregion

    #region Health Variables
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    #endregion
    
    #region Layers

    [SerializeField] public LayerMask _groundLayer;
    [SerializeField] private LayerMask _attackLayer;

    #endregion

    private void Awake()
    {
        #region State Machine Instances

        // Primary Movement States
        PrimaryMovementStateMachine = new StateMachine<PlayerPrimaryMovementState>();
        PrimaryIdleState = new PlayerPrimaryIdleState(this, PrimaryMovementStateMachine);
        MoveState = new PlayerMoveState(this, PrimaryMovementStateMachine);

        // Secondary Movement States
        SecondaryMovementStateMachine = new StateMachine<PlayerSecondaryMovementState>();
        SecondaryIdleState = new PlayerSecondaryIdleState(this, SecondaryMovementStateMachine);
        CrouchState = new PlayerCrouchState(this, SecondaryMovementStateMachine);
        JumpState = new PlayerJumpState(this, SecondaryMovementStateMachine);
        FallState = new PlayerFallState(this, SecondaryMovementStateMachine);

        // Action States
        ActionStateMachine = new StateMachine<PlayerActionState>();
        ListenState = new PlayerListenState(this, ActionStateMachine);
        AttackState = new PlayerAttackState(this, ActionStateMachine);

        #endregion

        #region GameObject References

        Rb = GetComponent<Rigidbody2D>();
        Bc = GetComponent<BoxCollider2D>();
        GroundCheck = transform.GetChild(0);
        Controls = GetComponent<PlayerInput>();
        Anim = GetComponent<Animator>();

        #endregion


        accelerationForce = maxHorizontalSpeed / accelerationTime;
        airAccelerationForce = maxHorizontalSpeed / airAccelerationTime;
        decelerationForce = maxHorizontalSpeed / decelerationTIme;
        airDecelerationForce = maxHorizontalSpeed / airDecelerationTIme;
    }

    #region Updaters

    // Start is called before the first frame update
    private void Start()
    {
        PrimaryMovementStateMachine.Initialize(PrimaryIdleState);
        SecondaryMovementStateMachine.Initialize(SecondaryIdleState);
        ActionStateMachine.Initialize(ListenState);
    }

    // Update is called once per frame
    private void Update()
    {
        PrimaryMovementStateMachine.CurrentState.Update();
        SecondaryMovementStateMachine.CurrentState.Update();
        ActionStateMachine.CurrentState.Update();
        
        Anim.SetBool("isGrounded", OnGround());
    }

    private void FixedUpdate()
    {
        PrimaryMovementStateMachine.CurrentState.FixedUpdate();
        SecondaryMovementStateMachine.CurrentState.FixedUpdate();
        ActionStateMachine.CurrentState.FixedUpdate();
    }
    private void OnValidate()
    {
        accelerationForce = maxHorizontalSpeed / accelerationTime;
        airAccelerationForce = maxHorizontalSpeed / airAccelerationTime;
        decelerationForce = maxHorizontalSpeed / decelerationTIme;
        airDecelerationForce = maxHorizontalSpeed / airDecelerationTIme;
    }

    #endregion

    public bool OnGround()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.GetChild(1).position, 0.2f);
    }

    #region Health Functions

    public void LoseHealth(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0) Die();
    }

    public void HealHealth(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
    }

    public void Die()
    {
        // Implement Later
    }

    #endregion
}
