using Runtime.GameSystems;

namespace Runtime.Environment
{
    public class EnvironmentSystem : RegisterGameSystem<EnvironmentModel>
    {
        public EnvironmentSystem(string id) : base(id)
        {
        }

        protected override void Update(EnvironmentModel item, float deltaTime)
        {
            item.Tick(deltaTime);
        }
    }
}