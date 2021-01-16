using UnityEngine;

namespace Game.Actors.Ship.View
{
    public class WheelView : MonoBehaviour
    {
        private Keel model;

        // Start is called before the first frame update
        void Start()
        {
            model = GetComponentInParent<Keel>();

        }

        // Update is called once per frame
        void Update()
        {
            transform.localRotation = Quaternion.Euler(0,0, -model.wheel);
        }
    }
}
