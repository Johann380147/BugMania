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

        public static IEnumerable<T> ReorderChampionList<T>(IEnumerable<T> collection)
        {
            var order1 = new List<int> { 0 };
            var order2 = new List<int> { 1, 0 };
            var order3 = new List<int> { 2, 0, 1 };

            if (collection == null)
            {
                return null;
            }
            if (collection.Count() == 1)
            {
                return order1.Select(i => collection.ElementAt(i)).ToList();
            }
            else if (collection.Count() == 2)
            {
                return order2.Select(i => collection.ElementAt(i)).ToList();
            }
            else
            {
                return order3.Select(i => collection.ElementAt(i)).ToList();
            }
        }

        public static IEnumerable<string> GetCrownList<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return null;
            }
            if (collection.Count() == 1)
            {
                return new List<string>(){
                    "/Content/Images/gold.png"
                };
            }
            else if (collection.Count() == 2)
            {
                return new List<string>(){
                    "/Content/Images/silver.png",
                    "/Content/Images/gold.png",
                };
            }
            else
            {
                return new List<string>(){
                    "/Content/Images/bronze.png",
                    "/Content/Images/gold.png",
                    "/Content/Images/silver.png"
                };
            }
        }
    }

}