using System;
using DefaultNamespace;
using Lib;
using Lib.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public class MissionsListUi : BaseComponent
    {
        private LocalFactory<Button> btnFactory;

        private void Awake()
        {
            btnFactory = new LocalFactory<Button>(transform);
        }

        private void Start()
        {
            Button btn;
            foreach (var stage in AppManager.GetStages())
            {
                btn = btnFactory.Create();
                btn.onClick.AddListener(() => AppManager.LoadStage(stage));
                btn.GetComponentInChildren<Text>().text = stage.name;
            }

            btn = btnFactory.Create();
            btn.onClick.AddListener(Application.Quit);
            btn.GetComponentInChildren<Text>().text = "Exit";
        }
    }
}