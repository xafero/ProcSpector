using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ProcSpector.API;
using ProcSpector.Core;
using ProcSpector.Core.Plugins;
using ProcSpector.Impl;
using ProcSpector.Impl.Net.Tools;
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
            var f = sys.Flags;

            var model = this.GetData<HandleViewModel>();
            model.Handles.Clear();
            if (model.Proc is { } proc)
            {
                Title = $"The windows of {proc.Name} (pid: {proc.Id})";

                if (f.HasFlag(FeatureFlags.GetWindows))
                    await foreach (var item in sys.GetHandles(proc))
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
                _rowMenu.FillContextMenu(CtxMenu.Window, sender ?? this);
                CreateContextMenu(_rowMenu);
            }
            e.Row.ContextMenu = _rowMenu;
        }

        private void CreateContextMenu(ContextMenu menu)
        {
            var sys = Factory.Platform.Value.System;
            var f = sys.Flags;
            if (f.HasFlag(FeatureFlags.CopyScreen))
                menu.Items.Add(new MenuItem { Header = "Copy screen", Command = GuiExt.Relay(CopyScreen) });
        }

        private async Task CopyScreen()
        {
            if (Grid.SelectedItem is not IHandle handle)
                return;
            if ((await Sys.CreateScreenShot(handle)).Save() is { } file)
                ProcExt.OpenInShell(file);
        }

        private static ISystem Sys => Factory.Platform.Value.System;
    }
}