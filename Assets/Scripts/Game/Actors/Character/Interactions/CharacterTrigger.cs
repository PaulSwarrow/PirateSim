using Lib;
using UnityEngine;

namespace Game.Actors.Character.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class CharacterTrigger : BaseComponent
    {

        private Collider area;
        private void Awake()
        {
            area = GetComponent<Collider>();
            
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<GameCharacterAgent>(out var agent))
            {
                agent.OnAreaEnter(area);
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<GameCharacterAgent>(out var agent))
            {
                agent.OnAreaExit(area);
            }
            
        }
    }
}