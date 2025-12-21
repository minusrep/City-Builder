using System.Linq;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Service;
using Runtime.Colony.Citizens.Collection;
using Runtime.Common;
using UnityEngine;

namespace Runtime.Colony.ShelterCitizenSpawn
{
    public class ShelterCitizenSpawnPresenter : IPresenter
    {
        private const string ShelterDescriptionKey = "shelter";
        
        private readonly BuildingModelCollection _buildings;
        private readonly CitizenModelCollection _citizens;

        public ShelterCitizenSpawnPresenter(BuildingModelCollection buildings, CitizenModelCollection citizens)
        {
            _buildings = buildings;
            _citizens = citizens;
        }

        public void Enable()
        {
            foreach (var building in _buildings.Models.Values)
            {
                if (building is ServiceBuildingModel serviceBuildingModel &&
                    serviceBuildingModel.Description.Id == ShelterDescriptionKey)
                {
                    EnsureCitizensForShelter(serviceBuildingModel);
                }
            }

            _buildings.OnAdded += OnBuildingAdded;
        }

        public void Disable()
        {
            _buildings.OnAdded -= OnBuildingAdded;
        }

        private void EnsureCitizensForShelter(ServiceBuildingModel shelter)
        {
            var existingCount = _citizens.Models.Values.Count(citizen => citizen.SpawnedFromBuildingId == shelter.Id);

            var needToSpawn = shelter.Description.MaxCitizenAmount - existingCount;

            for (var i = 0; i < needToSpawn; i++)
            {
                SpawnCitizen(shelter);
            }
        }

        private void SpawnCitizen(ServiceBuildingModel shelter)
        {
            var position = GetSpawnPosition(shelter.Position);
            _citizens.Create();
            var citizenModel = _citizens.Models.Last().Value;
            citizenModel.Position = position;
            citizenModel.SpawnedFromBuildingId = shelter.Id;
        }

        private Vector3 GetSpawnPosition(Vector2 center)
        {
            var random = Random.onUnitSphere * 10f;
            var insideUnitCircle = center + new Vector2(random.x, random.z);
            return new Vector3(insideUnitCircle.x, 0f, insideUnitCircle.y);
        }
        
        private void OnBuildingAdded(BuildingModel building)
        {
            if (building is ServiceBuildingModel serviceBuildingModel &&
                serviceBuildingModel.Description.Id == ShelterDescriptionKey)
            {
                EnsureCitizensForShelter(serviceBuildingModel);
            }
        }
    }
}