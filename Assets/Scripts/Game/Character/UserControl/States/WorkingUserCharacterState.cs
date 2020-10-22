using System;
using System.Collections;
using System.Collections.Generic;
using App.AI;
using App.Character.UserControl;
using Game.Character.UserControl.States;
using UnityEngine;

namespace App.Character
{
    public class WorkingUserCharacterState : BaseWorkingState<WorkPlace>
    {
        protected override WorkPlace workPlace => data;

        protected override void OnEntered()
        {
            
        }

        protected override void OnWorking()
        {
            if (Input.GetButtonDown("Action"))
            {
                Exit();
            }
        }

        protected override void OnExit()
        {
            stateMachine.RequireState<MainUserCharacterState>();
        }
    }
}