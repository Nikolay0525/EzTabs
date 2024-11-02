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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EzTabs.View.Window.MainControls.SimpleControls
{
    /// <summary>
    /// Interaction logic for UpDownTextBlock.xaml
    /// </summary>
    public partial class UpDownTextBlock : UserControl
    {
        public UpDownTextBlock()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(UpDownTextBlock), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(UpDownTextBlock), new PropertyMetadata(100.0));

        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(double), typeof(UpDownTextBlock), new PropertyMetadata(50.0));

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public double CurrentValue
        {
            get => (double)GetValue(CurrentValueProperty);
            set => SetValue(CurrentValueProperty, value);
        }

        private void OnMinusButtonClick(object sender, RoutedEventArgs e)
        {
            if (CurrentValue > Minimum)
                CurrentValue -= 1;
        }

        private void OnPlusButtonClick(object sender, RoutedEventArgs e)
        {
            if (CurrentValue < Maximum)
                CurrentValue += 1;
        }
    }
}
