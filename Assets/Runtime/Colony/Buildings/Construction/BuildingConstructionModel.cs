using System;
using Runtime.Descriptions.Buildings;
using Runtime.Input;
using UnityEngine;

namespace Runtime.Colony.Buildings.Construction
{
    public class BuildingConstructionModel
    {
        public event Action OnChangeSelectedBuilding;

        public BuildingDescription SelectedBuilding
        {
            get => _selectedBuilding;
            set
            {
                _selectedBuilding = value;
                OnChangeSelectedBuilding?.Invoke();
            }
        }

        public Vector2Int CurrentGridPosition { get; set; }
        public Vector2 CurrentWorldPosition { get; set; }
        public bool CanPlace { get; set; }
        public Vector2 CursorPosition => PlayerControls.Construction.MovePreview.ReadValue<Vector2>();
        private PlayerControls PlayerControls { get; }

        private BuildingDescription _selectedBuilding;

        public BuildingConstructionModel(PlayerControls playerControls)
        {
            PlayerControls = playerControls;
        }
    }
}