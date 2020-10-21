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
    public class DateTimeHelpersTests
    {
        [TestMethod()]
        public void CheckPrefixDefault_GetRelativeTimePassed_Test()
        {
            var result = DateTimeHelpers
                            .GetRelativeTimePassed(DateTime.UtcNow)
                            .Split(' ')[0];
            Assert.AreEqual("Posted", result);
        }

        [TestMethod()]
        public void CheckPrefixNonDefault_GetRelativeTimePassed_Test()
        {
            var result = DateTimeHelpers
                            .GetRelativeTimePassed(DateTime.UtcNow, "NewPrefix")
                            .Split(' ')[0];
            Assert.AreEqual("NewPrefix", result);
        }

        [TestMethod()]
        public void CheckReturnedStringForSeconds_GetRelativeTimePassed_Test()
        {
            var testDate = DateTime.UtcNow.AddSeconds(-2);
            var result = DateTimeHelpers
                            .GetRelativeTimePassed(testDate)
                            .Split(' ')[2];

            var expected = "seconds";

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CheckReturnedStringForMinutes_GetRelativeTimePassed_Test()
        {
            var testDate = DateTime.UtcNow.AddMinutes(-2);
            var result = DateTimeHelpers
                            .GetRelativeTimePassed(testDate)
                            .Split(' ')[2];

            var expected = "minutes";

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CheckReturnedStringForHours_GetRelativeTimePassed_Test()
        {
            var testDate = DateTime.UtcNow.AddHours(-2);
            var result = DateTimeHelpers
                            .GetRelativeTimePassed(testDate)
                            .Split(' ')[2];

            var expected = "hours";

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CheckReturnedStringForYesterday_GetRelativeTimePassed_Test()
        {
            var testDate = DateTime.UtcNow.AddHours(-24);
            var result = DateTimeHelpers
                            .GetRelativeTimePassed(testDate)
                            .Split(' ')[1];

            var expected = "yesterday";

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CheckReturnedStringForDays_GetRelativeTimePassed_Test()
        {
            var testDate = DateTime.UtcNow.AddDays(-2);
            var result = DateTimeHelpers
                            .GetRelativeTimePassed(testDate)
                            .Split(' ')[2];

            var expected = "days";

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CheckReturnedStringForMonths_GetRelativeTimePassed_Test()
        {
            var testDate = DateTime.UtcNow.AddMonths(-2);
            var result = DateTimeHelpers
                            .GetRelativeTimePassed(testDate)
                            .Split(' ')[2];

            var expected = "months";

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CheckReturnedStringForYears_GetRelativeTimePassed_Test()
        {
            var testDate = DateTime.UtcNow.AddYears(-2);
            var result = DateTimeHelpers
                            .GetRelativeTimePassed(testDate)
                            .Split(' ')[2];

            var expected = "years";

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CheckNullParameter_getLocalTime_Test()
        {
            var result = DateTimeHelpers.getLocalTime(null);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void CheckCorrectTime_getLocalTime_Test()
        {
            var result = DateTimeHelpers.getLocalTime(DateTime.UtcNow).Value;
            result = new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, 0);
            var expected = DateTime.Now;
            expected = new DateTime(expected.Year, expected.Month, expected.Day, expected.Hour, expected.Minute, 0);
            Assert.AreEqual(expected, result);
        }
    }
}