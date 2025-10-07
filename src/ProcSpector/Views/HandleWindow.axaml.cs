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
    public partial class HandleWindow : Window
    {
        public HandleWindow()
        {
            InitializeComponent();
        }

        private async Task LoadHandles()
        {
            var sys = Factory.Platform.Value.System;

            var model = this.GetData<HandleViewModel>();
            model.Handles.Clear();
            if (model.Proc is { } proc)
            {
                Title = $"The windows of {proc.Name} (pid: {proc.Id})";

                foreach (var item in await sys.GetHandles(proc))
                    model.Handles.Add(item);
            }
        }

        private async void OnLoaded(object? sender, RoutedEventArgs e)
        {
            await LoadHandles();
        }

        private async void RefreshClick(object? sender, RoutedEventArgs e)
        {
            await LoadHandles();
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
                _rowMenu.Items.Add(new MenuItem { Header = "Copy screen", Command = GuiExt.Relay(CopyScreen) });
            }
            e.Row.ContextMenu = _rowMenu;
        }

        private void CopyScreen()
        {
            if (Grid.SelectedItem is not IHandle handle)
                return;
            Sys.CreateScreenShot(handle);
        }

        private static ISystem Sys => Factory.Platform.Value.System;
    }
}