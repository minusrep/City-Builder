using System;
using Runtime.Colony;
using Runtime.Common;
using Runtime.LoadSteps;

namespace Runtime.UI.InGameMenu.SaveMenu
{
    public class SaveMenuPresenter : IPresenter
    {
        private readonly SaveMenuView _view;
        private readonly World _world;
        private readonly Action _onSaveCompleted;

        public SaveMenuPresenter(SaveMenuView view, World world, Action onSaveCompleted = null)
        {
            _view = view;
            _world = world;
            _onSaveCompleted = onSaveCompleted;
        }

        public void Enable()
        {
            _view.SaveButton.clicked += OnSaveClicked;
            
            var defaultName = "Save_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            _view.SaveNameField.value = defaultName;

            _view.SaveNameField.Focus();
        }

        public void Disable()
        {
            _view.SaveButton.clicked -= OnSaveClicked;
        }

        private async void OnSaveClicked()
        {
            var saveName = _view.SaveNameField.value;
            
            var saveStep = new WorldSaveStep(_world, saveName);
            await saveStep.Run();
            
            _onSaveCompleted?.Invoke();
        }
    }
}
