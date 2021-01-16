using System;
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
            img.gameObject.SetActive(GameManager.CharacterHud.InteractionAvailable);
            if (GameManager.CharacterHud.InteractionAvailable)
            {
                img.rectTransform.anchoredPosition = GameManager.CharacterHud.InteractionViewportPoint;
            }
        }
    }
}