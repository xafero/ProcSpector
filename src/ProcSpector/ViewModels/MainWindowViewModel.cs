using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ProcSpector.Lib;

namespace ProcSpector.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<IProcess> _processes = [];
    }
}