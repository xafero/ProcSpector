using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using ProcSpector.Core.Plugins;

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

        public static ICommand Relay(this EventHandler<object> handler, object arg, Func<object, object> get)
        {
            var cmd = Relay(DoExecTask);
            return cmd;

            Task DoExecTask()
            {
                return Task.Run(DoExecute);
            }

            void DoExecute()
            {
                var obj = get(arg);
                handler(arg, obj);
            }
        }

        public static ICommand Relay(this Action action)
        {
            var cmd = new RelayCommand(action);
            return cmd;
        }

        public static ICommand Relay(this Func<Task> func)
        {
            var cmd = new AsyncRelayCommand(func);
            return cmd;
        }

        public static void FillContextMenu(this ContextMenu rowMenu, CtxMenu menu, object sender)
        {
            var cm = PluginTool.Context.ContextMenu;
            var ri = rowMenu.Items;
            if (!cm.TryGetValue(menu, out var items))
                return;
            foreach (var act in items)
            {
                var mi = new MenuItem
                {
                    Header = act.Title, Command = Relay(act.Handler, sender, GetSelected)
                };
                ri.Add(mi);
            }
        }

        private static object GetSelected(object arg)
        {
            var grid = (DataGrid)arg;
            var item = grid.SelectedItem;
            return item;
        }
    }
}