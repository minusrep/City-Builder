using System;
using System.Collections.Generic;
using Runtime.Colony.Citizens.Collection;
using Runtime.GameSystems;

namespace Runtime.Colony.Buildings.Service
{
    public class ServiceBuildingSystem : IGameSystem
    {
        private readonly CitizenModelCollection _citizens;
        private readonly ServiceBuildingModel _model;
        public string Id { get; }

        public ServiceBuildingSystem(string id, ServiceBuildingModel model, CitizenModelCollection citizens)
        {
            _model = model;
            _citizens = citizens;
            Id = id;
        }

        public void Update(float deltaTime)
        {
            if (_model.IsActive)
            {
                var finished = new List<string>();
                var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                foreach (var key in _model.InService.Keys)
                {
                    var remaining = _model.InService[key] - currentTime;

                    if (remaining <= 0)
                    {
                        finished.Add(key);
                    }
                    else
                    {
                        _model.InService[key] = remaining;
                    }
                }

                foreach (var citizenId in finished)
                {
                    _model.InService.Remove(citizenId);

                    if (_model.WaitingQueue.Count > 0)
                    {
                        var nextCitizen = _model.WaitingQueue.Dequeue();
                        _model.InService[nextCitizen] = _model.Description.ServiceTime;
                    }
                }

                if (_model.InService.Count <= 0)
                {
                    _model.IsActive = false;
                }
            }
        }
    }
}