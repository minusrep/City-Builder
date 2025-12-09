using System.Collections.Generic;
using System.Linq;
using Runtime.Colony.Buildings.Models;
using Runtime.Common;
using Runtime.ViewDescriptions.Buildings;
using UnityEngine;
using IPresenter = Runtime.Core.IPresenter;

namespace Runtime.Colony.Buildings.Presenters
{
    public sealed class BuildingCollectionPresenter : IPresenter
    {
        private readonly BuildingModelCollection _models;
        private readonly BuildingViewDescriptionCollection _viewDescriptions;
        private readonly Transform _rootTransform;
        private readonly Dictionary<string, BuildingPresenter> _presenters = new();

        public BuildingCollectionPresenter(BuildingModelCollection models, BuildingViewDescriptionCollection viewDescriptions, Transform rootTransform)
        {
            _models = models;
            _rootTransform = rootTransform;
            _viewDescriptions = viewDescriptions;
        }

        public void Enable()
        {
            _models.OnAdded += HandleAdded;
            _models.OnRemoved += HandleRemoved;

            foreach (var model in _models.Models.Values)
            {
                HandleAdded(model);
            }
        }

        public void Disable()
        {
            _models.OnAdded -= HandleAdded;
            _models.OnRemoved -= HandleRemoved;

            foreach (var presenter in _presenters.Values)
            {
                presenter.Disable();
            }
        }

        private void HandleAdded(BuildingModel model)
        {
            var presenter = new BuildingPresenter(model, _viewDescriptions.GetById(model.BaseDescription.Id), _rootTransform);
            presenter.Enable();
            _presenters.Add(model.Id, presenter);
        }
        
        private void HandleRemoved(BuildingModel model)
        {
            var presenter = _presenters[model.Id];
            presenter.Disable();
            _presenters.Remove(model.Id);
        }
    }
}