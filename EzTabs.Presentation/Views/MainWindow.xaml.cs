﻿using EzTabs.Data;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewServices;
using EzTabs.Presentation.ViewModels.MainControlsViewModels;
using System.Windows;
using System.Windows.Input;

namespace EzTabs.Presentation.Views
{
    public partial class MainWindow : System.Windows.Window
    {
        private readonly IWindowService _windowService;
        private readonly EzTabsContext _context;

        public MainWindow(IWindowService windowService, EzTabsContext context)
        {
            _context = context;
            _windowService = windowService;
            InitializeComponent();
            SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _windowService.WindowWidth = e.NewSize.Width;
            _windowService.WindowHeight = e.NewSize.Height;
        }
    }
}