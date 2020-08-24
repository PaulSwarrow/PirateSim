using System;
using Lib;

namespace Ui
{
    public class SailsSetupUi : BaseComponent
    {
        private void Awake()
        {
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            
        }
    }
}