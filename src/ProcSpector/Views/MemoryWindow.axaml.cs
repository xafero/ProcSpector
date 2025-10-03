using Avalonia.Controls;
using Avalonia.Interactivity;
using ProcSpector.Lib;
using ProcSpector.Tools;
using ProcSpector.ViewModels;

namespace ProcSpector.Views
{
    public partial class MemoryWindow : Window
    {
        public MemoryWindow()
        {
            InitializeComponent();
        }

        private void LoadRegions()
        {
            var sys = Defaults.System;

            var model = this.GetData<MemoryViewModel>();
            model.Regions.Clear();
            if (model.Proc is { } proc)
            {
                Title = $"The memory of {proc.Name} (pid: {proc.Id})";

                model.Regions.AddRange(sys.GetRegions(proc));
            }
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            LoadRegions();
        }

        private void RefreshClick(object? sender, RoutedEventArgs e)
        {
            LoadRegions();
        }

        private void OnCellPointerPressed(object? sender, DataGridCellPointerPressedEventArgs e)
        {
            if (e.PointerPressedEventArgs.ClickCount == 2)
            {
            }
        }

        private void OnLoadingRow(object? sender, DataGridRowEventArgs e)
        {
        }
    }
}