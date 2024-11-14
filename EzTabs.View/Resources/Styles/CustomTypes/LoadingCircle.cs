using EzTabs.Presentation.Views.MainControls.SimpleControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EzTabs.Presentation.Resources.Styles.CustomTypes
{
    public class LoadingCircle : Control
    {

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoadingCircle), new PropertyMetadata(true));

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        static LoadingCircle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingCircle), new FrameworkPropertyMetadata(typeof(LoadingCircle)));
        }
    }
}
