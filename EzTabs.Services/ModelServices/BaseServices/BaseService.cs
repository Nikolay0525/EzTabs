using EzTabs.Data.Repository;
using EzTabs.Model;
using EzTabs.Services.RepoServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Services.ModelServices.BaseServices
{
    public abstract class BaseService
    {
        protected Task _initializeTask;
        protected object? _repository;
        protected BaseService(){}

        protected async Task<RepoImplementation<T>> InitializeRepoAsync<T>() where T : class
        {
            _repository = await RepoInitializeService.InitializeRepoAsync<T>();
            return await RepoInitializeService.InitializeRepoAsync<T>();
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
