using Avalonia.Controls;
using Avalonia.Interactivity;
using ProcSpector.Lib;
using ProcSpector.Tools;
using ProcSpector.ViewModels;

namespace ProcSpector.Views
{
    public partial class ModuleWindow : Window
    {
        public ModuleWindow()
        {
            InitializeComponent();
        }

        private void LoadModules()
        {
            var sys = Defaults.System;

            var model = this.GetData<ModuleViewModel>();
            model.Modules.Clear();
            if (model.Proc is { } proc)
            {
                Title = $"The modules of {proc.Name} (pid: {proc.Id})";

                model.Modules.AddRange(sys.GetModules(proc));
            }
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            LoadModules();
        }

        private void RefreshClick(object? sender, RoutedEventArgs e)
        {
            LoadModules();
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
                _rowMenu.Items.Add(new MenuItem { Header = "Open folder", Command = GuiExt.Relay(OpenFolder) });
            }
            e.Row.ContextMenu = _rowMenu;
        }

        private void OpenFolder()
        {
            if (Grid.SelectedItem is not IModule mod)
                return;
            ProcExt.OpenFolder(mod);
        }
    }
}