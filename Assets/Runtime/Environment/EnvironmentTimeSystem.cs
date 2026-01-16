using Runtime.GameSystems;

namespace Runtime.Environment
{
    public class EnvironmentTimeSystem : RegisterGameSystem<EnvironmentTimeModel>
    {
        public EnvironmentTimeSystem(string id) : base(id)
        {
        }

        protected override void Update(EnvironmentTimeModel item, float deltaTime)
        {
            item.Tick(deltaTime);
        }
    }
}