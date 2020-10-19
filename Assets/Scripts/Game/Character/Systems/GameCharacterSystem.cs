using System;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace App.Character
{
    /*
     * Global characters system
     * Implements characters lifecycle
     */
    public class GameCharacterSystem : GameSystem
    {
        private static List<GameCharacter> list = new List<GameCharacter>();

        public static GameCharacter First() => list.First();

        public static void AddAgent(GameCharacterAgent agent)
        {
            var character = new GameCharacter
            {
                agent = agent
            };
            list.Add(character);
        }

        
        public override void Update()
        {
        }
    }
}