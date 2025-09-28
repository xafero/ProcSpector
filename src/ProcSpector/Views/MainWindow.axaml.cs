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
            var model = this.GetData<MainWindowViewModel>();
            model.Processes.Clear();
            model.Processes.AddRange(sys.Processes);
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }

        private void RefreshClick(object? sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }
    }
}