using Game.Actors.Character;
using Game.Systems.Sea;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public class WindUi : BaseActor<WindUi>
    {
        [SerializeField] private Transform Arrow;
        [SerializeField] private Text Label;

        public Transform RelativeTo;
        // Start is called before the first frame update
        private WindSystem windSystem;
        void Start()
        {
            windSystem = GameManager.current.Get<WindSystem>();

        }

        // Update is called once per frame
        void Update()
        {
            var forward = RelativeTo != null? RelativeTo.forward: Vector3.forward;
            forward.y = 0;
            var angle = Vector3.SignedAngle(windSystem.Force, forward, Vector3.up);
            Arrow.rotation = Quaternion.Euler(0, 0, angle);
            Label.text = "Wind: "+ windSystem.Force.magnitude.ToString("0.00");
        }
    }
}
