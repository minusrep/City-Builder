using System;
using UnityEngine;

namespace Runtime.Colony.Buildings.Selection
{
    [RequireComponent(typeof(Outline))]
    public class SelectionOutline : MonoBehaviour
    {
        public void SetOutline(SelectionOutlineType type)
        {
            switch (type)
            {
                case SelectionOutlineType.Hover:
                    
                    _outline.OutlineColor = Color.yellow;
                    _outline.OutlineWidth = 5f;
                    _outline.enabled = true;
                    
                    break;
                
                case SelectionOutlineType.Selected:
                    
                    _outline.OutlineColor = Color.green;
                    _outline.OutlineWidth = 10f;
                    _outline.enabled = true;
                    
                    break;
                
                default:
                    
                    _outline.enabled = false;
                    
                    break;

            }
        }

        [SerializeField] private Outline _outline;
    }
}