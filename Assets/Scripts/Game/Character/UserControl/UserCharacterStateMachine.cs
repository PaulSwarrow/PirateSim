using System;
using System.Collections.Generic;
using App.AI;
using App.Character.UserControl;
using App.Tools;
using Lib.UnityQuickTools.Collections;

namespace App.Character
{
    public class UserCharacterStateMachine : CharacterStateMachine
    {
        protected override GenericMap<GameCharacterState> PrepareStates()
        {
            var map = new GenericMap<GameCharacterState>();
            map.Set(new MainUserCharacterState());
            map.Set(new WorkingUserCharacterState());
            return map;
        }

    }
}