using UnityEngine;

namespace Lib
{
    public class BaseUiComponent : BaseComponent
    {
        public RectTransform rectTransform => transform as RectTransform;
        
    }
}