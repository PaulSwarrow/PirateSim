using System;
using DefaultNamespace;
using Lib;
using Lib.Tools;
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
            foreach (var stage in AppManager.GetStages())
            {
                var btn = btnFactory.Create();
                btn.onClick.AddListener(()=> AppManager.LoadStage(stage));
                btn.GetComponentInChildren<Text>().text = stage.name;
            }
        }
    }
}