namespace Runtime.UI.HUD
{
    public class HUDModel
    {
        public BuildingSelectionModel BuildingSelectionModel { get; private set; }

        public HUDModel()
        {
            BuildingSelectionModel = new BuildingSelectionModel();
        }
    }
}