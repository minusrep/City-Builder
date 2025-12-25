using Runtime.AsyncLoad;
using Runtime.CameraControl;
using Runtime.Colony;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Citizens.Collection;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.GameSystems;
using Runtime.Input;
using Runtime.Services.SaveLoadSteps;
using Runtime.ViewDescriptions;
using System.Collections.Generic;
using Runtime.UI;
using Runtime.UI.InGameMenu;
using UnityEditor;
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
        
        [Header("View")]
        [SerializeField] private BuildingCollectionView _buildingCollectionView;
        [SerializeField] private CitizenViewCollection _citizenViewCollection;
        [SerializeField] private CameraControlView _cameraControlView;

        private readonly WorldDescription _worldDescription = new();

        private readonly WorldViewDescriptions _worldViewDescriptions = new();

        private readonly World _world = new();

        private readonly GameSystemCollection _gameSystems = new();

        private readonly AddressableModel _addressableModel = new();

        private readonly List<IPresenter> _presenters = new();

        private PlayerControls _playerControls;
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

            _playerControls = new PlayerControls();
            _cameraControlModel = new CameraControlModel(_playerControls);
            _cameraControlPresenter = new CameraControlPresenter(_cameraControlModel, _cameraControlView,
                _worldDescription.CameraControlDescription, _gameSystems);
            _cameraControlPresenter.Enable();

            _menuContent = new MenuContent(_menuDocument);

            var pauseMenuModel = new InGameMenuModel(_playerControls);
            var pauseMenuView = new InGameMenuView(_inGameMenuAsset, _loadMenuAsset, _achievementsMenuAsset);
            _inGameMenuPresenter = new InGameMenuPresenter(pauseMenuModel, pauseMenuView, _menuContent);
            _inGameMenuPresenter.Enable();

            Application.quitting += OnQuit;

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

        private void Update()
        {
            _gameSystems.Update(Time.deltaTime);
        }

#if UNITY_EDITOR
        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
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
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
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