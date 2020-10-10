using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugMania.Helpers
{
    public static class CollectionHelpers
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> newItems)
        {
            foreach (T item in newItems)
            {
                collection.Add(item);
            }
        }
    }

}