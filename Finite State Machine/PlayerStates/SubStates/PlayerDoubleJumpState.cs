using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    private bool canDoubleJump;
    private int xInput;
    public PlayerDoubleJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        canDoubleJump = playerData.canDoubleJump;
    }
    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.SetVelocityY(0.75f *playerData.jumpVelocity);
        CancelDoubleJump();
        Debug.Log("DoubleJumpState");
        player.InAirState.SetIsJumping();    
    }
    

    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler._moveInputNormalX;
        player.SetVelocityX(playerData.movementVelocity * xInput * .75f);
    }

    public bool CanDoubleJump()
    {
        if(canDoubleJump)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public void CancelDoubleJump() => canDoubleJump = false;
    public override void AnimationFinishTrigger() => isAbilityDone = true;
    public void ResetDoubleJump() => canDoubleJump = playerData.canDoubleJump;
}
