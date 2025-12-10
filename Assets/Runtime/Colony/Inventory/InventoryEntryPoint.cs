using Runtime.Systems;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryEntryPoint : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset cellAsset;
        
        [SerializeField] private UIDocument mainUIDocument;

        [SerializeField] private Transform building;
        
        private MenuContent _menuContent;

        public void Start()
        {
            _menuContent = new MenuContent(mainUIDocument);

            var inventoryView = new InventoryView(_menuContent, cellAsset);
            var inventoryPresenter = new InventoryPresenter(inventoryView, 1, building);
            inventoryPresenter.Enable();
        }


    }
}