using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ProcSpector.API;
using ProcSpector.Core.Plugins;
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
                _rowMenu.FillContextMenu(CtxMenu.Process, sender ?? this);
                CreateContextMenu(_rowMenu);
            }
            e.Row.ContextMenu = _rowMenu;
        }

        private void CreateContextMenu(ContextMenu menu)
        {
            var sys = Factory.Platform.Value.System;
            var f = sys.Flags;
            if (f.HasFlag(FeatureFlags.GetModules))
                menu.Items.Add(new MenuItem { Header = "Show modules", Command = GuiExt.Relay(OpenModuleView) });
            if (f.HasFlag(FeatureFlags.GetWindows))
                menu.Items.Add(new MenuItem { Header = "Show windows", Command = GuiExt.Relay(OpenHandleView) });
            if (f.HasFlag(FeatureFlags.GetMemory))
                menu.Items.Add(new MenuItem { Header = "Show memory", Command = GuiExt.Relay(OpenMemView) });
        }

        private void OpenModuleView()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            var modWind = new ModuleWindow { DataContext = new ModuleViewModel { Proc = proc } };
            modWind.ShowDialog(this);
        }

        private void OpenHandleView()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            var hdlWind = new HandleWindow { DataContext = new HandleViewModel { Proc = proc } };
            hdlWind.ShowDialog(this);
        }

        private void OpenMemView()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            var memWind = new MemoryWindow { DataContext = new MemoryViewModel { Proc = proc } };
            memWind.ShowDialog(this);
        }
    }
}