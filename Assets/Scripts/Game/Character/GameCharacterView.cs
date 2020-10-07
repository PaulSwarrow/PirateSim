using System;
using Lib;
using UnityEngine;

namespace App.Character
{
    public class GameCharacterView : BaseComponent
    {
        public Animator animator { get; private set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}