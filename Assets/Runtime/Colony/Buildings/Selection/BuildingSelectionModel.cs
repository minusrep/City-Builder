using System;

namespace Runtime.Colony.Buildings.Selection
{
    public class BuildingSelectionModel
    {
        public event Action OnChange;

        public string SelectedBuildingId { get; private set; }

        public bool CanSelect { get; set; } = true;

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