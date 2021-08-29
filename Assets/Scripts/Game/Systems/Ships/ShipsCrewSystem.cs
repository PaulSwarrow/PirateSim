using System.Collections.Generic;
using DI;
using Game.Actors.Character;
using Game.Interfaces;
using Game.Models;
using Game.Systems.Characters;
using Game.Tools;
using UnityEngine;

namespace Game.Systems
{
    public class ShipsCrewSystem : IGameSystem
    {
        [Inject] private AiCharacterSystem _aiSystem;
        public void Init()
        {
        }

        public Crew CreateCrew(int amount, ICharacterLiveArea livingArea)
        {
            var crew = new Crew
            {
                livigArea = livingArea
            };
            crew.staff = CreateStaff(amount, livingArea);
            return crew;
        }

        private List<GameCharacter> CreateStaff(int amount, ICharacterLiveArea livingArea)
        {
            var list = new List<GameCharacter>();
            for (var i = 0; i < amount; i++)
            {
                var position = livingArea.FindRandomPlace();
                var character = _aiSystem.Create(position.WorldPosition, Geometry.GetRandomForward());
                character.livingArea = livingArea;
                list.Add(character);
            }

            return list;
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}