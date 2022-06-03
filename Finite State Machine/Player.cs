using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDataPersistence
{
#region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerLongIdleState LongIdleState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDoubleJumpState DoubleJumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerInAirAttackState InAirAttackState{ get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set;}
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLightAttackState LightAttackState { get; private set; }
    public PlayerHeavyAttackState HeavyAttackState { get; private set; }
    public PlayerUpwardsAttackState UpwardsAttackState { get; private set; }
    public PlayerHurtState HurtState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }
    public PlayerInteractState InteractState { get; private set; }

    [SerializeField]
    public PlayerData playerData;
    public bool canInteract = false;

#endregion

#region WorldObjects
public GeneralUIOverlayHandler generalUIOverlayHandler;
public AudioManager AudioManager;

#endregion

#region Components
    public Animator Anim { get; private set; }
    public enableDisable InputHandler { get; set; }
    public Rigidbody2D _rbody { get; set; }
    public Collider2D PlayerCollider;
    public SpriteRenderer _spriteRenderer;
    public Transform attackPoint;
    public Transform attackPointLeft;
#endregion

#region Check Transforms

[SerializeField]
private Transform groundCheck;
[SerializeField]
private Transform wallCheck;

#endregion

#region Attack Variables
    private float lightAttackStartTime;
    private Collider2D[] attackedColliders;
    private Vector2 attackRight;
    private Vector2 attackLeft;
#endregion

#region Health/Defense Variables

public int maxHealth { get; private set; }
public int currentHealth { get; private set; }
public float knockBackTaken { get; private set; }
public float knockBackTakenDirection { get; private set; }
public int damageTaken { get; private set; }
public bool hurt { get; private set; }
public bool Dead { get; private set; }
public bool Invincible { get; private set; }

#endregion

#region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection; 

    private Vector2 workspace;

#endregion

#region Unity Callback Functions
    private void Awake()
    {
        if(playerData == null)
        {
            playerData = new PlayerData();
        }
        StateMachine = new PlayerStateMachine();
        InputHandler = GetComponent<enableDisable>();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        LongIdleState = new PlayerLongIdleState(this, StateMachine, playerData, "longIdle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "run");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        LightAttackState = new PlayerLightAttackState(this, StateMachine, playerData, "attack");
        HeavyAttackState = new PlayerHeavyAttackState(this, StateMachine, playerData, "heavyAttack");
        HurtState = new PlayerHurtState(this, StateMachine, playerData, "hurt");
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "death");
        InteractState = new PlayerInteractState(this, StateMachine, playerData, "idle");
        DoubleJumpState = new PlayerDoubleJumpState(this, StateMachine, playerData, "doubleJump");
        InAirAttackState = new PlayerInAirAttackState(this, StateMachine, playerData, "airAttack");
        UpwardsAttackState = new PlayerUpwardsAttackState(this, StateMachine, playerData, "airAttack");

        _rbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

}

    private void Start()
    {
        Anim = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);
        InputHandler = GetComponent<enableDisable>();
        FacingDirection = 1;
        generalUIOverlayHandler.PassHealth();
    }

    private void Update()
    {  
        if(currentHealth <= 0f)
        {
            Dead = true;
        }
        CurrentVelocity = _rbody.velocity;
        StateMachine.CurrentState.LogicUpdate();
        Debug.Log(StateMachine.CurrentState);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

#region Set Functions

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        _rbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        _rbody.velocity = workspace;
        CurrentVelocity = workspace;

    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        _rbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        _rbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

#endregion

#region Check Functions
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
    }


    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion

#region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
       _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
    public void ChangeToInteractState()
    {
        if(CheckIfGrounded())
        {
            StateMachine.ChangeState(InteractState);
        }
    }
    public void AttackCooldown(float startTime)
    {
        lightAttackStartTime = startTime;
        StartCoroutine(ResetAttack());
    }
    public IEnumerator ResetAttack()
    {
        yield return new WaitUntil(() => Time.time >= lightAttackStartTime + playerData.attackDisableTime);
        LightAttackState.AttackCooldown();

    }
#endregion

#region Attack Functions

public void BasicAttackTrigger()
{
    if(FacingDirection == 1)
    {
        attackRight = new Vector2(attackPoint.position.x, attackPoint.position.y);
        attackedColliders =  Physics2D.OverlapCircleAll(attackRight, playerData.attackRadius, playerData.attackLayerMask);
        if(attackedColliders != null)
        {
            foreach(Collider2D collider in attackedColliders)
            {
                collider.gameObject.GetComponent<HitPasser>().PassHit(0);
            }
        }
    }
    else
    {
        attackLeft = new Vector2(attackPointLeft.position.x, attackPointLeft.position.y);
        attackedColliders =  Physics2D.OverlapCircleAll(attackLeft, playerData.attackRadius, playerData.attackLayerMask);
        foreach(Collider2D collider in attackedColliders)
        {
            collider.GetComponent<HitPasser>().PassHit(0);
        }
    }
}

public void AirAttackTrigger()
{
    if(FacingDirection == 1)
    {
        attackRight = new Vector2(attackPoint.position.x, attackPoint.position.y);
        attackedColliders =  Physics2D.OverlapCircleAll(attackRight, playerData.attackRadius, playerData.attackLayerMask);
        foreach(Collider2D collider in attackedColliders)
        {
            collider.GetComponent<HitPasser>().PassHit(0);
        }
    }
    else
    {
        attackLeft = new Vector2(attackPointLeft.position.x, attackPointLeft.position.y);
        attackedColliders =  Physics2D.OverlapCircleAll(attackLeft, playerData.attackRadius, playerData.attackLayerMask);
        foreach(Collider2D collider in attackedColliders)
        {
            collider.GetComponent<HitPasser>().PassHit(0);
        }
    }
}

public void UpwardsAttackTrigger()
{
    if(FacingDirection == 1)
    {
        attackRight = new Vector2(attackPoint.position.x, attackPoint.position.y);
        attackedColliders =  Physics2D.OverlapCircleAll(attackRight, playerData.attackRadius, playerData.attackLayerMask);
        foreach(Collider2D collider in attackedColliders)
        {
            collider.GetComponent<HitPasser>().PassHit(0);
        }
    }
    else
    {
        attackLeft = new Vector2(attackPointLeft.position.x, attackPointLeft.position.y);
        attackedColliders =  Physics2D.OverlapCircleAll(attackLeft, playerData.attackRadius, playerData.attackLayerMask);
        foreach(Collider2D collider in attackedColliders)
        {
            collider.GetComponent<HitPasser>().PassHit(0);
        }
    }
}
#endregion

#region Save Functions and Variables

private bool dataChange;

public void LoadData(GameData data)
{    
    this.maxHealth = data.maxHealth;
    this.currentHealth = data.currentHealth;
}

public void SaveData(GameData data)
{
    data.maxHealth = this.maxHealth;
    data.currentHealth = this.currentHealth;

}




#endregion

public void PassHit(int damage, float knockBack, float direction)
{
    if(!Invincible)
    {
    StartCoroutine(IFrames());
    knockBackTaken = knockBack;
    knockBackTakenDirection = direction;
    damageTaken = damage;
    currentHealth -= damage;
    DataPersistenceManager.instance.SaveCharacterStats(this);
    generalUIOverlayHandler.PassHealth();
    hurt = true;
    }
}
public void RefillHealth(bool completely)
{
    if(completely)
    {
        currentHealth = maxHealth;
    }
    else
    {
        currentHealth += 1;
    }
    DataPersistenceManager.instance.SaveCharacterStats(this);
}
public void SetHurtFalse()
{
    hurt = false;
}
public IEnumerator IFrames()
{
    Invincible = true;
    Physics2D.IgnoreLayerCollision( 8, 7, true);
    yield return new WaitForSeconds(playerData.invincibilityTime);
    Invincible = false;
    Physics2D.IgnoreLayerCollision(8, 7, false);
}

public void IdleFriction()
{
    PlayerCollider.sharedMaterial.friction = 1f;
}

public void OutOfIdleFriction()
{
    PlayerCollider.sharedMaterial.friction = 0f;
}

}
