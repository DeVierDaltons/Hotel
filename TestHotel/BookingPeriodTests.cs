using Hotel.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHotel
{
    [TestClass]
    public class BookingPeriodTests
    {
        [TestMethod]
        public void NonOverlappingPeriods()
        {
            var period1 = new BookingPeriod(new DateTime(1992, 1, 1), new DateTime(1992, 2, 2));
            var period2 = new BookingPeriod(new DateTime(2006, 1, 1), new DateTime(2006, 2, 2));
            Assert.IsFalse(period1.OverlapsWith(period2));
            Assert.IsTrue(period1.DoesNotoverlapWith(period2));
        }

        [TestMethod]
        public void OverlappingPeriods()
        {
            var period1 = new BookingPeriod(new DateTime(1992, 1, 1), new DateTime(1992, 2, 2));
            var period2 = new BookingPeriod(new DateTime(1992, 1, 1), new DateTime(1992, 2, 2));
            Assert.IsTrue(period1.OverlapsWith(period2));
            Assert.IsFalse(period1.DoesNotoverlapWith(period2));
        }

        [TestMethod]
        public void SucceedingPeriod()
        {
            var period1 = new BookingPeriod(new DateTime(1992, 1, 1), new DateTime(1992, 2, 2));
            var period2 = new BookingPeriod(new DateTime(1992, 2, 3), new DateTime(2006, 2, 2));
            Assert.IsFalse(period1.OverlapsWith(period2));
            Assert.IsTrue(period1.DoesNotoverlapWith(period2));
        }

        [TestMethod]
        public void PrecedingPeriod()
        {
            var period1 = new BookingPeriod(new DateTime(1992, 2, 3), new DateTime(2006, 2, 2));
            var period2 = new BookingPeriod(new DateTime(1992, 1, 1), new DateTime(1992, 2, 2));
            Assert.IsFalse(period1.OverlapsWith(period2));
            Assert.IsTrue(period1.DoesNotoverlapWith(period2));
        }
    }
}
