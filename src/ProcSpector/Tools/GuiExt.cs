using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;

namespace ProcSpector.Tools
{
    public static class GuiExt
    {
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
    }
}