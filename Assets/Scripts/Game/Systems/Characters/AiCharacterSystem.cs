using System.Collections.Generic;
using System.Linq;
using Game.Actors.Character;
using Game.Actors.Character.AI;
using Game.Interfaces;
using UnityEngine;

namespace Game.Systems.Characters
{
    public class AiCharacterSystem : IGameSystem
    {
        private List<NpcBehaviourTree> list = new List<NpcBehaviourTree>();


        public void Init()
        {
        }

        public void Start()
        {
            GameManager.Characters.Foreach(TryCreateNpc);
            GameManager.GizmosEvent += DrawGizmos;
        }

        private void DrawGizmos()
        {
            var npc = list.First().npc;
            if (npc.targetPosition == null) return;
            foreach (var pathCorner in npc.path.corners)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(
                    npc.character.actor.navigator.surface.virtualNavmesh.Virtual2WorldPoint(pathCorner), 1);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(npc.targetPosition.worldPosition, 1);
        }

        public void Stop()
        {
            GameManager.GizmosEvent -= DrawGizmos;
        }

        private void TryCreateNpc(GameCharacter character)
        {
            if (character.actor.controlMode != CharacterControlMode.ai) return;

            var tree = new NpcBehaviourTree
            {
                npc = new Npc(character)
                {
                    liveArea = GameManager.current.currentShip,
                    targetPosition = character.actor.GetCurrentNavPoint()
                },
            };
            list.Add(tree);
            GameManager.current.StartCoroutine(tree.Coroutine());
        }
    }
}