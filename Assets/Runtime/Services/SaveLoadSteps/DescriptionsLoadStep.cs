using System.Collections.Generic;
using System.Threading.Tasks;
using fastJSON;
using Runtime.Descriptions;
using UnityEngine;

namespace Runtime.Services.SaveLoadSteps
{
    public class DescriptionsLoadStep : IStep
    {
        private readonly WorldDescription _worldDescription;

        public DescriptionsLoadStep(WorldDescription worldDescription)
        {
            _worldDescription = worldDescription;
        }
        
        public async Task Run()
        {
            var buildingDescriptions =
                JSON.ToObject<Dictionary<string, object>>(
                    Resources.Load<TextAsset>("Descriptions/buildings_description").text);
            var citizensDescriptions =
                JSON.ToObject<Dictionary<string, object>>(
                    Resources.Load<TextAsset>("Descriptions/citizens_description").text);
            var resourcesDescriptions =
                JSON.ToObject<Dictionary<string, object>>(
                    Resources.Load<TextAsset>("Descriptions/items_description").text);

            var pointsOfInterest = 
                JSON.ToObject <Dictionary<string, object>>(
                    Resources.Load<TextAsset>("Descriptions/points_of_interest_description").text);

            var data = new Dictionary<string, object>
            {
                { "buildings", buildingDescriptions },
                { "citizens", citizensDescriptions },
                { "resources", resourcesDescriptions },
                { "points_of_interest", pointsOfInterest}
            };

            _worldDescription.SetData(data);
            
            await Task.CompletedTask;
        }
    }
}