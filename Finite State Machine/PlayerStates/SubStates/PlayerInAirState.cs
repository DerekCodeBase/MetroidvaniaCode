using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{

    //Input
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool dashInput;

    //Checks
    private bool isGrounded;
    private bool coyoteTime;
    private bool isJumping;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool wallJumpCoyoteTime;
    private float startWallJumpCoyoteTime;
    private bool oldIsTouchingWall;
    private bool oldisTouchingWallBack;
    private bool attackInput;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldisTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();

        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldisTouchingWallBack))
        {
           StartWallJumpCoyoteTime(); 
        }
    }

    public override void Enter()
    {
        base.Enter();
    }


    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldisTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        jumpInputStop = player.InputHandler.jumpInputStop;
        CheckJumpMultiplier();
        checkCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler._moveInputNormalX;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.dashInput;
        attackInput = player.InputHandler.attackInput;



        if(isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if(jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if(jumpInput && player.DoubleJumpState.CanDoubleJump())
        {
            stateMachine.ChangeState(player.DoubleJumpState);
        }
        else if(attackInput && player.InAirAttackState.canAirAttack)
        {
            stateMachine.ChangeState(player.InAirAttackState);
        }
        else if(isTouchingWall && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if(dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput * .75f);
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
           // if(player.CurrentVelocity.y < 0.3f)
          //  {
          //      player.SetVelocityY(playerData.jumpFallSpeed);
          //  }
        }
    }


    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(0f);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void checkCoyoteTime()
    {
        if(coyoteTime && Time.time < startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreasAmountOfJumpsLeft();
        }

    }

    private void CheckWallJumpCoyoteTime()
    {
        if(wallJumpCoyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;
    public void SetIsJumping() => isJumping = true;

    public void StartWallJumpCoyoteTime() 
    { 
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;
}
