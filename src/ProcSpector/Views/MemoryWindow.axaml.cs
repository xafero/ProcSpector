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
    public partial class MemoryWindow : Window
    {
        public MemoryWindow()
        {
            InitializeComponent();
        }

        private async Task LoadRegions()
        {
            var model = this.GetData<MemoryViewModel>();
            model.Regions.Clear();
            if (model.Proc is { } proc)
            {
                Title = $"The memory of {proc.Name} (pid: {proc.Id})";

                if (Sys2 != null)
                    await foreach (var item in Sys2.GetRegions(proc))
                        model.Regions.Add(item);
            }
        }

        private async void OnLoaded(object? sender, RoutedEventArgs e)
        {
            await LoadRegions();
        }

        private async void RefreshClick(object? sender, RoutedEventArgs e)
        {
            await LoadRegions();
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
                _rowMenu.FillContextMenu(CtxMenu.Memory, sender ?? this);
                CreateContextMenu(_rowMenu);
            }
            e.Row.ContextMenu = _rowMenu;
        }

        private void CreateContextMenu(ContextMenu menu)
        {
            if (Sys2 != null)
                menu.Items.Add(new MenuItem { Header = "Save memory", Command = GuiExt.Relay(SaveMemory) });
        }

        private async Task SaveMemory()
        {
            if (Grid.SelectedItem is not IMemRegion region)
                return;
            if (Sys2 != null && (await Sys2.CreateMemSave(region)).Save() is { } file)
                FileExt.OpenInShell(file);
        }

        private static ISystem2? Sys2 => Factory.Platform.Value.System2;
    }
}