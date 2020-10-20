using System;
using System.Collections;
using System.Collections.Generic;
using App.AI;
using App.Character.UserControl;
using Game.Character.UserControl.States;
using UnityEngine;

namespace App.Character
{
    public class WorkingUserCharacterState : GameCharacterState<WorkPlace>
    {
        private static Dictionary<Type, BaseMotorController> controllers = new Dictionary<Type, BaseMotorController>
        {
        };
        
        private WorkPlace workPlace;

        public override void SetData(WorkPlace data)
        {
            workPlace = data;
            
        }

        public override void Start()
        {
            GameManager.current.StartCoroutine(Coroutine());
        }

        private IEnumerator Coroutine()
        {
            // workPlace.Occupy(character);
            yield return Cutscene.TransitionCutscene(character.agent, workPlace.entryScene, workPlace.characterMotor.animator);
            character.agent.transform.SetParent(workPlace.transform, true);
            character.agent.SetMotor(workPlace.characterMotor);
            // character.agent.SetMotor(workPlace.characterMotor);
            yield return new WaitUntil(()=> Input.GetButtonDown("Action"));
            
            yield return Cutscene.TransitionCutscene(character.agent, workPlace.exitScene, character.agent.defaultMotor.animator);
            character.agent.transform.SetParent(null, true);
            workPlace.Release();
            stateMachine.RequireState<MainUserCharacterState>();
            
            yield break;
        }

        public override void Update()
        {
        }

        public override void Stop()
        {
            
        }
    }
}