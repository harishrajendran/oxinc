using Newtonsoft.Json;
using OxincNotification.Models;
using System;
using System.Net;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OxincNotification.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtendedWorkingDayPage : ContentPage
    {
        public ExtendedWorkingDayPage(ILoginManager loginManager, ExtendedWorkingDay extendedWorkingDay)
        {
            InitializeComponent();
            if (extendedWorkingDay == null)
            {
                ExtendedWorkingDay = new ExtendedWorkingDay()
                {
                    ExtendedWorkingDate = DateTime.Now,
                    ClassConfigId = Convert.ToInt64(Application.Current.Properties["ClassConfigId"]),
                    SectionConfigId = Convert.ToInt64(Application.Current.Properties["SectionConfigId"]),
                    academicyearid = 1,
                    standardgroupid = 1,
                    type = "SpecialClass",
                    status = "Pending"
                };
                IsCreate = true;
            }
            else
            {
                ExtendedWorkingDay = extendedWorkingDay;
                ShouldEnableFields = false;
            }

            LoginManager = loginManager;

            BindingContext = this;
        }

        public ExtendedWorkingDay ExtendedWorkingDay
        {
            get
            {
                return _extendedWorkingDay;
            }
            set
            {
                _extendedWorkingDay = value;
                OnPropertyChanged(nameof(ExtendedWorkingDay));
            }
        }

        public bool IsCreate { get; private set; }
        public bool ShouldEnableFields { get; private set; }

        public bool IsCancelEnabled => !ShouldEnableFields && !ExtendedWorkingDay.IsCancelled;

        private ExtendedWorkingDay _extendedWorkingDay;

        public ILoginManager LoginManager { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }


        private void SaveClicked(object sender, EventArgs e)
        {
            if (LoginManager != null)
            {
                CreateOD();
                LoginManager.ShowMainPage(true);
                return;
            }
            else
            {
                SaveOD();
                Navigation.PopAsync();
            }
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            if (LoginManager != null)
            {
                CreateOD();
                LoginManager.ShowMainPage(true);
                return;
            }
            else
            {
                SaveOD();
                Navigation.PopAsync();
            }
        }

        private void SaveOD()
        {
            var apiUrl = @"http://integratedappenv.ap-south-1.elasticbeanstalk.com/api/ExtendedWorkingDay/UpdateExtendedWorkingDay";

            ExtendedWorkingDay.ClassConfigId = Convert.ToInt64(Application.Current.Properties["ClassConfigId"]);
            ExtendedWorkingDay.SectionConfigId = Convert.ToInt64(Application.Current.Properties["SectionConfigId"]);
            ExtendedWorkingDay.academicyearid = 1;
            ExtendedWorkingDay.standardgroupid = 1;
            ExtendedWorkingDay.status = "Cancelled";

            var inputJson = JsonConvert.SerializeObject(ExtendedWorkingDay);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            try
            {
                var output = client.UploadString(apiUrl, "PUT", inputJson);
            }
            catch (WebException ex)
            {
                new  ValidationHelper().GetValidation(ex);
            }
        }

        private void CreateOD()
        {
            var apiUrl = @"http://integratedappenv.ap-south-1.elasticbeanstalk.com/api/ExtendedWorkingDay/InsertExtendedWorkingDay";

            var inputJson = JsonConvert.SerializeObject(ExtendedWorkingDay);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            try
            {
                var output = client.UploadString(apiUrl, "Post", inputJson);
            }
            catch(WebException ex)
            {
                new ValidationHelper().GetValidation(ex);
            }
        }
    }
}