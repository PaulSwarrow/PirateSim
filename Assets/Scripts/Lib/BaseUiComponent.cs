using UnityEngine;

namespace Lib
{
    public class BaseUiComponent : BaseComponent
    {
        private RectTransform _transform;
        private bool cached;
        public RectTransform rectTransform => cached ? _transform : _transform = (RectTransform) transform;
        
    }
}