using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ProcSpector.API;
using ProcSpector.Core;
using ProcSpector.Impl;
using ProcSpector.Impl.Net;
using ProcSpector.Tools;
using ProcSpector.ViewModels;

// ReSharper disable AsyncVoidMethod

namespace ProcSpector.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task LoadProcesses()
        {
            var sys = Factory.Platform.Value.System;
            Title = $"All processes for {await sys.GetUserName()} on {await sys.GetHostName()}";

            var model = this.GetData<MainViewModel>();
            model.Processes.Clear();
            await foreach (var item in sys.GetAllProcesses())
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
                _rowMenu.Items.Add(new MenuItem { Header = "Kill tree", Command = GuiExt.Relay(KillTree) });
                _rowMenu.Items.Add(new MenuItem { Header = "Open folder", Command = GuiExt.Relay(OpenFolder) });
                _rowMenu.Items.Add(new MenuItem { Header = "Show modules", Command = GuiExt.Relay(OpenModuleView) });
                _rowMenu.Items.Add(new MenuItem { Header = "Show windows", Command = GuiExt.Relay(OpenHandleView) });
                _rowMenu.Items.Add(new MenuItem { Header = "Show memory", Command = GuiExt.Relay(OpenMemView) });
                _rowMenu.Items.Add(new MenuItem { Header = "Copy screen", Command = GuiExt.Relay(CopyScreen) });
                _rowMenu.Items.Add(new MenuItem { Header = "Save memory", Command = GuiExt.Relay(SaveMemory) });
                _rowMenu.Items.Add(new MenuItem { Header = "Dump mini", Command = GuiExt.Relay(DumpMini) });
            }
            e.Row.ContextMenu = _rowMenu;
        }

        private async Task SaveMemory()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            if ((await Sys.CreateMemSave(proc)).Save() is { } file)
                ProcExt.OpenInShell(file);
        }

        private async Task KillTree()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            await Sys.Kill(proc);
        }

        private async Task CopyScreen()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            if ((await Sys.CreateScreenShot(proc)).Save() is { } file)
                ProcExt.OpenInShell(file);
        }

        private async Task DumpMini()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            if ((await Sys.CreateMiniDump(proc)).Save() is { } file)
                ProcExt.OpenInShell(file);
        }

        private async Task OpenFolder()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            await Sys.OpenFolder(proc);
        }

        private void OpenMemView()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            var memWind = new MemoryWindow { DataContext = new MemoryViewModel { Proc = proc } };
            memWind.ShowDialog(this);
        }

        private void OpenHandleView()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            var hdlWind = new HandleWindow { DataContext = new HandleViewModel { Proc = proc } };
            hdlWind.ShowDialog(this);
        }

        private void OpenModuleView()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            var modWind = new ModuleWindow { DataContext = new ModuleViewModel { Proc = proc } };
            modWind.ShowDialog(this);
        }

        private static ISystem Sys => Factory.Platform.Value.System;
    }
}