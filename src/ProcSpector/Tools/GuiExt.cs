using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using CommunityToolkit.Mvvm.Input;

namespace ProcSpector.Tools
{
    public static class GuiExt
    {
        public static T? GetDataRaw<T>(this StyledElement item)
        {
            if (item.DataContext is T model)
                return model;

            return default;
        }

        public static T GetData<T>(this StyledElement item)
            where T : new()
        {
            if (item.DataContext is T model)
                return model;

            var created = new T();
            item.DataContext = created;
            return created;
        }

        public static void AddRange<T>(this ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Add(item);
            }
        }

        public static ICommand Relay(Action action)
        {
            var cmd = new RelayCommand(action);
            return cmd;
        }

        public static ICommand Relay(Func<Task> func)
        {
            var cmd = new AsyncRelayCommand(func);
            return cmd;
        }
    }
}