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

       

        public static IEnumerator TransitionCutscene(GameCharacterAgent agent, PlayableDirector director, CharacterMotor nextMotor = null)
        {
            agent.SetMotor(CharacterRootMotionMotor.Create());
            director.time = 0;
            agent.view.transform.SetParent(director.transform, true);
            var tracks = director.playableAsset.outputs;
            if (tracks.TryFind(item => item.streamName == TrackName, out var track))
            {
                director.SetGenericBinding(track.sourceObject, agent.view);
            }
            
            director.enabled = true;
            director.Play();
            yield return new WaitUntil(() => director.time / director.duration > .5f);
            if(nextMotor != null) agent.SetMotor(nextMotor);
            yield return new WaitUntil(() => director.time / director.duration >= 1);
            director.enabled = false;
            agent.view.transform.SetParent(agent.transform, true);
            
        }
    }
}