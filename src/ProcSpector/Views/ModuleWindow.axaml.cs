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
    }
}