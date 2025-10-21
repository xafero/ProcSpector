using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ProcSpector.API;
using ProcSpector.Impl;
using ProcSpector.Tools;
using ProcSpector.ViewModels;

// ReSharper disable AsyncVoidMethod

namespace ProcSpector.Views
{
    public partial class ProcWindow : Window
    {
        public ProcWindow()
        {
            InitializeComponent();
        }

        private async Task LoadProcesses()
        {
            var sys = Factory.Platform.Value.System;
            var f = sys.Flags;
            var usr = f.HasFlag(FeatureFlags.GetUserInfo)
                ? await sys.GetUserInfo()
                : null;
            Title = $"All processes for {usr?.Name ?? "?"} on {usr?.Host ?? "?"}";

            var model = this.GetData<ProcViewModel>();
            model.Processes.Clear();
            if (f.HasFlag(FeatureFlags.GetProcesses))
                await foreach (var item in sys.GetProcesses())
                    if (item.Path is not null)
                        model.Processes.Add(item);
        }

        private async void OnLoaded(object? sender, RoutedEventArgs e)
        {
            await LoadProcesses();
        }

        private async void RefreshClick(object? sender, RoutedEventArgs e)
        {
            await LoadProcesses();
        }

        private void OnCellPointerPressed(object? sender, DataGridCellPointerPressedEventArgs e)
        {
        }

        private void OnLoadingRow(object? sender, DataGridRowEventArgs e)
        {
        }
    }
}