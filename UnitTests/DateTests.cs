using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.Model;

namespace UnitTests
{
    [TestClass]
    public class DateTests
    { 
        [TestMethod]
        public void CheckForObjCreationAndInitialization()
        {
            var date = new Hotel.Model.Date(21, 12, 2017);
        }
    }
}
