using System.Collections;
using App.AI;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace App.Character.UserControl
{
    /*
     * Implements user control for selected character
     */
    public class UserControlSystem : GameSystem
    {
        public GameCharacter character { get; private set; }

        private UserCharacterStateMachine statemachine;

        public override void Start()
        {
            character = GameCharacterSystem.First();
            statemachine = new UserCharacterStateMachine();
            statemachine.Init(character);
            statemachine.RequireState<MainUserCharacterState>();
        }

        public override void Update()
        {
            
        }
    }
}