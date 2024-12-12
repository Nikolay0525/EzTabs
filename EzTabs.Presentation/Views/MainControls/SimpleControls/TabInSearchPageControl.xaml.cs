using System.Windows;
using System.Windows.Controls;

namespace EzTabs.Presentation.Views.MainControls.SimpleControls
{
    public partial class TabInSearchPageControl : UserControl
    {
        public TabInSearchPageControl()
        {
            InitializeComponent();
        }

        private static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TabInSearchPageControl), new PropertyMetadata(string.Empty));
        
        private static readonly DependencyProperty RatingProperty =
            DependencyProperty.Register("Rating", typeof(double), typeof(TabInSearchPageControl), new PropertyMetadata(0.0));

        private static readonly DependencyProperty TabIdProperty =
             DependencyProperty.Register("TabId", typeof(Guid), typeof(TabInSearchPageControl), new PropertyMetadata(Guid.Empty));
        
        private static readonly DependencyProperty CanBeEditedProperty =
             DependencyProperty.Register("CanBeEdited", typeof(bool), typeof(TabInSearchPageControl), new PropertyMetadata(false));

        public Guid TabId
        {
            get { return (Guid)GetValue(TabIdProperty); }
            set { SetValue(TabIdProperty, value); }
        }
        
        public double Rating
        {
            get { return (double)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        
        public bool CanBeEdited
        {
            get { return (bool)GetValue(CanBeEditedProperty); }
            set { SetValue(CanBeEditedProperty, value); }
        }
    }
}
