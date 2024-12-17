using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EzTabs.Presentation.Views.SimpleWindows
{
    public partial class OkInfoMessage : Window
    {
        public string UserInput { get; private set; }

        public OkInfoMessage(string title, string message)
        {
            InitializeComponent();
            this.Title = title;
            MessageTextBlock.Text = message;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
