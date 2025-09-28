using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ProcSpector.Lib;

namespace ProcSpector.ViewModels
{
    public partial class HandleViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<IHandle> _handles = [];

        [ObservableProperty] private IProcess? _proc;
    }
}