using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int maxHealth { get; private set; }
   
    public float poise { get; private set; }

    public float invincibilityTime { get; private set; }

    public float movementVelocity { get; private set; }
    public float jumpVelocity { get; private set; }

    public int amountOfJumps = 1;
    public bool canDoubleJump { get; private set; }

    public float jumpTime { get; private set; }
    

    

    [Header("In Air State")]
    public float coyoteTime = 0.1f;
    public float variableJumpHeightMultiplier = 1f;
    public float jumpFallSpeed = -2f;
    public float fallVelocity = -2f;
    public float jumpAttackVelocityX = 1f;
    public float jumpAttackVelocityY = 3f;


    [Header ("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    public float attackInputHoldTime = 0.3f;
    public float attackDisableTime = 0.7f;

    [Header ("Wall State")]
    public float wallSlideVelocity = 3f;
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
    public LayerMask whatIsWall;



    [Header ("Dash State")]
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 1f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distanceBetweenAfterImages = 0.5f;
    public float dashCooldown { get; private set; }

    [Header ("Attack State")]
    public float attackRadius = 1f;
    public LayerMask attackLayerMask;

    #region Set Functions

    public void SetAllStats(int health, float poise, float iFrameTime, float velo, float jumpVelo, bool doubleJump, float hangTime, float dashCooldownTime)
    {
        setLayerMasks();
        setMaxHealth(health);
        setPoise(poise);
        setInvincibilityTime(iFrameTime);
        setMovementVelocity(velo);
        setJumpVelocity(jumpVelo);
        setCanDoubleJump(doubleJump);
        setJumpTime(hangTime);
        setDashCooldown(dashCooldownTime);
        Debug.Log("Set Player Data");
    }

    private void setLayerMasks()
    {
        attackLayerMask = LayerMask.GetMask("Enemy", "Breakables");
        whatIsWall = LayerMask.GetMask("Walls");
        whatIsGround = LayerMask.GetMask("Ground");
    }
    
    private void setMaxHealth(int passedHealth)
    {
        maxHealth = passedHealth;
    }
    private void setPoise(float passedPoise)
    {
        poise = passedPoise;
    }
    private void setInvincibilityTime(float passedInvincibilityTime)
    {
        invincibilityTime = passedInvincibilityTime;
    }
    private void setMovementVelocity(float passedVelo)
    {
        movementVelocity = passedVelo;
    }
    private void setJumpVelocity(float passedJumpVelo)
    {
        jumpVelocity = passedJumpVelo;
    }
    private void setCanDoubleJump(bool passedCanDoubleJump)
    {
        canDoubleJump = passedCanDoubleJump;
    }
    private void setJumpTime(float passedJumpTime)
    {
        jumpTime = passedJumpTime;
    }
    private void setDashCooldown(float passedCooldown)
    {
        dashCooldown = passedCooldown;
    }
    #endregion
}
