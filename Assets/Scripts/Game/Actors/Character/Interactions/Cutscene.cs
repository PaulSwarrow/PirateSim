using System.Collections;
using Game.Actors.Character.Motors;
using Lib.UnityQuickTools.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Game.Actors.Character.Interactions
{
    public static class Cutscene
    {
        private const string TrackName = "Character";

       

        public static IEnumerator TransitionCutscene(GameCharacterActor actor, PlayableDirector director, RuntimeAnimatorController nextAnimator = null)
        {
            actor.SetMotor(CutsceneCharacterMotor.Create());
            director.time = 0;
            actor.view.transform.SetParent(director.transform, true);
            var tracks = director.playableAsset.outputs;
            if (tracks.TryFind(item => item.streamName == TrackName, out var track))
            {
                director.SetGenericBinding(track.sourceObject, actor.view.animator);
            }
            
            director.enabled = true;
            director.Play();
            yield return new WaitUntil(() => director.time / director.duration > .5f);
            if (nextAnimator != null)
            {
                actor.view.animator.runtimeAnimatorController = nextAnimator;

            }
            yield return new WaitUntil(() => director.state == PlayState.Paused);
            director.enabled = false;
            actor.transform.position = actor.view.transform.position;
            actor.transform.rotation = actor.view.transform.rotation;
            actor.view.transform.SetParent(actor.transform, true);

        }
    }
}