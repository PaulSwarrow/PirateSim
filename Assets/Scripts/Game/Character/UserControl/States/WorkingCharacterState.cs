using System;
using System.Collections;
using System.Collections.Generic;
using App.AI;
using Game.Character.UserControl.States;

namespace App.Character
{
    public class WorkingCharacterState : GameCharacterState
    {
        private static Dictionary<Type, BaseMotorController> controllers = new Dictionary<Type, BaseMotorController>
        {
        };
        
        private WorkPlace workPlace;

        public WorkingCharacterState(WorkPlace workPlace)
        {
            this.workPlace = workPlace;
        }
        public override void Start()
        {
            GameManager.current.StartCoroutine(Coroutine());
        }

        private IEnumerator Coroutine()
        {
            // workPlace.Occupy(character);
            yield return Cutscene.TransitionCutscene(character.agent, workPlace.entryScene, workPlace.characterMotor);
            // character.agent.SetMotor(workPlace.characterMotor);
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