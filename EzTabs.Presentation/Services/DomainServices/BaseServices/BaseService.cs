using EzTabs.Data.Repository;

namespace EzTabs.Presentation.Services.DomainServices.BaseServices
{
    public abstract class BaseService<T> where T : class
    {
        protected Task _initializeTask;
        protected RepoImplementation<T>? _repository;
        protected BaseService() { }
        protected async Task InitializeRepoAsync()
        {
            _repository = await RepoImplementation<T>.CreateRepoAsync();
        }

        protected async Task EnsureRepositoryInitialized()
        {
            if (_initializeTask != null)
            {
                await _initializeTask;
            }

            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }
        }
    }
}
