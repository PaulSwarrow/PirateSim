using UnityEngine;

namespace Game.Actors.Ship.View.Sim.Rigs
{
    public class SailClothHolder : BaseSailRig
    {
        [SerializeField] private float radius = 0.1f;
        [SerializeField] private Axis axis;
        public override int Priority => 0;
        protected override Color GizmoColor => Color.red;
        protected override bool CheckVertex(Vector3 vertex) => Mathf.Abs(vertex.GetAxis(axis)) < radius;

        protected override float GetValue(float progress) => 0;
    }
}