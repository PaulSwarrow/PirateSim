using System;
using System.Collections.Generic;
using System.Linq;
using DI;
using Game.Actors.Character;
using Game.Interfaces;
using Lib.UnityQuickTools.Collections;
using UnityEngine;

namespace Game.Systems.Characters
{
    /*
     * Global characters system
     * Implements characters statemachines
     */
    public class GameCharacterSystem : IGameSystem
    {
        [Inject] private ObjectSpawnSystem _spawnSystem;
        private static List<GameCharacter> list = new List<GameCharacter>();

        public static GameCharacter First() => list.First();

        public List<GameCharacter> FindAll(Predicate<GameCharacter> predicate) => list.FindAll(predicate);

        public bool TryFind(Predicate<GameCharacter> predicate, out GameCharacter character) =>
            list.TryFind(predicate, out character);

        public void Foreach(Action<GameCharacter> handler) => list.Foreach(handler);


        public GameCharacter CreateCharacter(Vector3 position, Vector3 forward)
        {
            var character = new GameCharacter
            {
                actor = CreateActor(position, forward),
            };
            character.actor.SetMotor(character.actor.defaultMotor);
            list.Add(character);
            return character;
        }

        private GameCharacterActor CreateActor(Vector3 position, Vector3 forward)
        {
            return _spawnSystem.Spawn(
                GameManager.Properties.characterActorPrefab,
                position,
                Quaternion.LookRotation(forward, Vector3.up));
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