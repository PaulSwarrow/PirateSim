using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Character
{
    /*
     * Global characters system
     * Implements characters statemachines
     */
    public class GameCharacterSystem : GameSystem
    {
        private static List<GameCharacter> list = new List<GameCharacter>();

        public static GameCharacter First() => list.First();

        public List<GameCharacter> FindAll(Predicate<GameCharacter> predicate) => list.FindAll(predicate);


        public static void AddAgent(GameCharacterAgent agent)
        {
            var character = new GameCharacter
            {
                agent = agent,
            };
            character.agent.SetMotor(character.agent.defaultMotor);
            list.Add(character);
        }

        public override void Start()
        {
            base.Start();
        }


        public override void Update()
        {
            list.ForEach(UpdateCharacter);
        }

        private void UpdateCharacter(GameCharacter character)
        {
        }
    }
}