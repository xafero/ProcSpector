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
    public partial class ProcWindow : Window
    {
        public ProcWindow()
        {
            InitializeComponent();
        }

        private async Task LoadProcesses()
        {
            if (Sys1 is not { } sys) return;
            var f = sys.Flags;
            var usr = f.HasFlag(FeatureFlags.GetUserInfo)
                ? await Sys1.GetUserInfo()
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
            if (Sys1 is not { } sys) return;
            var f = sys.Flags;
            if (f.HasFlag(FeatureFlags.GetModules))
                menu.Items.Add(new MenuItem { Header = "Show modules", Command = GuiExt.Relay(OpenModuleView) });
            if (f.HasFlag(FeatureFlags.GetWindows))
                menu.Items.Add(new MenuItem { Header = "Show windows", Command = GuiExt.Relay(OpenHandleView) });
            if (f.HasFlag(FeatureFlags.GetMemory))
                menu.Items.Add(new MenuItem { Header = "Show memory", Command = GuiExt.Relay(OpenMemView) });
            if (f.HasFlag(FeatureFlags.KillTree))
                menu.Items.Add(new MenuItem { Header = "Kill tree", Command = GuiExt.Relay(KillTree) });
            if (f.HasFlag(FeatureFlags.OpenFolder))
                menu.Items.Add(new MenuItem { Header = "Open folder", Command = GuiExt.Relay(OpenFolder) });
            if (f.HasFlag(FeatureFlags.CopyScreen))
                menu.Items.Add(new MenuItem { Header = "Copy screen", Command = GuiExt.Relay(CopyScreen) });
            if (f.HasFlag(FeatureFlags.SaveMemory))
                menu.Items.Add(new MenuItem { Header = "Save memory", Command = GuiExt.Relay(SaveMemory) });
            if (f.HasFlag(FeatureFlags.DumpMini))
                menu.Items.Add(new MenuItem { Header = "Dump mini", Command = GuiExt.Relay(DumpMini) });
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

        private async Task SaveMemory()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            if (Sys2 != null && (await Sys2.CreateMemSave(proc)).Save() is { } file)
                ProcExt.OpenInShell(file);
        }

        private async Task KillTree()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            if (Sys1 != null)
                await Sys1.Kill(proc);
        }

        private async Task CopyScreen()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            if (Sys2 != null && (await Sys2.CreateScreenShot(proc)).Save() is { } file)
                ProcExt.OpenInShell(file);
        }

        private async Task DumpMini()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            if (Sys2 != null && (await Sys2.CreateMiniDump(proc)).Save() is { } file)
                ProcExt.OpenInShell(file);
        }

        private async Task OpenFolder()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            if (Sys3 != null)
                await Sys3.OpenFolder(proc);
        }

        private static ISystem1? Sys1 => Factory.Platform.Value.System1;
        private static ISystem2? Sys2 => Factory.Platform.Value.System2;
        private static ISystem3? Sys3 => Factory.Platform.Value.System3;
    }
}