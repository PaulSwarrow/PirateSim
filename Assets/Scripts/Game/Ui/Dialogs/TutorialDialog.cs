using System;
using System.Security.Cryptography;
using DefaultNamespace;
using Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Dialogs
{
    public class TutorialDialog : BaseComponent
    {
        public event Action CloseEvent;
        [SerializeField] private Button okBtn;

        public bool Complete;
        private void Awake()
        {
            okBtn.onClick.AddListener(Close);
        }

        private void Close()
        {
            Complete = true;
            CloseEvent?.Invoke();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        private void OnEnable()
        {
            StageUi.RequireCursor(this);
        }

        private void OnDisable()
        {
            StageUi.LoseCursor(this);
        }
    }
}