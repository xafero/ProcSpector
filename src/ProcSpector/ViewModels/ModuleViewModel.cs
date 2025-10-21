using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ProcSpector.API;

namespace ProcSpector.ViewModels
{
    public partial class ModuleViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<IProcess> _processes = [];

        [ObservableProperty] private IProcess? _proc;
    }
}