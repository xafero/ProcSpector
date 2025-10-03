using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ProcSpector.Lib;
using ProcSpector.Lib.Memory;

namespace ProcSpector.ViewModels
{
    public partial class MemoryViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<IMemRegion> _regions = [];

        [ObservableProperty] private IProcess? _proc;
    }
}