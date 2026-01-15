using System.Collections.Generic;
using System.Threading.Tasks;
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
using Runtime.UI.InGameMenu;
using Runtime.ViewDescriptions;
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
        
        private readonly WorldDescription _worldDescription = new();

        private readonly WorldViewDescriptions _worldViewDescriptions = new();

        private readonly World _world = new();

        private readonly GameSystemCollection _gameSystems = new();

        private readonly AddressableModel _addressableModel = new();

        private readonly List<IPresenter> _presenters = new();

        private MenuContent _menuContent;
        private InGameMenuPresenter _inGameMenuPresenter;
        
        private PlayerControls _playerControls;

        private bool _isReloadingSession;
        
        private async void Start()
        {
            _menuContent = new MenuContent(_menuDocument, _popupDocument);
            _playerControls = new PlayerControls();

            IStep[] persistentLoadStep =
            {
                new AddressableLoadStep(_addressableModel, _presenters),
                new DescriptionsLoadStep(_worldDescription, _addressableModel),
                new ViewDescriptionsLoadStep(_worldViewDescriptions, _addressableModel),
            };
            
            _playerControls.Enable();
            
            foreach (var step in persistentLoadStep)
            {
                await step.Run();
            }
            
            await LoadSession();

            InitializeInGameMenu();
    
            Application.quitting += OnQuit;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

        private async Task LoadSession(string saveName = null)
        {
            IStep[] sessionLoadSteps =
            {
                new WorldLoadStep(_world, _worldDescription, _gameSystems, _playerControls, saveName),
                new GameSystemsCollectionLoadStep(_world, _gameSystems),
                new BuildingCollectionLoadStep(_presenters, _world, _buildingCollectionView, _worldViewDescriptions),
                new BuildingConstructionLoadStep(_presenters, _buildingConstructionView, _worldGridView,
                    _worldDescription, _world, _worldViewDescriptions, _menuContent),
                new CitizenCollectionLoadStep(_presenters, _world, _citizenViewCollection, _worldViewDescriptions),
                new AchievementCollectionLoadStep(_presenters, _world, _worldViewDescriptions, _menuContent),
                new CameraControlLoadStep(_presenters, _world, _cameraControlView, _worldDescription),
            };

            foreach (var step in sessionLoadSteps)
            {
                await step.Run();
            }
        }

        private void InitializeInGameMenu()
        {
            var pauseMenuModel = new InGameMenuModel(_world.PlayerControls);
            var pauseMenuView = new InGameMenuView(_worldViewDescriptions.MenuViewDescription.InGameMenuAsset);
            _inGameMenuPresenter = new InGameMenuPresenter(pauseMenuModel, pauseMenuView, _menuContent, _world,
                _worldViewDescriptions, ReloadSessionWithSave);
            _inGameMenuPresenter.Enable();
        }

        private void Update()
        {
            if (!_isReloadingSession)
            {
                _gameSystems.Update(Time.deltaTime);
            }
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

        private void Dispose()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
            Application.quitting -= OnQuit;

            for (var i = _presenters.Count - 1; i >= 0; i--)
            {
                _presenters[i].Disable();
            }

            _inGameMenuPresenter.Disable();
        }
        
        public async Task ReloadSessionWithSave(string saveName)
        {
            _isReloadingSession = true;

            DisposeSessionPresenters();
            
            ClearSessionData();

            await LoadSession(saveName);

            _isReloadingSession = false;
        }

        private void DisposeSessionPresenters()
        {
            for (int i = _presenters.Count - 1; i >= 0; i--)
            {
                var presenter = _presenters[i];
                presenter.Disable();
            }
            
            _presenters.Clear();
        }

        private void ClearSessionData()
        {
            _world.Citizens.Clear();
            _world.Buildings.Clear();
            _world.Achievements.Clear();
            
            _gameSystems.Clear();
            
            _buildingCollectionView.Clear();
            _citizenViewCollection.Clear();
            _worldGridView.Clear();
        }
    }
}