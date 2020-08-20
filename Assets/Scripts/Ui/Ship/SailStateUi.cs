using Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class SailStateUi : BaseComponent
    {
        [SerializeField] private Sprite Positive;
        [SerializeField] private Sprite Zero;
        [SerializeField] private Sprite Negative;
        [SerializeField] private Image image;

        public int State
        {
            set => image.sprite = value == 0 ? Zero : (value > 0 ? Positive : Negative);
        }
    }
}