using System;
using Runtime.Common;
using Runtime.SaveSystem;
using Runtime.ViewDescriptions;

namespace Runtime.UI.InGameMenu.LoadMenu
{
    public class LoadMenuPresenter : IPresenter
    {
        private readonly LoadMenuView _view;
        private readonly Action<string> _onLoadSelected;
        private readonly WorldViewDescriptions _worldViewDescriptions;

        public LoadMenuPresenter(LoadMenuView view, Action<string> onLoadSelected, WorldViewDescriptions worldViewDescriptions)
        {
            _view = view;
            _onLoadSelected = onLoadSelected;
            _worldViewDescriptions = worldViewDescriptions;
        }

        public void Enable()
        {
            _view.RefreshButton.clicked += RefreshSavesList;
            RefreshSavesList();
        }

        public void Disable()
        {
            _view.RefreshButton.clicked -= RefreshSavesList;
        }

        private void RefreshSavesList()
        {
            _view.SavesList.Clear();

            var saves = SaveFileManager.GetAllSaves();
            
            foreach (var save in saves)
            {
                var loadPanel = new LoadPanel(_worldViewDescriptions.LoadViewDescription.LoadAsset);
                
                loadPanel.Title.text = save.DisplayName;
                loadPanel.Date.text = save.LastModified.ToString("dd.MM.yyyy");
                loadPanel.Time.text = save.LastModified.ToString("HH:mm");
                
                loadPanel.LoadButton.clicked += () => OnLoadClicked(save.FileName);
                loadPanel.DeleteButton.clicked += () => OnDeleteClicked(save.FileName);
                
                _view.SavesList.Add(loadPanel.Root);
            }
        }

        private void OnLoadClicked(string saveName)
        {
            _onLoadSelected?.Invoke(saveName);
        }

        private void OnDeleteClicked(string saveName)
        {
            SaveFileManager.DeleteSave(saveName);
            RefreshSavesList();
        }
    }
}