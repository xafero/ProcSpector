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
            }
            e.Row.ContextMenu = _rowMenu;
        }

        private void KillTree()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            ProcExt.Kill(proc);
        }

        private void CopyScreen()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            ProcExt.CreateScreenShot(proc);
        }

        private void OpenFolder()
        {
            if (Grid.SelectedItem is not IProcess proc)
                return;
            ProcExt.OpenFolder(proc);
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
    }
}