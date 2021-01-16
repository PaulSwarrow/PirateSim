using Lib;
using Lib.Tools;
using UnityEngine.UI;

namespace Game.Ui
{
    public class MainMenu : BaseComponent
    {
        private LocalFactory<Button> btnFactory;

        private void Awake()
        {
            btnFactory = new LocalFactory<Button>(transform);
        }
    }
}