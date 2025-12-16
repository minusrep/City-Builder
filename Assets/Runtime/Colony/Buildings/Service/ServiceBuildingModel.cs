using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Citizens;
using Runtime.Descriptions.Buildings;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Colony.Buildings.Service
    {
        public class ServiceBuildingModel : BuildingModel
        {
            public IReadOnlyDictionary<int, long> InService => _inService;
            public Queue<int> WaitingQueue { get; private set; } = new();

            private ServiceBuildingDescription Description { get; }

            private Dictionary<int, long> _inService = new();
            
            private bool _isActive;
            
            public ServiceBuildingModel(string id,
                Vector2 position,
                ServiceBuildingDescription description) : base(id,
                position,
                description)
            {
                Description = description;
            }

            public bool TryEnter(int citizenId)
            {
                if (_inService.Count < Description.MaxCitizenAmount)
                {
                    _inService[citizenId] = Description.ServiceTime;
                    _isActive = true;
                    return true;
                }

                if (WaitingQueue.Count < Description.MaxQueue)
                {
                    WaitingQueue.Enqueue(citizenId);
                    return true;
                }

                return false;
            }

            public void Update(long currentTime)
            {
                if (_isActive)
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

                        if (WaitingQueue.Count > 0)
                        {
                            var nextCitizen = WaitingQueue.Dequeue();
                            _inService[nextCitizen] = Description.ServiceTime;
                        }
                    }

                    if (_inService.Count <= 0)
                    {
                        _isActive = false;
                    }
                }
            }

            public override Dictionary<string, object> Serialize()
            {
                var dictionary = new Dictionary<string, object>(base.Serialize())
                {
                    { "is_active", _isActive },
                    { "in_service", _inService.ToJson() },
                    { "waiting_queue", WaitingQueue },
                };
                return dictionary;
            }

            public override void Deserialize(Dictionary<string, object> data)
            {
                _isActive = data.GetBool("is_active");
                _inService = data.GetDictionary<int, long>("in_service");
                WaitingQueue = data.GetQueue<int>("waiting_queue");
            }
        }
    }