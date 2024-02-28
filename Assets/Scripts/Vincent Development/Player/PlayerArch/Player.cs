using System;
using UnityEngine;

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
    public InputReader input;
    public Animator Anim { get; private set; }
    

    #endregion
    
    #region Health Variables

    public float MaxHealth { get; set; } = 20f;
    public float CurrentHealth { get; set; }
    public static event Action<float> PlayerHealthChangeEvent;

    #endregion
    
    #region Layers

    [SerializeField] public LayerMask _groundLayer;
    [SerializeField] private LayerMask _attackLayer;

    #endregion

    public PlayerData Data;

    private AudioSource jump;
    private AudioSource land;
    //[SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip landClip;

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
        Anim = GetComponent<Animator>();

        #endregion
        
    }

    #region Updaters

    // Start is called before the first frame update
    private void Start()
    {
        PrimaryMovementStateMachine.Initialize(PrimaryIdleState);
        SecondaryMovementStateMachine.Initialize(SecondaryIdleState);
        ActionStateMachine.Initialize(ListenState);
        CurrentHealth = MaxHealth;
        LoseHealth(2);
        TempProj.PlayerHit += LoseHealth;
        Spikes.HitSpike += LoseHealth;
        jump = GetComponent<AudioSource>();
        land = GetComponent<AudioSource>();
        landClip = GetComponent<AudioClip>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        PrimaryMovementStateMachine.CurrentState.Update();
        SecondaryMovementStateMachine.CurrentState.Update();
        ActionStateMachine.CurrentState.Update();
        
        Anim.SetBool("isGrounded", OnGround());
        if(Input.GetKeyDown(KeyCode.Space) && OnGround()) {
        jump.Play();
        }
       
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
            if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Floor") && OnGround())
        {
            land.PlayOneShot(landClip);
        }
    }*/

    private void FixedUpdate()
    {
        PrimaryMovementStateMachine.CurrentState.FixedUpdate();
        SecondaryMovementStateMachine.CurrentState.FixedUpdate();
        ActionStateMachine.CurrentState.FixedUpdate();
    }

    #endregion

    public bool OnGround()
    {
         
        return Physics2D.OverlapCircle(GroundCheck.position, 0.15f, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.GetChild(0).position, 0.15f);
    }

    #region Health Functions

    public void LoseHealth(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0)
        {
            Anim.SetTrigger("Die");
            PlayerHealthChangeEvent?.Invoke(CurrentHealth);
            Die();
        }
        else
        {
            Anim.SetTrigger("Hurt");
            PlayerHealthChangeEvent?.Invoke(CurrentHealth);
        }
    }

    public void HealHealth(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        PlayerHealthChangeEvent?.Invoke(CurrentHealth);
    }

    public void Die()
    {
        // Implement Later
    }

    #endregion

    public void EnemyInRange()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.GetChild(5).position, 0.2f);
        foreach (Collider2D enemy in colliders)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                Debug.Log($"Hit {enemy.gameObject.name}");
            }
        }
    }
}
