using System;
using System.Collections.Generic;
using System.Linq;
using Game.Actors.Character;
using Game.Interfaces;

namespace Game.Systems.Characters
{
    /*
     * Global characters system
     * Implements characters statemachines
     */
    public class GameCharacterSystem : IGameSystem
    {
        private static List<GameCharacter> list = new List<GameCharacter>();

        public static GameCharacter First() => list.First();

        public List<GameCharacter> FindAll(Predicate<GameCharacter> predicate) => list.FindAll(predicate);


        public void RegisterAgent(GameCharacterAgent agent)
        {
            var character = new GameCharacter
            {
                agent = agent,
            };
            character.agent.SetMotor(character.agent.defaultMotor);
            list.Add(character);
        }

        public void Init()
        {
        }

        public void Start()
        {
            GameManager.UpdateEvent += Update;
        }

        public void Stop()
        {
            GameManager.UpdateEvent -= Update;
        }

        private void Update()
        {
            list.ForEach(UpdateCharacter);
        }

        private void UpdateCharacter(GameCharacter character)
        {
        }
    }
}