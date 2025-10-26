using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ProcSpector.API;
using ProcSpector.Core;
using ProcSpector.Core.Plugins;
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
            var model = this.GetData<HandleViewModel>();
            model.Handles.Clear();
            if (model.Proc is { } proc)
            {
                Title = $"The windows of {proc.Name} (pid: {proc.Id})";

                if (Sys2 != null)
                    await foreach (var item in Sys2.GetHandles(proc))
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
            if (Sys2 != null)
                menu.Items.Add(new MenuItem { Header = "Activate", Command = GuiExt.Relay(ActivateIt) });
            if (Sys2 != null)
                menu.Items.Add(new MenuItem { Header = "Copy screen", Command = GuiExt.Relay(CopyScreen) });
            if (Sys2 != null)
                menu.Items.Add(new MenuItem { Header = "Set mouse", Command = GuiExt.Relay(SetMouse) });
        }

        private async Task ActivateIt()
        {
            if (Grid.SelectedItem is not IHandle handle)
                return;
            if (Sys2 != null)
                await Sys2.Activate(handle);
        }

        private async Task SetMouse()
        {
            if (Grid.SelectedItem is not IHandle handle)
                return;
            if (Sys2 != null)
                await Sys2.SetMouse(handle, 25, 45);
        }

        private async Task CopyScreen()
        {
            if (Grid.SelectedItem is not IHandle handle)
                return;
            if (Sys2 != null && (await Sys2.CreateScreenShot(handle)).Save() is { } file)
                FileExt.OpenInShell(file);
        }

        private static ISystem2? Sys2 => Factory.Platform.Value.System2;
    }
}