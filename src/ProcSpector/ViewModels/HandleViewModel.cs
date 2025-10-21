using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ProcSpector.API;

namespace ProcSpector.ViewModels
{
    public partial class HandleViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<IWindow> _handles = [];

        [ObservableProperty] private IProcess? _proc;
    }
}