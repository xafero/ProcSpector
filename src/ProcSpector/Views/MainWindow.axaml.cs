using Avalonia.Controls;
using Avalonia.Interactivity;
using ProcSpector.Lib;
using ProcSpector.Tools;
using ProcSpector.ViewModels;

namespace ProcSpector.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadProcesses()
        {
            var sys = Defaults.System;
            Title = $"All processes for {sys.UserName} on {sys.HostName}";

            var model = this.GetData<MainViewModel>();
            model.Processes.Clear();
            model.Processes.AddRange(sys.GetAllProcesses());
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }

        private void RefreshClick(object? sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }

        private void OnCellPointerPressed(object? sender, DataGridCellPointerPressedEventArgs e)
        {
            if (e.PointerPressedEventArgs.ClickCount == 2 && e.Row.GetDataRaw<IProcess>() is { } proc)
            {
                var modWind = new ModuleWindow { DataContext = new ModuleViewModel { Proc = proc } };
                modWind.ShowDialog(this);
            }
        }
    }
}