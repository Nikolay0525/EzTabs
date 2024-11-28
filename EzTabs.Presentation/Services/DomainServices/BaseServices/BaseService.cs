using CommunityToolkit.Mvvm.ComponentModel;
using EzTabs.Data;
using EzTabs.Data.Repository;

using EzTabs.Presentation.Services.NavigationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EzTabs.Presentation.Services.DomainServices.BaseServices
{
    public abstract class BaseService<T> : ObservableObject where T : class
    {
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

        protected BaseService(EzTabsContext context, INavigationService navigationService) 
        {
            _navigationService = navigationService;
            _repository = new RepoImplementation<T>(context);
        }
    }
}
