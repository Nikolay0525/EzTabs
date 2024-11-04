using EzTabs.Services.ModelServices;
using EzTabs.ViewModel.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.ViewModel.MainControlsViewModels.SimpleControlsViewModels
{
    public class MainControlBarViewModel : BaseViewModel
    {
        protected UserService? _userService;
        private string? _username;
        protected string? Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        protected MainControlBarViewModel()
        {
            _userService = new UserService();
            if (UserService.SavedUser is null) throw new ArgumentNullException(nameof(UserService.SavedUser));
            Username = UserService.SavedUser.Name;
        }
    }
}
