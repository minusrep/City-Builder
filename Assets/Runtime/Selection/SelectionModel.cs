using System;

namespace Runtime.Colony.Buildings.Selection
{
    public class SelectionModel
    {
        public event Action OnChange;

        public string SelectedId { get; private set; }

        public bool CanSelect { get; set; } = true;

        public void Select(string id)
        {
            SelectedId = id;
            
            OnChange?.Invoke();
        }

        public void ClearSelected()
        {
            SelectedId = string.Empty;
            
            OnChange?.Invoke();
        }
    }
}