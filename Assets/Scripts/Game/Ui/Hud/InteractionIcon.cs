using System;
using Game.Systems.Characters;
using Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Hud
{
    public class InteractionIcon : BaseUiComponent
    {
        [SerializeField] private Image img;


        private void Update()
        {
            var hud = GameManager.current.Get<UserCharacterHud>();
            img.gameObject.SetActive(hud.InteractionAvailable);
            if (hud.InteractionAvailable)
            {
                img.rectTransform.anchoredPosition = hud.InteractionViewportPoint;
            }
        }
    }
}