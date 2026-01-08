using System.Collections.Generic;
using Runtime.AsyncLoad;
using Runtime.CameraControl;
using Runtime.Colony;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Citizens.Collection;
using Runtime.Colony.Construction;
using Runtime.Colony.Construction.Menu;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.GameSystems;
using Runtime.Services.SaveLoadSteps;
using Runtime.UI;
using Runtime.UI.InGameMenu;
using Runtime.ViewDescriptions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [Header("UI")] [SerializeField] private UIDocument _menuDocument;
        [SerializeField] private VisualTreeAsset _inGameMenuAsset;
        [SerializeField] private VisualTreeAsset _loadMenuAsset;
        [SerializeField] private VisualTreeAsset _achievementsMenuAsset;
        [SerializeField] private VisualTreeAsset _constructionMenuAsset;

        [Header("View")] [SerializeField] private BuildingCollectionView _buildingCollectionView;
        [SerializeField] private CitizenViewCollection _citizenViewCollection;
        [SerializeField] private CameraControlView _cameraControlView;
        [SerializeField] private BuildingConstructionView _buildingConstructionView;
        [SerializeField] private WorldGridView _worldGridView;

        private readonly WorldDescription _worldDescription = new();

        private readonly WorldViewDescriptions _worldViewDescriptions = new();

        private readonly World _world = new();

        private readonly GameSystemCollection _gameSystems = new();

        private readonly AddressableModel _addressableModel = new();

        private readonly List<IPresenter> _presenters = new();

        private CameraControlModel _cameraControlModel;
        private CameraControlPresenter _cameraControlPresenter;
        private MenuContent _menuContent;
        private InGameMenuPresenter _inGameMenuPresenter;

        private async void Start()
        {
            IStep[] loadSteps =
            {
                new AddressableLoadStep(_addressableModel, _presenters),
                new DescriptionsLoadStep(_worldDescription),
                new ViewDescriptionsLoadStep(_worldViewDescriptions, _addressableModel),
                new WorldLoadStep(_world, _worldDescription, _gameSystems),
                new GameSystemsCollectionLoadStep(_world, _gameSystems),
                new BuildingCollectionLoadStep(_presenters, _world, _buildingCollectionView,
                    _worldDescription, _worldViewDescriptions, _gameSystems),
                new CitizenCollectionLoadStep(_presenters, _world, _citizenViewCollection, _worldViewDescriptions),
            };

            foreach (var step in loadSteps)
            {
                await step.Run();
            }

            _cameraControlModel = new CameraControlModel(_world.PlayerControls);
            _cameraControlPresenter = new CameraControlPresenter(_cameraControlModel, _cameraControlView,
                _worldDescription.CameraControlDescription, _gameSystems);
            _cameraControlPresenter.Enable();

            _menuContent = new MenuContent(_menuDocument);

            var pauseMenuModel = new InGameMenuModel(_world.PlayerControls);
            var pauseMenuView = new InGameMenuView(_inGameMenuAsset, _loadMenuAsset, _achievementsMenuAsset);
            _inGameMenuPresenter = new InGameMenuPresenter(pauseMenuModel, pauseMenuView, _menuContent);
            _inGameMenuPresenter.Enable();

            var worldGridPresenter = new WorldGridPresenter(_world.Grid, _worldGridView, _world);
            worldGridPresenter.Enable();

            var buildingConstructionMenuView =
                new BuildingConstructionMenuView(_constructionMenuAsset);
            var buildingConstructionMenuPresenter = new BuildingConstructionMenuPresenter(buildingConstructionMenuView,
                _buildingConstructionView, _worldDescription, _world, _worldViewDescriptions, _menuContent);
            buildingConstructionMenuPresenter.Enable();

            Application.quitting += OnQuit;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

        private void Update()
        {
            _gameSystems.Update(Time.deltaTime);
        }

#if UNITY_EDITOR
        private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
        {
            if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
            {
                Dispose();
            }
        }
#endif

        private void OnQuit()
        {
            Dispose();
        }

        private async void Dispose()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
            Application.quitting -= OnQuit;

            var saving = new WorldSaveStep(_world);
            var savingTask = saving.Run();

            _presenters.Reverse();
            foreach (var presenter in _presenters)
            {
                presenter.Disable();
            }

            _inGameMenuPresenter.Disable();

            await savingTask;
        }
    }
}