using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Colony.Citizens.Systems;
using Runtime.Colony.StateMachine;
using Runtime.Descriptions.Citizens;
using Runtime.GameSystems;
using UnityEngine;

namespace Runtime.Services.SaveLoadSteps
{
    public class GameSystemsCollectionLoadStep : IStep
    {
        private readonly World _world;
        
        private readonly GameSystemCollection _gameSystems;
        
        private readonly CitizensDescription  _citizenDescription;
        
        public GameSystemsCollectionLoadStep(World world, GameSystemCollection gameSystems)
        {
            _world = world;
            
            _gameSystems = gameSystems;
        }

        public async Task Run()
        {
            foreach (var system in _world.WorldDescription.Citizens.Systems.Descriptions)
            {
                _gameSystems.Add(new CitizenStatSystem(system.Key, system.Value.Stat, system.Value.ChangeSpeed));
            }
            
            var stateMachineSystem = new StateMachineSystem(_world);
            
            _gameSystems.Add(stateMachineSystem);
            
            await Task.CompletedTask;
        }
    }
}