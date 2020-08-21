using Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class SailStateUi : BaseComponent
    {
        [SerializeField] private Sprite Positive;
        [SerializeField] private Sprite Zero;
        [SerializeField] private Sprite Negative;
        [SerializeField] private Image image;
        private float angle;
        public bool Jib;
        [SerializeField] private int state;

        public bool Active
        {
            set
            {
                image.color = value ? Color.white : Color.gray;
                if (value) image.transform.SetAsLastSibling();
            }
        }

        public int State
        {
            set
            {
                state = value;
                image.sprite = value == 0 ? Zero : (value > 0 ? Positive : Negative);
            }
            
        }

        public float Angle
        {
            get => angle;
            set
            {
                angle = value;
                transform.localEulerAngles = -Vector3.forward * (angle - (Jib ? 0 : 90));
            }
        }
    }
}