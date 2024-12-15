using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EzTabs.Presentation.Views.MainControls;

public partial class TabControl : UserControl
{
    public TabControl()
    {
        InitializeComponent();
        this.Loaded += new RoutedEventHandler(ControlBar_Loaded);
    }

    void ControlBar_Loaded(object sender, RoutedEventArgs e)
    {
        var w = Window.GetWindow(ZoomDropButton);
        w.Deactivated += new EventHandler(ControlBarPopUpCloser_Loaded);
        if (w != null)
        {
            w.LocationChanged += (sender2, args) =>
            {
                if (ZoomPopup != null && InfoPopup != null)
                {
                    var zoomPopupOffset = ZoomPopup.HorizontalOffset;
                    ZoomPopup.HorizontalOffset = zoomPopupOffset + 1;
                    ZoomPopup.HorizontalOffset = zoomPopupOffset;
                    
                    var infoPopupOffset = InfoPopup.HorizontalOffset;
                    InfoPopup.HorizontalOffset = infoPopupOffset + 1;
                    InfoPopup.HorizontalOffset = infoPopupOffset;
                    
                    var ratePopupOffset = RatePopup.HorizontalOffset;
                    RatePopup.HorizontalOffset = ratePopupOffset + 1;
                    RatePopup.HorizontalOffset = ratePopupOffset;
                }
            };
            w.SizeChanged += (sender3, e2) =>
            {
                if (ZoomPopup != null && InfoPopup != null)
                {
                    var zoomPopupOffset = ZoomPopup.HorizontalOffset;
                    ZoomPopup.HorizontalOffset = zoomPopupOffset + 1;
                    ZoomPopup.HorizontalOffset = zoomPopupOffset;

                    var infoPopupOffset = InfoPopup.HorizontalOffset;
                    InfoPopup.HorizontalOffset = infoPopupOffset + 1;
                    InfoPopup.HorizontalOffset = infoPopupOffset;

                    var ratePopupOffset = RatePopup.HorizontalOffset;
                    RatePopup.HorizontalOffset = ratePopupOffset + 1;
                    RatePopup.HorizontalOffset = ratePopupOffset;
                }
            };
        }
    }
    void ControlBarPopUpCloser_Loaded(object? sender, EventArgs e)
    {
        ZoomPopup.IsOpen = false;
        InfoPopup.IsOpen = false;
        RatePopup.IsOpen = false;
    }
}
