using System.Collections.Generic;
using Game.Actors.Character;
using Game.Actors.Character.AI;
using Game.Interfaces;

namespace Game.Systems.Characters
{
    public class AiCharacterSystem : IGameSystem
    {
        private List<Npc> list = new List<Npc>();


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

            var npc = new Npc(character);
            list.Add(npc);
        }
    }
}