using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ProcSpector.Lib;

namespace ProcSpector.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            var sys = Defaults.System;
            foreach (var sp in sys.Processes)
            {
                Debug.WriteLine(sp);
            }
        }
    }
}