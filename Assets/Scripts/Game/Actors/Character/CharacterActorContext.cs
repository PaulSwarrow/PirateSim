using DI;
using Game.Actors.Character.Core;
using Game.Actors.Character.StateMachine;
using Lib.Navigation;
using UnityEngine;

namespace Game.Actors.Character
{
    public class CharacterActorContext
    {
        [Inject] public DynamicNavmeshAgent agent;
        [Inject] public ICharacterInput input;
        [Inject] public CharacterCore core;
        [Inject] public Transform transform;
        [Inject] public GameCharacterView view;
        [Inject] public CharacterActorSettings settings;
    }
}