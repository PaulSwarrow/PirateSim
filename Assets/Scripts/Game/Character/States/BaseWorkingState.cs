using System.Collections;
using App.AI;
using App.Character.UserControl;
using UnityEngine;

namespace App.Character
{
    public abstract class BaseWorkingState<T> : GameCharacterState<T>
    {
        protected abstract WorkPlace workPlace {get;}
        private bool entered;
        private bool exit;

        

        public override void Start()
        {
            GameManager.current.StartCoroutine(EnterCoroutine());
        }

        protected void Exit()
        {
            exit = true;
        }

        private IEnumerator EnterCoroutine()
        {
            yield return Cutscene.TransitionCutscene(character.agent, workPlace.entryScene,
                workPlace.characterMotor.animator);
            character.agent.transform.SetParent(workPlace.transform, true);
            character.agent.SetMotor(workPlace.characterMotor);
            entered = true;
            OnEntered();
        }


        private IEnumerator ExitCoroutine()
        {
            entered = false;
            yield return Cutscene.TransitionCutscene(character.agent, workPlace.exitScene,
                character.agent.defaultMotor.animator);
            character.agent.transform.SetParent(null, true);
            workPlace.Release();
            OnExit();
        }

        protected abstract void OnEntered();
        protected abstract void OnWorking();
        protected abstract void OnExit();

        public override void Update()
        {
            if (exit)
            {
                GameManager.current.StartCoroutine(ExitCoroutine());
            }
            else if (entered)
            {
                OnWorking();
            }
        }


        public override void Stop()
        {
        }
    }
}