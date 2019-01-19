using OxincNotification.MenuItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OxincNotification.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherPortalMainPage : MasterDetailPage
    {
        public interface IAndroidMethods
        {
            void CloseApp();
        }

        private static ILoginManager LoginManager;
        public List<MasterPageItem> MenuList { get; set; }

        public TeacherPortalMainPage(ILoginManager loginManager)
        {
            InitializeComponent();
            MenuList = new List<MasterPageItem>();

            var homePage = new MasterPageItem() { Title = "Home", Icon = "home.png", TargetType = typeof(HomePage) };
            var leaveApproval = new MasterPageItem() { Title = "Approve Leaves", Icon = "leaveapproval.png", TargetType = typeof(LeaveApprovalBrowse) };
            var onDuty = new MasterPageItem() { Title = "Register On Duty", Icon = "leaveapproval.png", IsCreateEnabled=true, TargetType = typeof(StudentODBrowse) };
            var extendedWorkingDay = new MasterPageItem() { Title = "Register Working Day", Icon = "workingDay.png", IsCreateEnabled=true, TargetType = typeof(ExtendedWorkingDayBrowse) };
            var dailyAttendance = new MasterPageItem() { Title = "Daily Attendance", Icon = "meeting.png", TargetType = typeof(DailyAttendanceFilterPage) };
            //var meeting = new MasterPageItem() { Title = "Schedule Meetings", Icon = "meeting.png", TargetType = typeof(ScheduleMeetingPage) };
            //var homeWork = new MasterPageItem() { Title = "Home Work", Icon = "homework.png", TargetType = typeof(HomeworkPage) };
            var logOut = new MasterPageItem() { Title = "Log Out", Icon = "logout.png", TargetType = typeof(int) };

            // Adding menu items to menuList
            MenuList.Add(homePage);
            MenuList.Add(leaveApproval);
            MenuList.Add(onDuty);
            MenuList.Add(extendedWorkingDay);
            //MenuList.Add(meeting);
            //MenuList.Add(homeWork);
            MenuList.Add(logOut);

            navigationDrawerList.ItemsSource = MenuList;

            LoginManager = loginManager;
            //TeacherPortalMain.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;

            if (item == null) return;

            if (item.Title == "Log Out")
            {
                Application.Current.Properties["IsLoggedIn"] = false;
                Application.Current.SavePropertiesAsync();
                LoginManager.Logout();
                return;
            }
            
            Type page = item.TargetType;

            if(item.TargetType == typeof(ExtendedWorkingDayPage))
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(page, LoginManager));
            }
            else
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            }
            navigationDrawerList.SelectedItem = null;
            IsPresented = false;
        }

        

        public void CreateClicked(object sender, EventArgs e)
        {
            var item = ((sender as Button).CommandParameter) as MasterPageItem;
            Type page = (item.TargetType == typeof(ExtendedWorkingDayBrowse)) ? typeof(ExtendedWorkingDayPage): typeof(StudentODPage);
            Detail = new NavigationPage((Page)Activator.CreateInstance(page, LoginManager, null));
            IsPresented = false;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as TeacherPortalMainPageMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            if (item.Title == "Page 5")
            {
                Application.Current.Properties["IsLoggedIn"] = false;
                Application.Current.SavePropertiesAsync();
                LoginManager.Logout();
            }

            Detail = new NavigationPage(page);
            IsPresented = false;

            //TeacherPortalMain.ListView.SelectedItem = null;
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    if (Device.RuntimePlatform == Device.Android)
        //        DependencyService.Get<IAndroidMethods>().CloseApp();

        //    return base.OnBackButtonPressed();
        //}
    }
}