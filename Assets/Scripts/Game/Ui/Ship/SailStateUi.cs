using System;
using App;
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

        [SerializeField] private Sprite[] stateIcons;
        
        [SerializeField] private Image image;
        private float angle;
        public bool Jib;
        private Button btn;


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


        public float Fill
        {
            set
            {
                image.transform.localScale = new Vector3(value < 0? -1:1, 1,1);
                
                var abs = Mathf.Abs(value);
                int i;
                if (abs < GameManager.current.sailsConfig.MinWindCatch) i = 0;
                else i = Mathf.CeilToInt(abs * stateIcons.Length) - 1;
                image.sprite = stateIcons[i];
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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                ClickEvent?.Invoke(this);
            else if (eventData.button == PointerEventData.InputButton.Right)
                AltClickEvent?.Invoke(this);
        }
    }
}