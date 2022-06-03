using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightAttackState : PlayerAbilityState
{
    private float gravityScale;
    public bool canAttack = true;
    public float attackDisableTime;
    public PlayerLightAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        if(!player.InputHandler.attackInput)
        {
            isAbilityDone = true;
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        player.InputHandler.UseAttackInput();

    }


    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseAttackInput();
        attackDisableTime = playerData.attackDisableTime;
        canAttack = false;
        player.AttackCooldown(startTime);
        player.SetVelocity(0, Vector2.zero);
        gravityScale = player._rbody.gravityScale;
        player._rbody.gravityScale = 0;
        player.AudioManager.Play("Sword Slash");
    }


    public override void Exit()
    {
        base.Exit();
        player._rbody.gravityScale = gravityScale;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        
        player.SetVelocity(0, Vector2.zero);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void AttackCooldown() => canAttack = true;

    public void UseAttack() => canAttack = false;
}
