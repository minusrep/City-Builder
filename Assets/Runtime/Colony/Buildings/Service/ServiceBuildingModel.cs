using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Descriptions.Buildings;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Colony.Buildings.Service
    {
        public class ServiceBuildingModel : BuildingModel
        {
            public Queue<string> WaitingQueue { get; private set; } = new();

            public ServiceBuildingDescription Description { get; }

            public Dictionary<string, long> InService = new();

            public bool IsActive;
            
            public ServiceBuildingModel(string id,
                Vector2 position,
                ServiceBuildingDescription description) : base(id,
                position,
                description)
            {
                Description = description;
            }

            public bool TryEnter(string citizenId)
            {
                if (InService.Count < Description.MaxCitizenAmount)
                {
                    InService[citizenId] = Description.ServiceTime;
                    IsActive = true;
                    return true;
                }

                if (WaitingQueue.Count < Description.MaxQueue)
                {
                    WaitingQueue.Enqueue(citizenId);
                    return true;
                }

                return false;
            }

            public override Dictionary<string, object> Serialize()
            {
                var dictionary = new Dictionary<string, object>(base.Serialize())
                {
                    { "is_active", IsActive },
                    { "in_service", InService.ToJson() },
                    { "waiting_queue", WaitingQueue },
                };
                return dictionary;
            }

            public override void Deserialize(Dictionary<string, object> data)
            {
                IsActive = data.GetBool("is_active");
                InService = data.GetDictionary<string, long>("in_service");
                WaitingQueue = data.GetQueue<string>("waiting_queue");
            }
        }
    }