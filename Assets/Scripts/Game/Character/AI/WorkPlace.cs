using System;
using System.Collections;
using App.Character;
using Lib;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace App.AI
{
    [RequireComponent(typeof(PlayableDirector))]
    public class WorkPlace : BaseComponent
    {
        public event Action TakenEvent; 
        public event Action ReleasedEvent;
        
        [SerializeField] private TimelineAsset entryScene;
        [SerializeField] private TimelineAsset exitScene;
        [SerializeField] private RuntimeAnimatorController controller;
        private PlayableDirector director;
        private GameCharacterView character;

        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponentInChildren<WorkerCharacter>();
            if (item)
            {
                item.TakeWorkPlace(this);
            }
        }

        public bool Reserved => character;

        public void AddCharacter(GameCharacterView view)
        {
            Assert.IsNull(character);

            character = view;
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            TakenEvent?.Invoke();
            director.playableAsset = entryScene;
            director.enabled = true;
            director.time = 0;
            director.Play();
            yield return new WaitUntil(() => director.time / director.duration > .5f);
            character.animator.runtimeAnimatorController = controller;
            character.animator.applyRootMotion = true;
            yield return new WaitUntil(() => director.time / director.duration >= 1);
            director.enabled = false;
            director.playableAsset = null;
            yield break;
        }


        public void ExtractCharacter()
        {
            var view = character;
            character = null;
            ReleasedEvent?.Invoke();
        }
    }
}