using System;

namespace Runtime.UI.HUD
{
    public class BuildingSelectionModel
    {
        public event Action OnChange;
        
        public string SelectedBuildingId { get; private set; }

        public void SelectBuilding(string id)
        {
            SelectedBuildingId = id;
            
            OnChange?.Invoke();
        }

        public void ClearSelectedBuilding()
        {
            SelectedBuildingId = string.Empty;
            
            OnChange?.Invoke();
        }
    }
}