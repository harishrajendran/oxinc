using Newtonsoft.Json;
using OxincNotification.Models;
using OxincNotification.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OxincNotification.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaveApprovalPage : ContentPage, INotifyPropertyChanged
    {
        public LeaveApproval LeaveApproval { get; set; }
        public LeaveApprovalPage(LeaveApproval leaveApproval)
        {
            InitializeComponent();
            LeaveApproval = leaveApproval;
            BindingContext = this;

            rollNumber.Text = leaveApproval.DisplayName;
            leaveDays.Text = leaveApproval.LeaveDays;
            leaveReason.Text = leaveApproval.LeaveReason;
            IsRejectionInvalid = false;
        }

        public bool IsRejectionInvalid { get; set; }

        private void ApproveClicked(object sender, SelectedItemChangedEventArgs e)
        {
            ApproveLeave();
            
            Navigation.PopAsync();
        }

        private void ApproveLeave()
        {
            var apiUrl = @"http://integratedappenv.ap-south-1.elasticbeanstalk.com/api/leaveapproval/updateleaverequest";
            LeaveApproval.ApprovalStatus = LeaveApprovalStatus.Approved;
            var input = new List<LeaveApproval>() { LeaveApproval };

            var inputJson = JsonConvert.SerializeObject(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var output = client.UploadString(apiUrl, "PUT", inputJson);
        }

        private void RejectClicked(object sender, SelectedItemChangedEventArgs e)
        {
            if (LeaveApproval.RejectionReason == null)
            {
                IsRejectionInvalid = true;
                OnPropertyChanged(nameof(IsRejectionInvalid));
                return;
            }

            IsRejectionInvalid = false;

            RejectLeave();
            Navigation.PopAsync();
        }

        private void RejectLeave()
        {
            var apiUrl = @"http://integratedappenv.ap-south-1.elasticbeanstalk.com/api/leaveapproval/updateleaverequest";
            LeaveApproval.RejectionReason = rejectionReason.Text;
            LeaveApproval.ApprovalStatus = LeaveApprovalStatus.Cancelled;
            var input = new List<LeaveApproval>() { LeaveApproval };

            var inputJson = JsonConvert.SerializeObject(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            var output = client.UploadString(apiUrl, "PUT", inputJson);
        }
    }
}