using System.Collections;
using System.Linq;
using App.AI;
using Lib.UnityQuickTools.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace App.Character
{
    public class CharacterCutsceneState : GameCharacterState
    {
        public CharacterMotor nextMotor;

        public PlayableDirector director;
        private string trackName;

        public override void Start()
        {
            character.agent.SetMotor(CharacterRootMotionMotor.Create());
            director.time = 0;
            character.agent.view.transform.SetParent(director.transform, true);
            var tracks = director.playableAsset.outputs;
            if (tracks.TryFind(item => item.streamName == trackName, out var track))
            {
                director.SetGenericBinding(track.sourceObject, character.agent.view);
            }

            character.agent.StartCoroutine(Cutscene());
            
        }

        private IEnumerator Cutscene()
        {
            director.enabled = true;
            director.Play();
            yield return new WaitUntil(() => director.time / director.duration > .5f);
            character.agent.SetMotor(nextMotor);
            yield return new WaitUntil(() => director.time / director.duration >= 1);
            director.enabled = false;
        }

        public override void Update()
        {
        }

        public override void Stop()
        {
        }
    }
}