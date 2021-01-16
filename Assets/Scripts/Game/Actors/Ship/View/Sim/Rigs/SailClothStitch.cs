using UnityEngine;

namespace Game.Actors.Ship.View.Sim.Rigs
{
    public class SailClothStitch : BaseSailRig
    {
        [SerializeField] private Axis axis;
        [SerializeField] private float radius = 0.1f;
        [SerializeField] private float minScale = 0.05f;
        [SerializeField] private float minValue = 0.001f;
        public override int Priority => 2;
        protected override Color GizmoColor => Color.green;
        protected override bool CheckVertex(Vector3 vertex) => Mathf.Abs(vertex.GetAxis(axis)) < radius;

        protected override float GetValue(float progress)
        {
            if (progress < 1)
            {
                return Mathf.Lerp(minValue, 5, Mathf.Pow(progress, 4));
            }

            return float.MaxValue;
        }


        public override void OnClothUpdate(ClothSkinningCoefficient[] coefficients, float progress)
        {
            var s = transform.localScale;
            s.y = Mathf.Max(minScale, progress);
            transform.localScale = s;
            
            base.OnClothUpdate(coefficients, progress);
        }
    }
}