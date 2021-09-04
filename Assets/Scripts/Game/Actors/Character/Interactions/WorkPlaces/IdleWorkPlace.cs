using System;
using Game.Actors.Character.Motors;
using UnityEngine;

namespace Game.Actors.Character.Interactions
{
    public class IdleWorkPlace : WorkPlace
    {
        public override bool AllowChilling => true;
    }
}