using Runtime.Common;
using Runtime.Descriptions;
using Runtime.Descriptions.Buildings;
using Runtime.GameSystems;
using Runtime.ViewDescriptions;
using Runtime.ViewDescriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Construction
{
    public class BuildingConstructionPresenter : IPresenter
    {
        private readonly BuildingConstructionModel _model;
        private readonly BuildingConstructionView _view;
        private readonly World _world;
        private readonly GameSystemCollection _systemCollection;
        private readonly BuildingsDescriptionCollection _descriptionCollection;
        private readonly BuildingViewDescriptionCollection _viewDescriptionCollection;

        private BuildingConstructionSystem _system;

        public BuildingConstructionPresenter(BuildingConstructionModel model, BuildingConstructionView view,
            World world,
            GameSystemCollection systemCollection,
            WorldDescription descriptions,
            WorldViewDescriptions viewDescriptions)
        {
            _model = model;
            _view = view;
            _world = world;
            _systemCollection = systemCollection;
            _descriptionCollection = descriptions.BuildingCollection;
            _viewDescriptionCollection = viewDescriptions.BuildingViewDescriptions;
        }
        
        public void Enable()
        {
            _model.IsActive = true;
            _model.SelectedBuilding = _descriptionCollection.Descriptions["sawmill"];
            
            _view.GameObject.SetActive(true);
            var viewDescription = _viewDescriptionCollection.Get(_model.SelectedBuilding.ViewDescriptionId);
            _view.SetPreviewRenderer(Object.Instantiate(viewDescription.Prefab.PreviewRenderer));

            _system = new BuildingConstructionSystem(_model, _view, _world);

            _systemCollection.Add(_system);
        }

        public void Disable()
        {
            _view.GameObject.SetActive(false);
            _systemCollection.Remove(_system);
            _model.IsActive = false;
        }
    }
}