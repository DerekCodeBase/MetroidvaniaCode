using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerAbilityState
{
    public int hitDirection;
    public PlayerHurtState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }


    public override void Enter()
    {
        base.Enter();
        player.SetHurtFalse();
    }



    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(player.knockBackTaken < playerData.poise)
        {
            player.SetVelocityX(0f);
        }
        else if(player.knockBackTaken > playerData.poise)
        {
            player.SetVelocityX(5f * player.knockBackTakenDirection);
        }

    }
}
