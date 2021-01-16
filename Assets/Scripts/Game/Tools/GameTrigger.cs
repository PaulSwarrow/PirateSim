using System;
using Lib;
using UnityEngine;

namespace Game.Tools
{
    public class GameTrigger : BaseComponent
    {
        public event Action<GameTrigger, Collider> EnterEvent; 
        private void OnTriggerEnter(Collider other)
        {
            EnterEvent?.Invoke(this, other);
        }
    }
}