using EzTabs.ViewModel.BaseViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EzTabs.ViewModel.ViewModelHelper.ViewModelFactory;

namespace EzTabs.ViewModel.ViewModelHelper
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public T CreateViewModel<T,U>(U? option) where T : BaseViewModel where U : class
        {
            return ActivatorUtilities.CreateInstance<T>(_serviceProvider, option);
        }
    }
}
