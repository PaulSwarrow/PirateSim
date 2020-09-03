using System;
using System.Collections.Generic;
using Game.Ui.Dialogs;
using Lib;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Object = System.Object;

namespace DefaultNamespace
{
    public class StageUi : BaseComponent
    {
        private static HashSet<Object> CursorUsers = new HashSet<object>();
        public static StageUi current { get; private set; }

        public static void RequireCursor(object user) => CursorUsers.Add(user);
        public static void LoseCursor(object user) => CursorUsers.Remove(user);
        
        public IStageGoalProvider GoalProvider;

        [SerializeField] private RectTransform menu;
        [SerializeField] private Text goalDescription;
        [SerializeField] private Text goalProgress;
        [SerializeField] private WinDialog winDialog;
        private void Awake()
        {
            menu.gameObject.SetActive(false);
            current = this;
        }

        private void OnDestroy()
        {
            current = null;
        }

        private void Update()
        {
            winDialog.gameObject.SetActive(GoalProvider.GoalAchieved);
            if (CursorUsers.Count > 0)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
            goalDescription.text = GoalProvider.GoalDescription;
            goalProgress.text = GoalProvider.GoalState;

            if (Input.GetButtonDown("Cancel")) menu.gameObject.SetActive(!menu.gameObject.activeSelf);
        }
    }
}