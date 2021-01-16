using System.Collections;
using App.AI;
using App.Character.UserControl;
using Game.Character.Statemachine;
using UnityEngine;

namespace App.Character
{
    public class WorkingState : GameCharacterState<WorkingState.Api>, IStateWithData<WorkPlace>
    {
        //API:
        public class Api : BaseStateApi
        {
            public WorkPlace WorkPlace { get; internal set; }
            public bool Entered { get; internal set; }
            public bool Ready { get; internal set; }
        }



        public void SetData(WorkPlace data)
        {
            api.WorkPlace = data;
        }

        public override void Start()
        {
            GameManager.current.StartCoroutine(EnterCoroutine());
        }

        private IEnumerator EnterCoroutine()
        {
            yield return Cutscene.TransitionCutscene(character.agent, api.WorkPlace.entryScene,
                api.WorkPlace.characterMotor.animator);
            character.agent.transform.SetParent(api.WorkPlace.transform, true);
            character.agent.SetMotor(api.WorkPlace.characterMotor);
            api.Ready = true;
        }


        private IEnumerator ExitCoroutine()
        {
            api.Ready = false;
            yield return Cutscene.TransitionCutscene(character.agent, api.WorkPlace.exitScene,
                character.agent.defaultMotor.animator);
            character.agent.transform.SetParent(null, true);
            api.WorkPlace.Release();
            // stateMachine.RequireState<MainState>();//To the main state
        }

        public override void Update()
        {
            if (api.Ready && api.Exit)
            {
                GameManager.current.StartCoroutine(ExitCoroutine());
            }
        }

        public override void Stop()
        {
            
        }
    }
}