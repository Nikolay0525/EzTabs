using CommunityToolkit.Mvvm.ComponentModel;
using EzTabs.Data;
using EzTabs.Data.Repository;
using EzTabs.Presentation.Services.ContextServices;
using EzTabs.Presentation.Services.NavigationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EzTabs.Presentation.Services.DomainServices.BaseServices
{
    public abstract class BaseService<T> : ObservableObject where T : class
    {
        protected Task _initializeTask;
        protected RepoImplementation<T> _repository;

        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }

        protected BaseService(IContextFactoryService contextFactoryService, INavigationService navigationService) 
        {
            _navigationService = navigationService;
            _initializeTask = Task.Run(async () =>
            {
                var context = await contextFactoryService.CreateAsync(); 
                _repository = new RepoImplementation<T>(context);    
            });
        }

        protected async Task EnsureRepoCreated()
        {
            if (_repository == null) await _initializeTask;
        }
    }
}
