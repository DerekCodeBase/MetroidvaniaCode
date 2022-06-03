using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerGroundedState
{
    public PlayerInteractState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.GeneralControlsDisable();
        player.InputHandler.MenuControlsEnable();

    }

    public override void Exit()
    {
        base.Exit();
        player.InputHandler.MenuControlsDisable();
        player.InputHandler.GeneralControlsEnable();
    }

    public override void LogicUpdate()
    {
        if(player.InputHandler.DialogueOver)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

}
