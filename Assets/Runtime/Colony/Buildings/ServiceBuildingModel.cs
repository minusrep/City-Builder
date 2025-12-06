    using Runtime.Descriptions.Buildings;
    using System.Collections.Generic;
    using Runtime.Colony.Citizens;
    using Runtime.Utilities;
    using UnityEngine;

    namespace Runtime.Colony.Buildings
    {
        public class ServiceBuildingModel : BuildingModel
        {
            public IReadOnlyDictionary<int, long> InService => _inService;
            public Queue<int> WaitingQueue { get; } = new();
            public ICitizenNeedService NeedService { get; }

            private readonly ServiceBuildingDescription _description;
            private readonly Dictionary<int, long> _inService = new();
            
            private bool _isActive;
            
            public ServiceBuildingModel(int id,
                Vector2 position,
                ServiceBuildingDescription description,
                ICitizenNeedService needService) : base(id,
                position,
                description)
            {
                _description = description;
                NeedService = needService;
            }

            public bool TryEnter(int citizenId)
            {
                if (_inService.Count < _description.MaxCitizenAmount)
                {
                    _inService[citizenId] = _description.ServiceTime;
                    return true;
                }

                if (WaitingQueue.Count < _description.MaxQueue)
                {
                    WaitingQueue.Enqueue(citizenId);
                    return true;
                }

                return false;
            }

            public void Update(long currentTime)
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
                    NeedService.RestoreNeed(citizenId, _description.ServiceResource);

                    if (WaitingQueue.Count > 0)
                    {
                        var nextCitizen = WaitingQueue.Dequeue();
                        _inService[nextCitizen] = _description.ServiceTime;
                    }
                }
            }

            public override Dictionary<string, object> Serialize()
            {
                var dictionary = new Dictionary<string, object>(base.Serialize())
                {
                    { "is_active", _isActive },
                    { "in_service", _inService },
                    { "waiting_queue", WaitingQueue },
                };
                return dictionary;
            }

            //TODO: Дописать десериализацию полей
            public override void Deserialize(Dictionary<string, object> data)
            {
                _isActive = data.GetBool("is_active");
            }
        }
    }