using Lib;
using UnityEngine;

namespace Game.Actors.Character.Interaction
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
            if (other.TryGetComponent<GameCharacterActor>(out var agent))
            {
                agent.OnAreaEnter(area);
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<GameCharacterActor>(out var agent))
            {
                agent.OnAreaExit(area);
            }
            
        }
    }
}