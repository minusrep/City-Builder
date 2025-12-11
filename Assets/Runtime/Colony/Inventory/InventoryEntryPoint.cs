using System.Collections.Generic;
using Runtime.Colony.GameResources;
using Runtime.Descriptions;
using Runtime.Descriptions.GameResources;
using Runtime.Systems;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryEntryPoint : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset cellAsset;

        [SerializeField] private UIDocument mainUIDocument;

        [SerializeField] private Transform building1;
        [SerializeField] private Transform building2;
        [SerializeField] private InventoryViewDescription inventoryViewDescription;

        private MenuContent _menuContent;
        private InventoryPresenter _inventoryPresenter;

        public void Start()
        {
            _menuContent = new MenuContent(mainUIDocument, null);
            
            var inventoryModel = new InventoryModel(4);

            inventoryModel.TryAddItem(MockData.ResourceDescriptions["wood"], 8);
            inventoryModel.TryAddItem(MockData.ResourceDescriptions["iron"], 4);
            inventoryModel.TryAddItem(MockData.ResourceDescriptions["wood"], 15);
            
            var _inventoryPresenter = new InventoryPresenter(inventoryModel, inventoryViewDescription, building1);
            
            _inventoryPresenter.Enable();
            
            var inventoryModel1 = new InventoryModel(2);

            inventoryModel1.TryAddItem(MockData.ResourceDescriptions["wood"], 8);
            inventoryModel1.TryAddItem(MockData.ResourceDescriptions["iron"], 4);
            
            var _inventoryPresenter2 = new InventoryPresenter(inventoryModel1, inventoryViewDescription, building2);
            
            _inventoryPresenter2.Enable();
        }
    }
    
    public static class MockData
    {
        public static Dictionary<string, ResourceModel> ResourceDescriptions = new()
        {
            {
                "wood",
                new ResourceModel(new ResourceDescription(new Dictionary<string, object>
                    {
                        { "type", "wood" },
                        { "reduction_time", 0},
                        { "reduction_amount", 0}
                    }
                ))
                {
                    MaxAmount = 10
                }
            },
            {
                "iron",
                new ResourceModel(new ResourceDescription(new Dictionary<string, object>
                {
                    { "type", "iron" },
                    { "reduction_time", 0},
                    { "reduction_amount", 0}
                }))
                {
                    MaxAmount = 10
                }
            }
        };
    }
}