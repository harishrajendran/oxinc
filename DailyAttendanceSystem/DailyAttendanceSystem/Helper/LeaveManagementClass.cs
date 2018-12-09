using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;

namespace DailyAttendanceSystem.Helper
{
    public class LeaveManagementClass 
    {
        public void Execute(IJobExecutionContext context)
        {
            string classquery = "select id,Classes from classconfigs where isactive=1";
            var ClassConfig = Repository.executeclassconfigquery(classquery).ToArray();
            string sectionquery = "select id,sections from sectionconfigs where isactive=1";
            var SectionConfig = Repository.executesectionconfigquery(sectionquery).ToArray();
            for (int i = 0; i <= ClassConfig.Count(); i++)
            {
                for (int j = 0; j <= SectionConfig.Count(); j++)
                {
                    Repository.executeINsertOrUpdateSP("LeaveManagementForUnInformedLeave", new { classconfigId = ClassConfig[i].id, sectionConfigId= SectionConfig[j].id, CurrentDate = DateTime.Now });
                }
            }
        }
    }
}