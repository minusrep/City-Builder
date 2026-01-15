using Runtime.Common;
using UnityEngine.UIElements;

namespace Runtime.Colony.Citizens.HUD
{
    public class CitizenHUDPresenter : IPresenter
    {
        private const string CitizenNameStyle = "citizen-name";
        
        private readonly CitizenHUDView _view;
        
        private readonly CitizenModel _model;

        public CitizenHUDPresenter(CitizenHUDView view, CitizenModel model)
        {
            _view = view;
            _model = model;
        }
        
        public void Enable()
        {
            var nameTextElement = new TextElement()
            {
                text = _model.Name,
            };

            nameTextElement.AddToClassList(CitizenNameStyle);
            
            _view.Root.Add(nameTextElement);
        }

        public void Disable()
        {
            _view.Root?.Clear();
        }
    }
}