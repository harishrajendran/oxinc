using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DailyAttendanceSystem.Models;
using System.Net;
using System.Collections.Specialized;

namespace DailyAttendanceSystem.Helper
{
    public class DailyAttendanceMarker
    {
        public void MarkingSystem(string RFIDNumber)
        {
            string query = "select id,EmergencyContactNumber,fullname from student where RFIDNumber = '" + RFIDNumber + "'";
            var EmergencyContactNumber_FullName_studentid = Repository.executeStudentDetailquery(query).Select(x => new { x.EmergencyContactNumber, x.fullname, x.id }).FirstOrDefault();



            string RFIDquery = "select id,StudentId from dailyattendance where StudentId = '" + EmergencyContactNumber_FullName_studentid.id + "' && DailyDate='" +DateTime.Now+"'";
            IEnumerable<DailyAttendanceDetail> RFIDresult = Repository.executeRFIDQueryquery(RFIDquery);
            int DailyAttendance_Count = RFIDresult.Select(x => x.id).Count();
            if (DateTime.Now.Hour > 8 && (DateTime.Now.Hour <= 10 && DateTime.Now.Minute <= 30) && DailyAttendance_Count == 0)
            {
                string message = HttpUtility.UrlEncode(EmergencyContactNumber_FullName_studentid.fullname + "has entered the school at" + DateTime.Now);
                sendSMS(EmergencyContactNumber_FullName_studentid.EmergencyContactNumber, message);
                Repository.executeINsertOrUpdateSP("FullDayPresentMarking", new { studentid = EmergencyContactNumber_FullName_studentid.id, CurrentDate = DateTime.Now });
                Repository.executeINsertOrUpdateSP("CancellingLeave", new { studentid = EmergencyContactNumber_FullName_studentid.id, CurrentDate = DateTime.Now });
            }
            else if ((DateTime.Now.Hour >= 12 && DateTime.Now.Minute >=30) && DateTime.Now.Hour < 14 && DailyAttendance_Count == 0)
            {
                string message = HttpUtility.UrlEncode(EmergencyContactNumber_FullName_studentid.fullname + "has entered the school at" + DateTime.Now);
                sendSMS(EmergencyContactNumber_FullName_studentid.EmergencyContactNumber, message);
                Repository.executeINsertOrUpdateSP("HalfDayPresentMarkingForAfternoon", new { studentid = EmergencyContactNumber_FullName_studentid.id, CurrentDate = DateTime.Now });
                Repository.executeINsertOrUpdateSP("CancellingLeave", new { studentid = EmergencyContactNumber_FullName_studentid.id, CurrentDate = DateTime.Now });
            }
            else if ((DateTime.Now.Hour > 11 && DateTime.Now.Minute > 30) && DateTime.Now.Hour < 14 && DailyAttendance_Count == 1)
            {
                string message = HttpUtility.UrlEncode(EmergencyContactNumber_FullName_studentid.fullname + " has left the school at" + DateTime.Now);
                sendSMS(EmergencyContactNumber_FullName_studentid.EmergencyContactNumber, message);
                Repository.executeINsertOrUpdateSP("UpdateHalfDayLeave_DailyAttendance", new { dailyattendanceid = RFIDresult.Select(x => x.id).FirstOrDefault() });
                Repository.executeINsertOrUpdateSP("CancellingLeave", new { studentid = EmergencyContactNumber_FullName_studentid.id, CurrentDate = DateTime.Now });
            }
            else if (DateTime.Now.Hour >= 14  && DateTime.Now.Hour <= 18 && DailyAttendance_Count == 1)
            {
                string message = HttpUtility.UrlEncode(EmergencyContactNumber_FullName_studentid.fullname + " has left the school at" + DateTime.Now);
                sendSMS(EmergencyContactNumber_FullName_studentid.EmergencyContactNumber, message);
            }

        }

        public void sendSMS(string EmergencyContactNumber,string message)
        {
            
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "uXSeD8JwJQo-0T2nYKb3yc20Lqg7Kk8pfShyFjOugU"},
                {"numbers" , EmergencyContactNumber},
                {"message" , message},
                {"sender" , "TXTLCL"}
                });
               
            }
        }
    }
}