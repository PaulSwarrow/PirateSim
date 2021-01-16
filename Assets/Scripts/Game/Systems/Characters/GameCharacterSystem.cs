using System;
using System.Collections.Generic;
using System.Linq;
using Game.Actors.Character;
using Game.Interfaces;
using Lib.UnityQuickTools.Collections;

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
        public bool TryFind(Predicate<GameCharacter> predicate, out GameCharacter character) => list.TryFind(predicate, out character);

        public void Foreach(Action<GameCharacter> handler) => list.Foreach(handler);


        public void RegisterAgent(GameCharacterActor actor)
        {
            var character = new GameCharacter
            {
                actor = actor,
            };
            character.actor.SetMotor(character.actor.defaultMotor);
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