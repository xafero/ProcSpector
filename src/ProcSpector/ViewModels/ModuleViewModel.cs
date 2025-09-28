using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ProcSpector.Lib;

namespace ProcSpector.ViewModels
{
    public partial class ModuleViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<IModule> _modules = [];

        [ObservableProperty] private IProcess? _proc;
    }
}