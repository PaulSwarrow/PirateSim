using System;
using System.Collections;
using Game.Actors.Character.Interactions;
using UnityEngine.Assertions;

namespace Game.Actors.Character.AI.Hardcode
{
    public class EnterWorkPlaceBTN : BehaviourTreeCoroutine
    {
        public EnterWorkPlaceBTN()
        {
            action = Enter;
        }

        private IEnumerator Enter(Npc npc)
        {
            Assert.IsNull(npc.currentWorkPlace);
            return Cutscene.EnterWorkPlace(npc.character, npc.targetWorkPlace);
            /*

            yield return Cutscene.TransitionCutscene(npc.character.actor, npc.targetWorkPlace.entryScene,
                npc.targetWorkPlace.characterMotor.animator);
            npc.character.actor.transform.SetParent(npc.targetWorkPlace.transform, true);
            npc.character.actor.SetMotor(npc.targetWorkPlace.characterMotor);*/
        }
    }
}