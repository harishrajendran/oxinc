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
	public partial class ExtendedWorkingDayBrowse : ContentPage
	{
        public ExtendedWorkingDayBrowse()
        {
            InitializeComponent();
            ExtendedWorkingDays = new ObservableCollection<ExtendedWorkingDay>();
        }
        public ObservableCollection<ExtendedWorkingDay> ExtendedWorkingDays { get; set; }

        public ExtendedWorkingDayPage SelectedExtendedWorkingDay { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadExtendedWorkingDays();
            list.ItemsSource = ExtendedWorkingDays;
        }

        private void LoadExtendedWorkingDays()
        {
            GetExtendedWorkingDays();
        }

        private void GetExtendedWorkingDays()
        {
            var apiUrl = @"http://integratedappenv.ap-south-1.elasticbeanstalk.com/api/ExtendedWorkingDay/GetValueForBrowse";
            var input = new
            {
                ClassConfigId = Application.Current.Properties["ClassConfigId"],
                SectionConfigId = Application.Current.Properties["SectionConfigId"],
                StandardGroupId = 1,
                Type="SpecialClass"
            };

            var inputJson = JsonConvert.SerializeObject(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(apiUrl, "POST", inputJson);
            ExtendedWorkingDays = JsonConvert.DeserializeObject<ObservableCollection<ExtendedWorkingDay>>(json);
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var item = args.Item as ExtendedWorkingDay;
            if (item == null)
                return;

            await Navigation.PushAsync(new ExtendedWorkingDayPage(null, item));

            //// Manually deselect item.
            list.SelectedItem = null;
        }
    }
}