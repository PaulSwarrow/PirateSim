using Lib;
using UnityEngine;

namespace Game.Actors.Character.Interactions
{
    public class InteractiveObject : BaseComponent
    {
        public Vector3 position => transform.position;
    }
}