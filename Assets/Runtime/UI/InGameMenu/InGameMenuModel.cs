using Runtime.Input;

namespace Runtime.UI.InGameMenu
{
    public class InGameMenuModel
    {
        public readonly PlayerControls playerControls;

        public InGameMenuModel(PlayerControls model)
        {
            playerControls = model;
        }
    }
}