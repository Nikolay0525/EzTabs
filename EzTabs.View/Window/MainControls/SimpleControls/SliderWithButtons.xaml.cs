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
    /// Interaction logic for SliderWithButtons.xaml
    /// </summary>
    public partial class SliderWithButtons : UserControl
    {
        public SliderWithButtons()
        {
            InitializeComponent();
        }

        // Dependency Properties for binding
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(SliderWithButtons), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(SliderWithButtons), new PropertyMetadata(100.0));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(SliderWithButtons), new PropertyMetadata(50.0));

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

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        // Event handlers for the buttons
        private void OnMinusButtonClick(object sender, RoutedEventArgs e)
        {
            if (Value > Minimum)
                Value -= 1; // adjust step size here if needed
        }

        private void OnPlusButtonClick(object sender, RoutedEventArgs e)
        {
            if (Value < Maximum)
                Value += 1; // adjust step size here if needed
        }
    }
}
