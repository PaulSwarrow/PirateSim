using Game.AI;
using Services.AI.Configs;
using UnityEngine;

namespace App.Configs
{
    [CreateAssetMenu(menuName = "PG/Npc Behavior", fileName ="Npc behavior")]
    public class NpcBehaviorConfig  : BehaviourTreeConfig<CharacterBehaviourContext>
    {
        
    }
}