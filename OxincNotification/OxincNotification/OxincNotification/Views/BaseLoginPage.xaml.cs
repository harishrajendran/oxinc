using Newtonsoft.Json;
using OxincNotification.Models;
using System;
using System.ComponentModel;
using System.Net;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OxincNotification.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseLoginPage : ContentPage, INotifyPropertyChanged
    {
        public BaseLoginPage(ILoginManager loginManager)
        {
            InitializeComponent();
            LoginManager = loginManager;

            IsUserNameValid = true;
            IsPasswordValid = true;
            BindingContext = this;
        }

        public string IdentificationCode { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsUserNameValid { get; private set; }

        public bool IsPasswordValid { get; private set; }

        public bool InvalidLogin { get; set; }

        private static ILoginManager LoginManager;

        public void OnButtonClick(object sender, EventArgs e)
        {
            if (UserName == null)
            {
                IsUserNameValid = false;
                OnPropertyChanged(nameof(IsUserNameValid));
                return;
            }
            if (Password == null)
            {
                IsPasswordValid = false;
                OnPropertyChanged(nameof(IsPasswordValid));
                return;
            }
            if (ValidateCredentials(out bool isTeacherPortal))
                LoginManager.ShowMainPage(isTeacherPortal);
        }

        private bool ValidateCredentials(out bool isTeacherPortal)
        {
            if (!TryLoginUser(out UserLogin userDetails))
            {
                InvalidLogin = true;
                OnPropertyChanged(nameof(InvalidLogin));
                isTeacherPortal = false;
                return false;
            }

            //Call API to update login parameters and get required data
            Application.Current.Properties["IsLoggedIn"] = true;
            Application.Current.Properties["UserId"] = userDetails.Id;
            Application.Current.Properties["ClassConfigId"] = userDetails.classconfigid;
            Application.Current.Properties["SectionConfigId"] = userDetails.sectionconfigid;
            Application.Current.SavePropertiesAsync();
            isTeacherPortal = true;
            return true;
        }

        private bool TryLoginUser(out UserLogin userDetails)
        {
            var apiUrl = @"http://integratedappenv.ap-south-1.elasticbeanstalk.com/api/UserLogin/GetUserDetails";
            var input = new
            {
                LoginName = UserName,
                sentpassword = Password
            };

            var inputJson = JsonConvert.SerializeObject(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            try
            {
                string json = client.UploadString(apiUrl, "POST", inputJson);
                userDetails = JsonConvert.DeserializeObject<UserLogin>(json);
                return true;
            }
            catch(Exception ex)
            {
                userDetails = null;
                return false;
            }
        }
    }
}