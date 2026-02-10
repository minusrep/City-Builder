using Runtime.GameSystems;

namespace Runtime.Colony.Citizens.Systems
{
    public class CitizenStatSystem : RegisterGameSystem<CitizenModel>
    {
        private readonly string _stat;

        private readonly float _changeSpeed;
        
        public CitizenStatSystem(string id, string stat, float changeSpeed) : base(id)
        {
            _stat = stat;
            
            _changeSpeed = changeSpeed;
        }

        protected override void Update(CitizenModel item, float deltaTime)
        {
            item.Stats.Get(_stat).ChangeValue(_changeSpeed * deltaTime);
        }
    }
} 