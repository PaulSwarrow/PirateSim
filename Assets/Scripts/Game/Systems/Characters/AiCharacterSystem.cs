using System.Collections.Generic;
using Game.Actors.Character;
using Game.Actors.Character.AI;
using Game.Interfaces;

namespace Game.Systems.Characters
{
    public class AiCharacterSystem : IGameSystem
    {
        private List<NpcBrain> list = new List<NpcBrain>();


        public void Init()
        {
            
        }

        public void Start()
        {
            var characters = GameManager.Characters.FindAll(item => item.agent.controlMode == CharacterControlMode.ai);
            characters.ForEach(CreateNpc);
        }

        public void Stop()
        {
            
        }

        private void CreateNpc(GameCharacter character)
        {
        }
    }
}