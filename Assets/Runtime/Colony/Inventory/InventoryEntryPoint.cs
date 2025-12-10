using System.Collections;
using System.Collections.Generic;
using Runtime.Colony.GameResources;
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
        [SerializeField] private UIDocument worldUIDocument;

        [SerializeField] private Transform building;

        private MenuContent _menuContent;
        private InventoryPresenter _inventoryPresenter;

        public void Start()
        {
            _menuContent = new MenuContent(mainUIDocument, worldUIDocument);

            var inventoryView = new InventoryView(_menuContent, cellAsset);
            var inventoryModel = new InventoryModel(4);

            inventoryModel.TryAddItem(MockData.ResourceDescriptions["wood"], 8);
            inventoryModel.TryAddItem(MockData.ResourceDescriptions["iron"], 4);
            inventoryModel.TryAddItem(MockData.ResourceDescriptions["wood"], 15);
            
            _inventoryPresenter = new InventoryPresenter(inventoryModel, inventoryView);
            
            StartCoroutine(TestRoutine());
        }

        private IEnumerator TestRoutine()
        {
            _inventoryPresenter.Enable();
            
            yield return new WaitForSeconds(5f);
            
            _inventoryPresenter.Disable();
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