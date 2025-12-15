using System;
using System.Timers;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Pool;

namespace Runtime.Colony.Buildings.Service
{
    public class ServiceBuildingPresenter : BuildingPresenter<ServiceBuildingView>
    {
        private readonly ServiceBuildingModel _model;
        private Timer _timer;
        
        public ServiceBuildingPresenter(ServiceBuildingModel model, IBuildingViewPool viewPool, ViewDescriptions.ViewDescriptions viewDescriptions) : base(model, viewPool, viewDescriptions)
        {
            _model = model;
            _timer = new Timer(1000);
            _timer.AutoReset = true;
        }

        public override void Enable()
        {
            base.Enable();
            _timer.Elapsed += Update;
            _timer.Start();
        }

        public override void Disable()
        {
            base.Disable();
            _timer.Elapsed -= Update;
            _timer.Dispose();
            _timer = null;
        }
        
        private void Update(object sender, ElapsedEventArgs e)
        {
            _model.Update(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
        }
    }
}