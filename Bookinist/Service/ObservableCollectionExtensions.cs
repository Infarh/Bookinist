using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
    internal static class ObservableCollectionExtensions
    {
        public static void AddClear<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            collection.Add(items);
        }

        public static void Add<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }
    }
}
