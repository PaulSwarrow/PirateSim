using System;
using System.Collections.Generic;

namespace App.Character.AI
{
    public class AiCharacterSystem : GameSystem
    {
        public event Action<NpcBrain> NpcCreatedEvent;
        public event Action<NpcBrain> NpcDisposedEvent;
        
        private List<NpcBrain> list = new List<NpcBrain>();
        

        public override void Start()
        {
            base.Start();
            var characters = GameManager.Characters.FindAll(item => item.agent.controlMode == CharacterControlMode.ai);
            characters.ForEach(CreateNpc);
        }

        private void CreateNpc(GameCharacter character)
        {
        }

        public override void Update()
        {
        }
    }
}