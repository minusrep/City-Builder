using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Colony.Citizens.Systems;
using Runtime.Colony.StateMachine;
using Runtime.GameSystems;

namespace Runtime.Services.SaveLoadSteps
{
    public class GameSystemsCollectionLoadStep : IStep
    {
        private readonly World _world;
        
        private readonly GameSystemCollection _gameSystems;
        
        public GameSystemsCollectionLoadStep(World world, GameSystemCollection gameSystems)
        {
            _world = world;
            
            _gameSystems = gameSystems;
        }

        public async Task Run()
        {
            var hungrySystem = new CitizenStatSystem("hungry", "satiety", -10);
            
            var feedSystem = new CitizenStatSystem("feed", "satiety", 40);
            
            var fatigueSystem = new CitizenStatSystem("fatigue", "energy", -5);
            
            var sleepSystem = new CitizenStatSystem("sleep", "energy", 10);

            var stateMachineSystem = new StateMachineSystem(_world);
            
            _gameSystems.Add(hungrySystem);
            
            _gameSystems.Add(feedSystem);
            
            _gameSystems.Add(fatigueSystem);
            
            _gameSystems.Add(sleepSystem);
            
            _gameSystems.Add(stateMachineSystem);
            
            await Task.CompletedTask;
        }
    }
}