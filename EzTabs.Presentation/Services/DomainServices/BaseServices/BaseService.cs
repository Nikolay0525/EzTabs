using EzTabs.Data;
using EzTabs.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EzTabs.Presentation.Services.DomainServices.BaseServices
{
    public abstract class BaseService<T> where T : class
    {
        protected Task _initializeTask;
        protected RepoImplementation<T>? _repository;
        protected BaseService(EzTabsContext context) 
        {
            _repository = new RepoImplementation<T>(context);
        }

        protected void EnsureRepositoryInitialized()
        {
            if (_repository == null) throw new InvalidOperationException("Repository is not initialized.");
        }
    }
}
