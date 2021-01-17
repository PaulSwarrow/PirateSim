using System.Collections.Generic;
using Game.Actors.Character;
using Game.Actors.Character.AI;
using Game.Interfaces;

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
        }

        public void Stop()
        {
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