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
    public partial class MemoryWindow : Window
    {
        public MemoryWindow()
        {
            InitializeComponent();
        }

        private async Task LoadRegions()
        {
            var sys = Factory.Platform.Value.System;

            var model = this.GetData<MemoryViewModel>();
            model.Regions.Clear();
            if (model.Proc is { } proc)
            {
                Title = $"The memory of {proc.Name} (pid: {proc.Id})";

                foreach (var item in await sys.GetRegions(proc))
                    model.Regions.Add(item);
            }
        }

        private async void OnLoaded(object? sender, RoutedEventArgs e)
        {
            await LoadRegions();
        }

        private async void RefreshClick(object? sender, RoutedEventArgs e)
        {
            await LoadRegions();
        }

        private void OnCellPointerPressed(object? sender, DataGridCellPointerPressedEventArgs e)
        {
            if (e.PointerPressedEventArgs.ClickCount == 2)
            {
            }
        }

        private ContextMenu? _rowMenu;

        private void OnLoadingRow(object? sender, DataGridRowEventArgs e)
        {
            if (_rowMenu == null)
            {
                _rowMenu = new ContextMenu();
                _rowMenu.Items.Add(new MenuItem { Header = "Save memory", Command = GuiExt.Relay(SaveMemory) });
            }
            e.Row.ContextMenu = _rowMenu;
        }

        private void SaveMemory()
        {
            if (Grid.SelectedItem is not IMemRegion region)
                return;
            Sys.CreateMemSave(region);
        }

        private static ISystem Sys => Factory.Platform.Value.System;
    }
}