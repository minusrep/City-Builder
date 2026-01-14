using System.Collections.Generic;
using Runtime.AsyncLoad;
using Runtime.CameraControl;
using Runtime.Colony;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Buildings.Construction;
using Runtime.Colony.Buildings.Construction.WorldGrid;
using Runtime.Colony.Citizens.Collection;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.GameSystems;
using Runtime.Input;
using Runtime.LoadSteps;
using Runtime.UI;
using Runtime.UI.HUD;
using Runtime.UI.InGameMenu;
using Runtime.ViewDescriptions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [Header("UI")] 
        [SerializeField] private UIDocument _menuDocument;
        [SerializeField] private UIDocument _popupDocument;
        [SerializeField] private UIDocument _hudDocument;

        [Header("View")] 
        [SerializeField] private BuildingCollectionView _buildingCollectionView;
        [SerializeField] private CitizenViewCollection _citizenViewCollection;
        [SerializeField] private CameraControlView _cameraControlView;
        [SerializeField] private BuildingConstructionView _buildingConstructionView;
        [SerializeField] private WorldGridView _worldGridView;
        [SerializeField] private HUDView _hudView;

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
        
        private PlayerControls _playerControls;
        
        private async void Start()
        {
            _menuContent = new MenuContent(_menuDocument, _popupDocument);
            _playerControls = new PlayerControls();

            IStep[] loadSteps =
            {
                new AddressableLoadStep(_addressableModel, _presenters),
                new DescriptionsLoadStep(_worldDescription, _addressableModel),
                new ViewDescriptionsLoadStep(_worldViewDescriptions, _addressableModel),
                
                new WorldLoadStep(_world, _worldDescription, _gameSystems, _playerControls),
                new GameSystemsCollectionLoadStep(_world, _gameSystems),
                new BuildingCollectionLoadStep(_presenters, _world, _buildingCollectionView, _worldViewDescriptions),
                new BuildingConstructionLoadStep(_buildingConstructionView, _worldGridView,
                    _worldDescription, _world, _worldViewDescriptions, _menuContent),
                new CitizenCollectionLoadStep(_presenters, _world, _citizenViewCollection, _worldViewDescriptions),
                new HUDLoadStep(_presenters, _world, _playerControls, _hudView),
                new AchievementCollectionLoadStep(_presenters, _world, _worldViewDescriptions, _menuContent)
            };
            
            _playerControls.Enable();
            
            foreach (var step in loadSteps)
            {
                await step.Run();
            }

            _cameraControlModel = new CameraControlModel(_world.PlayerControls);
            _cameraControlPresenter = new CameraControlPresenter(_cameraControlModel, _cameraControlView,
                _worldDescription.CameraControlDescription, _gameSystems);
            _cameraControlPresenter.Enable();

            var pauseMenuModel = new InGameMenuModel(_world.PlayerControls);
            var inGameMenuView = new InGameMenuView(_worldViewDescriptions.MenuViewDescription.InGameMenuAsset);
            _inGameMenuPresenter = new InGameMenuPresenter(pauseMenuModel, inGameMenuView, _menuContent, _world,
                _worldViewDescriptions);
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

        private void Dispose()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
            Application.quitting -= OnQuit;
            
            _presenters.Reverse();
            foreach (var presenter in _presenters)
            {
                presenter.Disable();
            }

            _inGameMenuPresenter.Disable();
        }
    }
}