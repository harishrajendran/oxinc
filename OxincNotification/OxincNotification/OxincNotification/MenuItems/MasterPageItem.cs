using System;
using System.Collections.Generic;
using System.Text;

namespace OxincNotification.MenuItems
{
    public class MasterPageItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool IsCreateEnabled { get; set; }
        public Type TargetType { get; set; }
    }
}
