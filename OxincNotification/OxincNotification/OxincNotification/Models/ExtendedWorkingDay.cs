using System;
using Xamarin.Forms;

public class ExtendedWorkingDay
{
    public long? Id { get; set; }

    public string type { get; set; }

    public long? standardgroupid { get; set; }

    public long? academicyearid { get; set; }

    public string status { get; set; }

    public DateTime ExtendedWorkingDate { get; set; }
    
    public long? ClassConfigId { get; set; }

    public long? SectionConfigId { get; set; }

    public string DateLabel => ExtendedWorkingDate.ToString("dd MMM yyyy");
    public string BrowseLabel => $"{type} - {status}";
    public bool IsCancelled => status == "Cancelled";

    public long CreatedById => Convert.ToInt64(Application.Current.Properties["UserId"]);

    public bool IsActive => status == "Active";
}