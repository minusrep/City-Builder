using Runtime.Descriptions;
using Runtime.Descriptions.Buildings;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Runtime
{
    public sealed class EntryPoint : MonoBehaviour
    {
        private void Start()
        {
            var factory = new DescriptionFactory();
            factory.Register("production", typeof(ProductionBuildingDescription));
            factory.Register("service", typeof(ServiceBuildingDescription));
            factory.Register("storage", typeof(StorageBuildingDescription));
            var json = Resources.Load<TextAsset>("Buildings");
            var buildingDescriptions = new BuildingsDescriptionCollection();
            JsonConvert.PopulateObject(json.text, buildingDescriptions);
            Debug.Log(buildingDescriptions);
        }
    }
}