using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyAttendanceSystem.Models
{
    public class DailyAttendance
    {
        internal long StudentId { get; set; }
        internal bool ForeNoon { get{return false; } }
        internal bool AfterNoon { get { return false; } }
        internal int TotalDayCounter { get; set; }
        internal bool HalfDayLeave { get; set; }
        internal bool FullDayLeave { get; set; }
        internal DateTime DailyDate { get { return DateTime.Now; } }
        internal bool FullDayPresent { get; set; }
        internal long CreatedById { get { return 1; } }
        internal DateTime CreatedByTime { get { return DateTime.Now; } }
        internal long? UpdatedById { get { return 1; } }
        internal DateTime? UpdatedByTime { get { return DateTime.Now; } }
    }
    public class StudentDetail
    {
        public ulong id {get;set;}
        public string EmergencyContactNumber { get; set; }
        public string fullname { get; set; }

    }
    public class DailyAttendanceDetail
    {
        public long id { get; set; }
        public long studentid { get; set; }

    }
    public class ClassConfig
    {
        internal long id { get; set; }
        internal string Classes { get; set; }
    }
    public class sectionconfig
    {
        internal long id { get; set; }
        internal string sections { get; set; }
    }
}