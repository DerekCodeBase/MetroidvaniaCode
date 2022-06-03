using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    private bool canDoubleJump;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
        canDoubleJump = playerData.canDoubleJump;
    }


    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.SetVelocityY(playerData.jumpVelocity);
        amountOfJumpsLeft--;
        isAbilityDone = true;

        player.InAirState.SetIsJumping(); 
        Debug.Log("JumpState");   
    }


    public override void Exit()
    {
        base.Exit();
    }

    public bool CanJump()
    {
        if(amountOfJumpsLeft > 0)
        {
            return true;
        }
        else{
            return false;
        }
    }


    public void ResetAmountOfJumpsLEft() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreasAmountOfJumpsLeft() => amountOfJumpsLeft --;
}
