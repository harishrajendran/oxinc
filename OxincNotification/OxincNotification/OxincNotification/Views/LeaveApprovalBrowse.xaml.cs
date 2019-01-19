using Newtonsoft.Json;
using OxincNotification.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OxincNotification.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaveApprovalBrowse : ContentPage
    {
        public ObservableCollection<LeaveApproval> LeaveApprovals { get; set; }

        public LeaveApproval SelectedLeaveApproval { get; set; }

        public LeaveApprovalBrowse()
        {
            LeaveApprovals = new ObservableCollection<LeaveApproval>();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadLeaveApprovals();
            list.ItemsSource = LeaveApprovals;
        }

        public void LoadLeaveApprovals()
        {
            GetLeaveApprovals();
        }

        private void GetLeaveApprovals()
        {
            var apiUrl = @"http://integratedappenv.ap-south-1.elasticbeanstalk.com/api/leaveapproval/getvalueforbrowse";
            var input = new
            {
                userid = Application.Current.Properties["UserId"],
                approvalstatus = "Pending"
            };

            var inputJson = JsonConvert.SerializeObject(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(apiUrl, "POST", inputJson);
            LeaveApprovals = JsonConvert.DeserializeObject<ObservableCollection<LeaveApproval>>(json);
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var item = args.Item as LeaveApproval;
            if (item == null)
                return;

            await Navigation.PushAsync(new LeaveApprovalPage(item));

            //// Manually deselect item.
            list.SelectedItem = null;
        }
    }
}