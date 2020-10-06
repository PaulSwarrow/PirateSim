using UnityEngine;

namespace App.Character.Locomotion
{
    public interface ICharacterMotor
    {
        
        Vector3 Forward { get; set; }
        Vector3 Velocity { get; set; }
    }
}