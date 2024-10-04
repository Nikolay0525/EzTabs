using EzTabs.Model.Model.BaseClasses;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EzTabs.View.Windows.AuthorizationWindow.UserControls;

namespace EzTabs.View.Windows.AuthorizationWindow
{
    public partial class AuthorizationWindow : Window
    {
        bool BrightTheme = true;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void BrightnessModeButton_Click(object sender, RoutedEventArgs e)
        {
            /*Image BrightImage = new()
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/EzTabs.View;component/Resources/Images/BrightMode.png"))
            };

            Image DarkImage = new()
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/EzTabs.View;component/Resources/Images/DarkMode.png"))
            };

            if (BrightTheme)
            {
                BrightnessModeButton.Content = DarkImage;
                this.Background = Brushes.DarkBlue;
                BrightTheme = false;
            }
            else if (!BrightTheme)
            {
                BrightnessModeButton.Content = BrightImage;
                this.Background = Brushes.White;
                BrightTheme = true;
            }*/
        }
    }
}