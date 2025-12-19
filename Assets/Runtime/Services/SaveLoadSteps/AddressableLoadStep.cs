using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.AsyncLoad;
using Runtime.Common;

namespace Runtime.Services.SaveLoadSteps
{
    public class AddressableLoadStep : IStep
    {
        private readonly AddressableModel _addressableModel;
        private readonly List<IPresenter> _presenters;

        public AddressableLoadStep(AddressableModel addressableModel, List<IPresenter> presenters)
        {
            _addressableModel = addressableModel;
            _presenters = presenters;
        }
        
        public async Task Run()
        {
            var addressablePresenter =  new AddressablePresenter(_addressableModel);
            addressablePresenter.Enable();
            _presenters.Add(addressablePresenter);
            
            await Task.CompletedTask;
        }
    }
}