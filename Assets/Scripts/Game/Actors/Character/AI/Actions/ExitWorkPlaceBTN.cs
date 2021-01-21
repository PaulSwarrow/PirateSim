using System;
using System.Collections;
using Game.Actors.Character.Interactions;
using UnityEngine.Assertions;

namespace Game.Actors.Character.AI.Hardcode
{
    public class ExitWorkPlaceBTN : BehaviourTreeCoroutine
    {
        public ExitWorkPlaceBTN()
        {
            action = Exit;
        }

        private IEnumerator Exit(Npc npc)
        {
            Assert.IsNotNull(npc.currentWorkPlace);
            
            

            yield return Cutscene.TransitionCutscene(
                npc.character.actor,
                npc.currentWorkPlace.exitScene,
                npc.character.actor.defaultMotor.animator);
            npc.character.actor.transform.SetParent(null, true);
            npc.currentWorkPlace.Release();
            npc.currentWorkPlace = null;

            npc.character.actor.SetDefaultMotor();
        }
    }
}