using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OxincNotification.Services;
using OxincNotification.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OxincNotification
{
    public partial class App : Application, ILoginManager
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;
        public static new App Current;

        public App()
        {
            Current = this;
            InitializeComponent();


            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;

            if (isLoggedIn)
                MainPage = new TeacherPortalMainPage(this);
            else
                MainPage = new BaseLoginPage(this);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void ShowMainPage(bool isTeacherPortal)
        {
            if (isTeacherPortal)
                MainPage = new TeacherPortalMainPage(this);
        }

        public void Logout()
        {
            Properties["IsLoggedIn"] = false; // only gets set to 'true' on the LoginPage
            MainPage = new BaseLoginPage(this);
        }
    }
}
