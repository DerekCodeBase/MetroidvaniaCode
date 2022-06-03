using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash {get; private set; }
    private float lastDashTime;
    private bool isHolding;
    private bool dashInputStop;

    private Vector2 dashDirectionInput;

    private Vector2 dashDirection;


    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }    

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }
    public void ResetCanDash() => CanDash = true;


    public override void Enter()
    {
        base.Enter();
        CanDash = false;
        player.InputHandler.UseDashInput();
        isHolding = true;
        dashDirection = Vector2.right * player.FacingDirection;

        Time.timeScale = playerData.holdTimeScale;
        startTime = Time.unscaledTime;
    }

    public override void Exit()
    {
        base.Exit();

        if (player.CurrentVelocity.y > 0)
        {
            player.SetVelocityY(player.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(!isExitingState)
        {
            if(isHolding)
            {
                dashDirection = new Vector2(player.InputHandler.RawDashDirectionInput, 0);
                dashInputStop = player.InputHandler.dashInputStop;

                if(dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    startTime = Time.time;
                    player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player._rbody.drag = playerData.drag;
                    player.AudioManager.Play("Dash");
                    player.SetVelocity(playerData.dashVelocity, dashDirection);
                }
            }
            else
            {                
                player.AudioManager.Play("Dash");
                player.SetVelocity(playerData.dashVelocity, dashDirection);

                if(Time.time >= startTime + playerData.dashTime)
                {
                    player._rbody.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

}
