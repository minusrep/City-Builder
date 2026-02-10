using UnityEngine;

namespace Runtime.Selection
{
    public class SelectionOutline : MonoBehaviour
    {
        [SerializeField] private Outline _outline;
        
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

                    if (_outline != null)
                    {
                        _outline.enabled = false;
                    }
                    
                    break;

            }
        }
    }
}