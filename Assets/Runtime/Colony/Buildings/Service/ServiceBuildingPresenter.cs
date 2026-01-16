using Runtime.Colony.Buildings.Common;
using Runtime.Common.ObjectPool;
using Runtime.ViewDescriptions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Service
{
    public class ServiceBuildingPresenter : BuildingPresenter
    {
        private readonly ServiceBuildingModel _model;

        public ServiceBuildingPresenter(ServiceBuildingModel model, IObjectPool<BuildingView> viewPool, WorldViewDescriptions worldViewDescriptions) : base(model, viewPool, worldViewDescriptions)
        {
            _model = model;
        }

        public override void Enable()
        {
            base.Enable();
            
            _model.OnCitizenAmountChanged += OnCitizenAmountChanged;
        }

        public override void Disable()
        {
            _model.OnCitizenAmountChanged += OnCitizenAmountChanged;
        }
        
        private void OnCitizenAmountChanged(int value)
        {
            Debug.Log(View.ServiceCitizensLabel);
            if (View.ServiceCitizensLabel == null)
                return;
            Debug.Log(1);
            View.ServiceCitizensLabel.text = value.ToString();
            View.ServiceCitizensLabel.style.display = DisplayStyle.Flex;
        }
    }
}