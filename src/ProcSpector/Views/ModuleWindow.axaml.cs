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
    public partial class ModuleWindow : Window
    {
        public ModuleWindow()
        {
            InitializeComponent();
        }

        private async Task LoadModules()
        {
            var model = this.GetData<ModuleViewModel>();
            model.Modules.Clear();
            if (model.Proc is { } proc)
            {
                Title = $"The modules of {proc.Name} (pid: {proc.Id})";

                if (Sys1 != null)
                    await foreach (var item in Sys1.GetModules(proc))
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
                _rowMenu.FillContextMenu(CtxMenu.Module, sender ?? this);
                CreateContextMenu(_rowMenu);
            }
            e.Row.ContextMenu = _rowMenu;
        }

        private void CreateContextMenu(ContextMenu menu)
        {
            if (Sys3 != null)
                menu.Items.Add(new MenuItem { Header = "Open folder", Command = GuiExt.Relay(OpenFolder) });
        }

        private async Task OpenFolder()
        {
            if (Grid.SelectedItem is not IModule mod)
                return;
            if (Sys3 != null)
                await Sys3.OpenFolder(mod);
        }

        private static ISystem1? Sys1 => Factory.Platform.Value.System1;
        private static ISystem3? Sys3 => Factory.Platform.Value.System3;
    }
}