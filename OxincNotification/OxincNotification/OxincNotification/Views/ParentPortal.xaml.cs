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
    public partial class ParentPortal : MasterDetailPage
    {
        private static ILoginManager LoginManager;
        public List<MasterPageItem> MenuList { get; set; }

        public ParentPortal()
        {
            InitializeComponent();
            MenuList = new List<MasterPageItem>();

            var homePage = new MasterPageItem() { Title = "Home", Icon = "home.png", TargetType = typeof(HomePage) };
            var leaveApproval = new MasterPageItem() { Title = "Approve Leaves", Icon = "leaveapproval.png", TargetType = typeof(LeaveApprovalBrowse) };
            var logOut = new MasterPageItem() { Title = "Log Out", Icon = "logout.png", TargetType = typeof(int) };

            // Adding menu items to menuList
            MenuList.Add(homePage);
            MenuList.Add(leaveApproval);
            MenuList.Add(logOut);

            //navigationDrawerList.ItemsSource = MenuList;

            //LoginManager = loginManager;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as ParentPortalMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}