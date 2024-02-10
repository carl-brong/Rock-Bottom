using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    #region State Machine Variables

    public StateMachine<PlayerPrimaryMovementState> PrimaryMovementStateMachine { get; set; }
    public StateMachine<PlayerSecondaryMovementState> SecondaryMovementStateMachine { get; set; }
    public StateMachine<PlayerActionState> ActionStateMachine { get; set; }
    public PlayerPrimaryIdleState PrimaryIdleState { get; set; }
    public PlayerSecondaryIdleState SecondaryIdleState { get; set; }
    public PlayerMoveState MoveState { get; set; }
    public PlayerListenState ListenState { get; set; }
    public PlayerJumpState JumpState { get; set; }
    public PlayerFallState FallState { get; set; }
    public PlayerAttackState AttackState { get; set; }
    public PlayerCrouchState CrouchState { get; set; }


    #endregion

    #region GameObject Components

    public Rigidbody2D Rb { get; set; }
    public BoxCollider2D Bc { get; set; }
    private Transform GroundCheck { get; set; }

    #endregion

    #region Primary Movement Variables

    [Header("Primary Movement Variables")]
    public float maxHorizontalSpeed;
    public float maxVerticalSpeed;
    public float accelerationTime;
    public float airAccelerationTime;
    [HideInInspector] public float accelerationForce;
    [HideInInspector] public float airAccelerationForce;
    public float deccelerationTIme;
    public float airDeccelerationTIme;
    [HideInInspector] public float deccelerationForce;
    [HideInInspector] public float airDeccelerationForce;

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

    [HideInInspector] public int _groundLayer = 1 << 6;

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

        #endregion


        accelerationForce = maxHorizontalSpeed / accelerationTime;
        airAccelerationForce = maxHorizontalSpeed / airAccelerationTime;
        deccelerationForce = maxHorizontalSpeed / deccelerationTIme;
        airDeccelerationForce = maxHorizontalSpeed / airDeccelerationTIme;
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
        deccelerationForce = maxHorizontalSpeed / deccelerationTIme;
        airDeccelerationForce = maxHorizontalSpeed / airDeccelerationTIme;
    }

    #endregion

    public bool OnGround()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, _groundLayer);
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
