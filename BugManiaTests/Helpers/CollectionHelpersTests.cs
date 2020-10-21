using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugMania.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugMania.Helpers.Tests
{
    [TestClass()]
    public class CollectionHelpersTests
    {
        [TestMethod()]
        public void CheckNullCollectionParameter_AddRange_Test()
        {
            ICollection<string> list = null;
            List<string> newItems = new List<string> { "item" };

            list.AddRange(newItems);
            Assert.IsNull(list);
        }

        [TestMethod()]
        public void CheckNullNewItemsParameter_AddRange_Test()
        {
            ICollection<string> list = new List<string>();
            List<string> newItems = null;
            List<string> expected = new List<string>();

            list.AddRange(newItems);

            CollectionAssert.AreEqual(expected, (List<string>)list);
        }

        [TestMethod()]
        public void CheckNormal_AddRange_Test()
        {
            ICollection<string> list = new List<string> { "winter" };
            List<string> newItems = new List<string> { "summer", "spring" };
            List<string> expected = new List<string> { "winter", "summer", "spring" };

            list.AddRange(newItems);

            CollectionAssert.AreEqual(expected, (List<string>)list);
        }

        [TestMethod()]
        public void CheckNullParameter_ReorderChampionList_Test()
        {
            var list = CollectionHelpers.ReorderChampionList<string>(null);
            Assert.IsNull(list);
        }

        [TestMethod()]
        public void CheckReturnedOrderThreeItems_ReorderChampionList_Test()
        {
            var list = new List<int>() { 1, 2, 3 };
            list = (List<int>)CollectionHelpers.ReorderChampionList<int>(list);

            var expected = new List<int>() { 3, 1, 2 };
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod()]
        public void CheckReturnedOrderTwoItems_ReorderChampionList_Test()
        {
            var list = new List<int>() { 1, 2 };
            list = (List<int>)CollectionHelpers.ReorderChampionList<int>(list);

            var expected = new List<int>() { 2, 1};
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod()]
        public void CheckReturnedOrderOneItem_ReorderChampionList_Test()
        {
            var list = new List<int>() { 1 };
            list = (List<int>)CollectionHelpers.ReorderChampionList<int>(list);

            var expected = new List<int>() { 1 };
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod()]
        public void CheckNullParameter_GetCrownListTest()
        {
            List<int> list = null;
            var result = list.GetCrownList();
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void CheckReturnedListThreeItems_GetCrownList_Test()
        {
            List<int> list = new List<int> { 1, 2, 3 };
            var result = (List<string>)list.GetCrownList();
            var expected = new List<string>
            {
                "/Content/Images/bronze.png",
                "/Content/Images/gold.png",
                "/Content/Images/silver.png"
            };
            
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CheckReturnedListTwoItems_GetCrownList_Test()
        {
            List<int> list = new List<int> { 1, 2 };
            var result = (List<string>)list.GetCrownList();
            var expected = new List<string>
            {
                "/Content/Images/silver.png",
                "/Content/Images/gold.png"
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CheckReturnedListOneItem_GetCrownList_Test()
        {
            List<int> list = new List<int> { 1 };
            var result = (List<string>)list.GetCrownList();
            var expected = new List<string>
            {
                "/Content/Images/gold.png"
            };

            CollectionAssert.AreEqual(expected, result);
        }
    }
}