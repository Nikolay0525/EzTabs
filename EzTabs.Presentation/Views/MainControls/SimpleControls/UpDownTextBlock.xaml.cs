using System.Windows;
using System.Windows.Controls;

namespace EzTabs.Presentation.Views.MainControls.SimpleControls
{
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
