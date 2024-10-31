using EzTabs.Services.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.ViewModel.BaseViewModels
{
    public abstract class BaseMainControlViewModel : BaseViewModel
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
        protected BaseMainControlViewModel()
        {
            _userService = new UserService();
            //if (UserService.SavedUser is null) throw new ArgumentNullException(nameof(UserService.SavedUser));
            //Username = UserService.SavedUser.Name;
        }
    }
}
