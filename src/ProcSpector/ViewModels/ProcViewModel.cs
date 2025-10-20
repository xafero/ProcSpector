using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ProcSpector.API;

namespace ProcSpector.ViewModels
{
    public partial class ProcViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<IProcess> _processes = [];
    }
}