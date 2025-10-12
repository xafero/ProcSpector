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
    public partial class ModuleWindow : Window
    {
        public ModuleWindow()
        {
            InitializeComponent();
        }

        private async Task LoadModules()
        {
            var sys = Factory.Platform.Value.System;

            var model = this.GetData<ModuleViewModel>();
            model.Modules.Clear();
            if (model.Proc is { } proc)
            {
                Title = $"The modules of {proc.Name} (pid: {proc.Id})";

                await foreach (var item in sys.GetModules(proc))
                    model.Modules.Add(item);
            }
        }

        private async void OnLoaded(object? sender, RoutedEventArgs e)
        {
            await LoadModules();
        }

        private async void RefreshClick(object? sender, RoutedEventArgs e)
        {
            await LoadModules();
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

        private async Task OpenFolder()
        {
            if (Grid.SelectedItem is not IModule mod)
                return;
            await Sys.OpenFolder(mod);
        }

        private static ISystem Sys => Factory.Platform.Value.System;
    }
}