using Runtime.Common;
using Runtime.UI.InGameMenu.AchievementsMenu;
using Runtime.UI.InGameMenu.LoadMenu;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Runtime.UI.InGameMenu
{
    public class InGameMenuPresenter : IPresenter
    {
        private readonly InGameMenuView _view;
        private readonly InGameMenuModel _model;
        private readonly MenuContent _menuContent;

        private IPresenter _currentMenuPresenter;

        public InGameMenuPresenter(InGameMenuModel model, InGameMenuView view, MenuContent menuContent)
        {
            _model = model;
            _view = view;
            _menuContent = menuContent;
        }

        public void Enable()
        {
            _model.playerControls.UI.Enable();

            _model.playerControls.UI.Pause.performed += OnPerformed;
            _view.ResumeButton.clicked += OnResumeClicked;
            _view.SaveButton.clicked += OnSaveClicked;
            _view.LoadButton.clicked += OnLoadClicked;
            _view.AchievementsButton.clicked += OnAchievementsClicked;
            _view.ExitButton.clicked += OnExitClicked;
        }

        public void Disable()
        {
            _model.playerControls.UI.Pause.performed -= OnPerformed;
            _view.ResumeButton.clicked -= OnResumeClicked;
            _view.SaveButton.clicked -= OnSaveClicked;
            _view.LoadButton.clicked -= OnLoadClicked;
            _view.AchievementsButton.clicked -= OnAchievementsClicked;
            _view.ExitButton.clicked -= OnExitClicked;

            _model.playerControls.UI.Disable();
        }

        private void OnPerformed(InputAction.CallbackContext context)
        {
            if (!_menuContent.MenuRoot.Contains(_view.Root))
            {
                _menuContent.MenuRoot.Add(_view.Root);
                CloseMenu();
            }
            else
            {
                _view.Root.RemoveFromHierarchy();
            }
        }

        private void OpenMenu(VisualElement root, IPresenter loadMenuPresenter)
        {
            CloseMenu();

            _view.PageContent.style.display = DisplayStyle.Flex;
            _view.PageContent.Add(root);

            _currentMenuPresenter = loadMenuPresenter;
            _currentMenuPresenter.Enable();
        }

        private void CloseMenu()
        {
            _view.PageContent.style.display = DisplayStyle.None;

            if (_currentMenuPresenter == null)
            {
                return;
            }

            _currentMenuPresenter.Disable();

            _view.PageContent.Clear();
            _currentMenuPresenter = null;
        }

        private void OnResumeClicked()
        {
            _view.Root.RemoveFromHierarchy();
        }

        //TODO: Save logic
        private void OnSaveClicked()
        {
        }

        private void OnLoadClicked()
        {
            var loadMenuView = new LoadMenuView(_view.LoadPageAsset);
            var loadMenuPresenter = new LoadMenuPresenter(loadMenuView);

            OpenMenu(loadMenuView.Root, loadMenuPresenter);
        }

        private void OnAchievementsClicked()
        {
            var achievementsMenuView = new AchievementsMenuView(_view.AchievementsPageAsset);
            var achievementsMenuPresenter = new AchievementsMenuPresenter(achievementsMenuView);

            OpenMenu(achievementsMenuView.Root, achievementsMenuPresenter);
        }

        private void OnExitClicked()
        {
            Application.Quit();
        }
    }
}