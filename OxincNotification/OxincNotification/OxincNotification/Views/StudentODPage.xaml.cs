using Newtonsoft.Json;
using OxincNotification.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OxincNotification.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentODPage : ContentPage
    {
        private StudentOD _studentOD;

        public StudentODPage(ILoginManager loginManager, StudentOD studentOD)
        {
            InitializeComponent();
            if (studentOD == null)
                StudentOD = new StudentOD() { FromDate = DateTime.Now, ToDate = DateTime.Now.AddDays(1), IsActive = true };
            else
                StudentOD = studentOD;

            LoginManager = loginManager;

            BindingContext = this;
        }

        public ILoginManager LoginManager { get; set; }

        public string Validation { get; set; }

        public StudentOD StudentOD
        {
            get
            {
                return _studentOD;
            }
            set
            {
                _studentOD = value;
                OnPropertyChanged(nameof(StudentOD));
            }
        }

        private void SaveClicked(object sender, SelectedItemChangedEventArgs e)
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
            var apiUrl = @"http://integratedappenv.ap-south-1.elasticbeanstalk.com/api/StudentOD/PutStudentOD";
            ServicePointManager.DefaultConnectionLimit = 5;

            var inputJson = JsonConvert.SerializeObject(StudentOD);
            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                try
                {
                    var output = client.UploadString(apiUrl, "PUT", inputJson);
                }
                catch (WebException ex)
                {
                    new ValidationHelper().GetValidation(ex);
                }
            }
        }

        private void CreateOD()
        {
            var apiUrl = @"http://integratedappenv.ap-south-1.elasticbeanstalk.com/api/StudentOD/PostStudentOD";

            var inputJson = JsonConvert.SerializeObject(StudentOD);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            try
            {
                var output = client.UploadString(apiUrl, "Post", inputJson);
            }
            catch (Exception ex)
            {
                new ValidationHelper().GetValidation(ex as WebException);
            }
        }

    }
}