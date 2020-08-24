using System;
using Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class SailStateUi : BaseComponent
    {
        public event Action<SailStateUi> ClickEvent; 
        [SerializeField] private Sprite Positive;
        [SerializeField] private Sprite Zero;
        [SerializeField] private Sprite Negative;
        [SerializeField] private Image image;
        private float angle;
        public bool Jib;
        [SerializeField] private int state;
        private Button btn;

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
                image.transform.localEulerAngles = -Vector3.forward * (angle - (Jib ? 0 : 90));
            }
        }

        private void Awake()
        {
            btn = GetComponent<Button>();
        }

        private void OnEnable()
        {
            btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            ClickEvent?.Invoke(this);
        }

        private void OnDisable()
        {
            btn.onClick.RemoveListener(OnClick);
            
        }
    }
}