using System;
using System.Collections.Generic;
using System.Text;

namespace OxincNotification.Models
{
    public class DailyAttendance
    {

        public class AttendanceReportModel
        {
            public long? ClassConfigId { get; set; }
            public long? SectionConfigId { get; set; }
            public int? FromYear { get; set; }
            public int? ToYear { get; set; }
            public AttendanceReportTypeValues Filter { get; set; }
            public DateTime? Date { get; set; }
            public MonthValues Month { get; set; }
            public string StudentRollNumber { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }

            public int NumberOfStudentsPresent { get; set; }
            public int NumberOfAbsentees { get; set; }
            public List<DailyAttendanceDetail> DailyAttendanceDetails { get; set; }

            public List<PeriodicReportDetail> PeriodicReportDetails { get; set; }

            public List<LeaveReportDetail> LeaveReportDetails { get; set; }

            public AttendanceReportModel()
            {
                DailyAttendanceDetails = new List<DailyAttendanceDetail>();
                PeriodicReportDetails = new List<PeriodicReportDetail>();
                LeaveReportDetails = new List<LeaveReportDetail>();
            }
        }

        public class DailyAttendanceDetail
        {
            public long Id { get; set; } // Used For Update
            public string StudentRollNumber { get; set; }
            public string StudentName { get; set; }
            public Gender Gender { get; set; }
            public bool FN { get; set; }
            public bool AN { get; set; }
        }

        public class PeriodicReportDetail
        {
            public string StudentRollNumber { get; set; }
            public string StudentName { get; set; }
            public Gender Gender { get; set; }
            public int NumberOfWorkingDays { get; set; }
            public int NumberOfDaysPresent { get; set; }
            public int NumberOfDaysAbsent { get; set; }
        }

        public class LeaveReportDetail
        {
            public string StudentRollNumber { get; set; }
            public string StudentName { get; set; }
            public Gender Gender { get; set; }
            public DateTime? LeaveDate { get; set; }
            public bool IsFullDayLeave { get; set; }
            public bool IsHalfDayLeave { get; set; }
        }
        public enum Gender
        {
            Male,
            Female
        }

        public enum AttendanceReportTypeValues
        {
            DailyAttendance,
            MonthlyReport,
            YearlyReport,
            PeriodicReport,
            LeaveReport
        }

        public enum MonthValues
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }
    }
}
