using System;
using System.Collections;
using System.Collections.Generic;
using Game.Actors.Character;
using Game.Actors.Character.Interactions;
using Game.Interfaces;

namespace Game.Systems
{
    public class WorkPlacesSystem : IGameSystem
    {
        public void Init()
        {
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        //abort is not supported
        private HashSet<GameCharacterActor> tasks = new HashSet<GameCharacterActor>();

        public void EnterWorkPlace(GameCharacterActor actor, WorkPlace workPlace, Action callback)
        {
            if (tasks.Contains(actor))
            {
                //TODO handle queue
                throw new Exception("Already in transition");
                return;
            }

            //TODO coroutine manager?
            GameManager.current.StartCoroutine(Coroutine(actor, Cutscene.EnterWorkPlace(actor, workPlace), callback));
        }

        //TODO better api
        public void ExitWorkPlace(GameCharacterActor actor, Action callback)
        {
            if (tasks.Contains(actor))
            {
                //TODO handle queue
                throw new Exception("Already in transition");
                return;
            }

            //TODO coroutine manager?
            GameManager.current.StartCoroutine(Coroutine(actor, Cutscene.ExitWorkPlace(actor), callback));
        }

        private IEnumerator Coroutine(GameCharacterActor actor, IEnumerator enterWorkPlace, Action callback)
        {
            tasks.Add(actor);
            yield return enterWorkPlace;
            tasks.Remove(actor);
            callback?.Invoke();
        }
    }
}