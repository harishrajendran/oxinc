using Microsoft.VisualStudio.TestTools.UnitTesting;
using DailyAttendanceSystem.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyAttendanceSystem.Helper.Tests
{
    [TestClass()]
    public class DailyAttendanceMarkerTests
    {
        [TestMethod()]
        public void sendSMSTest()
        {
            DailyAttendanceMarker k = new DailyAttendanceMarker();
            k.MarkingSystem("12345678");
        }
    }
}