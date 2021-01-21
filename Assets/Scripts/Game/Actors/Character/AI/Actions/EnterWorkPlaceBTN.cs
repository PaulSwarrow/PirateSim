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
            return Cutscene.EnterWorkPlace(npc.character, npc.targetWorkPlace);
        }
    }
}