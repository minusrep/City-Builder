using System.Collections.Generic;
using Runtime.AsyncLoad;
using Runtime.Colony;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Citizens;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.GameSystems;
using Runtime.Services.SaveLoadSteps;
using UnityEngine;

namespace Runtime
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private BuildingCollectionView _buildingCollectionView;
        
        private readonly WorldDescription _worldDescription = new();
        private readonly ViewDescriptions.ViewDescriptions _viewDescriptions  = new();

        private readonly World _world = new();
        
        private readonly GameSystemCollection _gameSystemCollection = new();
        
        private readonly AddressableModel _addressableModel = new();
        
        private readonly List<IPresenter> _presenters = new();

        private async void Start()
        {
            IStep[] loadSteps =
            {
                new AddressableLoadStep(_addressableModel, _presenters),
                new DescriptionsLoadStep(_worldDescription),
                new ViewDescriptionsLoadStep(_viewDescriptions, _addressableModel),
                new WorldLoadStep(_world, _worldDescription),
                new BuildingCollectionLoadStep(_presenters, _world, _buildingCollectionView, 
                    _worldDescription, _viewDescriptions, _gameSystemCollection),
            };

            foreach (var step in loadSteps)
            {
                await step.Run();
            }
        }

        private void Update()
        {
            _gameSystemCollection.Update(Time.deltaTime);
        }

        private void OnDisable()
        {
            _presenters.Reverse();
            foreach (var presenter in _presenters)
            {
                presenter.Disable();
            }
        }
    }
}