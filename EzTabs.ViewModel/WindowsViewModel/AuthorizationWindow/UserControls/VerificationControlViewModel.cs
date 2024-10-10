using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.ViewModel.WindowsViewModel.AuthorizationWindow.UserControls
{
    public class VerificationControlViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<string, string> ShowMessage;

        private string _verificationCode;
        public string VerificationCode 
        {
            get => _verificationCode; 
            set
            {
                _verificationCode = value;
                OnPropertyChanged(VerificationCode);
            } 
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
