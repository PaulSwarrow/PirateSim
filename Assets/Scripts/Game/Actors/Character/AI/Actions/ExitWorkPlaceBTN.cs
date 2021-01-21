using System;
using System.Collections;
using Game.Actors.Character.Interactions;
using UnityEditor.Experimental.GraphView;
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
            return Cutscene.ExitWorkPlace(npc.character);
        }
    }
}