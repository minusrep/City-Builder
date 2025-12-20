using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Colony.Citizens.Collection;
using Runtime.Common;

namespace Runtime.Services.SaveLoadSteps
{
    public class CitizenCollectionLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;

        private readonly World _world;
        
        private readonly CitizenViewCollection _citizenViewCollection;

        public CitizenCollectionLoadStep(List<IPresenter> presenters, World world, CitizenViewCollection citizenViewCollection)
        {
            _presenters = presenters;
            _world = world;
            _citizenViewCollection = citizenViewCollection;
        }

        public async Task Run()
        {
            var citizenCollectionPresenter =
                new CitizenPresenterCollection(_citizenViewCollection, _world.Citizens, _world);
            
            citizenCollectionPresenter.Enable();
            
            _presenters.Add(citizenCollectionPresenter);
            
            await Task.CompletedTask;
        }
    }
}