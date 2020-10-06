using Cinemachine;
using UnityEngine;

namespace Lib.Tools.CinemachineExtensions
{
    public class DynamicSurfaceFreeLook : CinemachineExtension
    {
        [SerializeField] private Rigidbody surface;
 
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var offset = surface.GetPointVelocity(state.RawPosition);
                state.RawPosition += offset * Time.fixedDeltaTime;
            }
        }
        
    }
}