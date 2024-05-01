using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    private bool isWallSliding;
    private float wallFallSpeed = 6f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] LayerMask wallLayer;
    private PlayerData sprintButton;


    #region State Machine Variables

    public StateMachine<PlayerPrimaryMovementState> PrimaryMovementStateMachine { get; private set; }
    public StateMachine<PlayerSecondaryMovementState> SecondaryMovementStateMachine { get; private set; }
    public PlayerPrimaryIdleState PrimaryIdleState { get; private set; }
    public PlayerSecondaryIdleState SecondaryIdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerCrouchState CrouchState { get; private set; }
    
    #endregion

    #region GameObject Components   

    public Rigidbody2D Rb { get; private set; }
    public BoxCollider2D Bc { get; private set; }
    [SerializeField] private Transform GroundCheck1;
    //[SerializeField] private Transform GroundCheck2;
    public InputReader input;
    public Animator Anim { get; private set; }
    private AudioSource jumpSound;

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
    public Vector3 startpos;
    [SerializeField] private GameStateSO _gameStateManager;
    public Canvas pauseMenu;

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

        #endregion

        #region GameObject References

        Rb = GetComponent<Rigidbody2D>();
        Bc = GetComponent<BoxCollider2D>();
        Anim = GetComponent<Animator>();
        jumpSound = GetComponent<AudioSource>();


        #endregion

        input.PauseEvent += EnablePause;


    }

    #region Updaters

    // Start is called before the first frame update
    private void Start()
    {
        PrimaryMovementStateMachine.Initialize(PrimaryIdleState);
        SecondaryMovementStateMachine.Initialize(SecondaryIdleState);
        CurrentHealth = MaxHealth;
        TempProj.PlayerHit += LoseHealth;
        Spikes.HitSpike += LoseHealth;

        startpos = transform.position;
    }

    private void OnDestroy()
    {
        TempProj.PlayerHit -= LoseHealth;
        Spikes.HitSpike -= LoseHealth;
        input.PauseEvent -= EnablePause;
    }

    // Update is called once per frame
    private void Update()
    {
        Shader.SetGlobalVector("_Player", transform.position);
        PrimaryMovementStateMachine.CurrentState.Update();
        SecondaryMovementStateMachine.CurrentState.Update();
        
        Anim.SetBool("isGrounded", OnGround());
        //Carl Brong Start
        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
        {
            jumpSound.Play();
        }
        if (OnWall())
        {
            WallSlide();
        }
        //Carl Brong End
    }

    private void FixedUpdate()
    {
        PrimaryMovementStateMachine.CurrentState.FixedUpdate();
        SecondaryMovementStateMachine.CurrentState.FixedUpdate();
    }

    #endregion

    public bool OnGround()
    {
        
        return Physics2D.OverlapCircle(GroundCheck1.position, 0.15f, _groundLayer) ||
               Physics2D.OverlapCircle(wallCheck.position, 0.15f, wallLayer);
    }

    public bool OnWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.15f, wallLayer);
    }
    //Carl Brong Start
    private void WallSlide()
    {
        if(OnGround()) 
        {
            isWallSliding = true;
            Rb.velocity = new Vector2(Rb.velocity.x, Mathf.Clamp(Rb.velocity.y, -wallFallSpeed, float.MaxValue));
            //OnGround();
            Debug.Log(OnGround());
        }
        else
        {
            isWallSliding = false;
        }
    }
    //Carl Brong End
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.GetChild(0).position, 0.15f);
    }

    private void EnablePause()
    {
        pauseMenu.gameObject.SetActive(true);
    }
    
    #region Health Functions

    public void LoseHealth(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            Anim.SetFloat("CurrentHealth", CurrentHealth);
            PlayerHealthChangeEvent?.Invoke(CurrentHealth);
            Die();
        }
        else
        {
            Anim.SetFloat("CurrentHealth", CurrentHealth);
            Anim.SetTrigger("Hurt");
            PlayerHealthChangeEvent?.Invoke(CurrentHealth);
        }
    }

    public void HealHealth(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        Anim.SetFloat("CurrentHealth", CurrentHealth);
        PlayerHealthChangeEvent?.Invoke(CurrentHealth);
    }

    public void Die()
    {
        _gameStateManager.UpdateGameState(GameState.GameOver);
       
        
    }

    #endregion
    
}
