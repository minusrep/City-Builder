using Runtime.Descriptions.Buildings;
using Runtime.Input;
using UnityEngine;

namespace Runtime.Colony.Construction
{
    public class BuildingConstructionModel
    {
        public bool IsActive { get; set; }
        public BuildingDescription SelectedBuilding { get; set; }
        public Vector2Int CurrentGridPosition { get; set; }
        public Vector2 CurrentWorldPosition { get; set; }
        public Vector3 VisualWorldOffset { get; set; }
        public bool CanPlace { get; set; }

        public Vector2 CursorPosition => PlayerControls.Construction.MovePreview.ReadValue<Vector2>();

        private PlayerControls PlayerControls { get; }

        public BuildingConstructionModel(PlayerControls playerControls)
        {
            PlayerControls = playerControls;
        }
    }
}