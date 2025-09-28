using Avalonia.Controls;
using Avalonia.Interactivity;
using ProcSpector.Lib;
using ProcSpector.Tools;
using ProcSpector.ViewModels;

namespace ProcSpector.Views
{
    public partial class HandleWindow : Window
    {
        public HandleWindow()
        {
            InitializeComponent();
        }

        private void LoadHandles()
        {
            var sys = Defaults.System;

            var model = this.GetData<HandleViewModel>();
            model.Handles.Clear();
            if (model.Proc is { } proc)
            {
                Title = $"The windows of {proc.Name} (pid: {proc.Id})";

                model.Handles.AddRange(sys.GetHandles(proc));
            }
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            LoadHandles();
        }

        private void RefreshClick(object? sender, RoutedEventArgs e)
        {
            LoadHandles();
        }
    }
}