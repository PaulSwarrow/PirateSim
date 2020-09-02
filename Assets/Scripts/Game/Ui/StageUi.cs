using System;
using System.Collections.Generic;
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

        public static void RequireCursor(object user) => CursorUsers.Add(user);
        public static void LoseCursor(object user) => CursorUsers.Remove(user);
        
        public IStageGoalProvider GoalProvider;

        [SerializeField] private RectTransform menu;
        [SerializeField] private Text goalDescription;
        [SerializeField] private Text goalProgress;

        private void Awake()
        {
            menu.gameObject.SetActive(false);
        }

        private void Update()
        {
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