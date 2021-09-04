using System.Collections;
using Game.Actors.Character.Core.Motors;
using Lib.UnityQuickTools.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;

namespace Game.Actors.Character.Interactions
{
    public static class Cutscene
    {
        private const string TrackName = "Character";


        /*public static IEnumerator EnterWorkPlace(GameCharacterActor actor, WorkPlace workPlace)
        {
            Assert.IsNull(actor.currentWorkPlace);
            actor.currentWorkPlace = workPlace;
            yield return TransitionCutscene(actor, workPlace.entryScene,
                workPlace.characterMotor.animator);
            actor.transform.SetParent(workPlace.transform, true);
            actor.SetMotor(workPlace.characterMotor);
        }


        public static IEnumerator ExitWorkPlace(GameCharacterActor actor)
        {
            Assert.IsNotNull(actor.currentWorkPlace);
            yield return TransitionCutscene(
                actor,
                actor.currentWorkPlace.exitScene,
                actor.defaultMotor.animator);
            actor.transform.SetParent(null, true);
            actor.currentWorkPlace.Release();
            actor.currentWorkPlace = null;
            actor.SetDefaultMotor();
        }
        */


        public static IEnumerator TransitionCutscene(GameCharacterActor actor, PlayableDirector director,
            RuntimeAnimatorController nextAnimator = null)
        {
            actor.Core.SetMotor<InternalRootMotionMotor>();
            director.time = 0;
            actor.View.transform.SetParent(director.transform, true);
            var tracks = director.playableAsset.outputs;
            if (tracks.TryFind(item => item.streamName == TrackName, out var track))
            {
                director.SetGenericBinding(track.sourceObject, actor.View.animator);
            }

            director.enabled = true;
            director.Play();
            yield return new WaitUntil(() => director.time / director.duration > .5f);
            if (nextAnimator != null)
            {
                actor.View.animator.runtimeAnimatorController = nextAnimator;
            }

            yield return new WaitUntil(() => director.state == PlayState.Paused);
            director.enabled = false;
            actor.transform.position = actor.View.transform.position;
            actor.transform.rotation = actor.View.transform.rotation;
            actor.View.transform.SetParent(actor.transform, true);
        }
    }
}