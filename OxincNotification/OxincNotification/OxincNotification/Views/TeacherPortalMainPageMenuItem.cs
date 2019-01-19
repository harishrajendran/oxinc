using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxincNotification.Views
{

    public class TeacherPortalMainPageMenuItem
    {
        public TeacherPortalMainPageMenuItem()
        {
            TargetType = typeof(TeacherPortalMainPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}