using System.Collections;
using System.Collections.Generic;
using App.Character;
using Lib;
using Lib.UnityQuickTools.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace App.AI
{
    public static class Cutscene
    {
        private const string TrackName = "Character";

       

        public static IEnumerator TransitionCutscene(GameCharacterAgent agent, PlayableDirector director, RuntimeAnimatorController nextAnimator = null)
        {
            agent.SetMotor(CutsceneCharacterMotor.Create());
            director.time = 0;
            agent.view.transform.SetParent(director.transform, true);
            var tracks = director.playableAsset.outputs;
            if (tracks.TryFind(item => item.streamName == TrackName, out var track))
            {
                director.SetGenericBinding(track.sourceObject, agent.view.animator);
            }
            
            director.enabled = true;
            director.Play();
            yield return new WaitUntil(() => director.time / director.duration > .5f);
            if (nextAnimator != null)
            {
                agent.view.animator.runtimeAnimatorController = nextAnimator;

            }
            yield return new WaitUntil(() => director.state == PlayState.Paused);
            director.enabled = false;
            agent.transform.position = agent.view.transform.position;
            agent.transform.rotation = agent.view.transform.rotation;
            agent.view.transform.SetParent(agent.transform, true);

        }
    }
}