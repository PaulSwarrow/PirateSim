using System;
using UnityEngine;

namespace Game.Tools
{
    public class PlayerSpawn : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.ReadSceneEvent += AddPlayerCharacter;
        }

        private void AddPlayerCharacter()
        {
            GameManager.ReadSceneEvent -= AddPlayerCharacter;
            GameManager.CharacterUserControl.CreatePlayer(transform.position, transform.forward);
        }
    }
}