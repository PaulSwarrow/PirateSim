using System;
using Lib;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
    public class SailStateUi : BaseComponent,  IPointerClickHandler
    {
        public event Action<SailStateUi> ClickEvent; 
        public event Action<SailStateUi> AltClickEvent; 
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

        public int Value
        {
            set => image.color = value == 0 ? Color.gray : Color.white;
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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                ClickEvent?.Invoke(this);
            else if (eventData.button == PointerEventData.InputButton.Right)
                AltClickEvent?.Invoke(this);
        }
    }
}