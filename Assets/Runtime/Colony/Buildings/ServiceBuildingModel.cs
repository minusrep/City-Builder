using Runtime.Descriptions.Buildings;
using System.Collections.Generic;
using Runtime.Colony.Citizens;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    public class ServiceBuildingModel : BuildingModel
    {
        private readonly ServiceBuildingDescription _description;
        private readonly Queue<int> _waitingQueue = new();
        private readonly Dictionary<int, long> _inService = new();
        
        private bool _isActive;
        
        public ServiceBuildingModel(int id, Vector2 position, ServiceBuildingDescription description) : base(id, position)
        {
            _description = description;
        }
        
        public bool TryEnter(int citizenId)
        {
            if (_inService.Count < _description.MaxCitizenAmount)
            {
                _inService[citizenId] = _description.ServiceTime;
                return true;
            }

            if (_waitingQueue.Count < _description.MaxQueue)
            {
                _waitingQueue.Enqueue(citizenId);
                return true;
            }

            return false;
        }

        public void Update(long currentTime, ICitizenNeedService  needService)
        {
            var finished = new List<int>();

            foreach (var key in _inService.Keys)
            {
                var remaining = _inService[key] - currentTime;

                if (remaining <= 0)
                {
                    finished.Add(key);
                }
                else
                {
                    _inService[key] = remaining;
                }
            }

            foreach (var citizenId in finished)
            {
                _inService.Remove(citizenId);
                needService.RestoreNeed(citizenId, _description.ServiceResource);

                if (_waitingQueue.Count > 0)
                {
                    var nextCitizen = _waitingQueue.Dequeue();
                    _inService[nextCitizen] = _description.ServiceTime;
                }
            }
        }
    }
}