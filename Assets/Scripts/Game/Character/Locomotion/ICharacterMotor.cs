using UnityEngine;

namespace App.Character.Locomotion
{
    public interface ICharacterMotor
    {
        void Move(Vector3 offset);
        
        Vector3 Forward { get; set; }
        float LocalRotation { get; set; }
    }
}