using System.Collections;
using Game.Actors.Character.Interactions;
using Game.Actors.Character.Statemachine;

namespace Game.Actors.Character.States
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
            yield return Cutscene.TransitionCutscene(character.actor, api.WorkPlace.entryScene,
                api.WorkPlace.characterMotor.animator);
            character.actor.transform.SetParent(api.WorkPlace.transform, true);
            character.actor.SetMotor(api.WorkPlace.characterMotor);
            api.Ready = true;
        }


        private IEnumerator ExitCoroutine()
        {
            api.Ready = false;
            yield return Cutscene.TransitionCutscene(character.actor, api.WorkPlace.exitScene,
                character.actor.defaultMotor.animator);
            character.actor.transform.SetParent(null, true);
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