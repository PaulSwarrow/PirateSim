using Lib;
using Lib.UnityQuickTools.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Views
{
    public class ColorizedUiGroup : BaseComponent
    {
        private Image[] images;

        public Color Color
        {
            set {  images.Foreach(item => item.color = value); }
        }

        private void Awake()
        {
            images = GetComponentsInChildren<Image>();
        }
    }
}