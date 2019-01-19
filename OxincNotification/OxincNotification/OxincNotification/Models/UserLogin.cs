namespace OxincNotification.Models
{
    public class UserLogin
    {
        public long Id { get; set; }
        public string LoginName { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string sentpassword { get; set; }
        public long? sectionconfigid { get; set; }
        public long? classconfigid { get; set; }
        public string email { get; set; }
        public bool? isclassteacher { get; set; }
    }
}