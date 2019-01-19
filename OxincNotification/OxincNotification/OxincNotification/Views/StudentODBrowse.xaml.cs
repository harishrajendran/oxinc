using Newtonsoft.Json;
using OxincNotification.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OxincNotification.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentODBrowse : ContentPage
	{
		public StudentODBrowse ()
		{
			InitializeComponent ();
            StudentODs = new ObservableCollection<StudentOD>();
        }
        public ObservableCollection<StudentOD> StudentODs { get; set; }

        public StudentOD SelectedStudentOD { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadStudentODs();
            list.ItemsSource = StudentODs;
        }

        private void LoadStudentODs()
        {
            GetStudentODs();
        }

        private void GetStudentODs()
        {
            var apiUrl = @"http://integratedappenv.ap-south-1.elasticbeanstalk.com/api/StudentOD/GetStudentOD";
            var input = new
            {
                TeacherId = Application.Current.Properties["UserId"]
            };

            var inputJson = JsonConvert.SerializeObject(input);
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(apiUrl, "POST", inputJson);
            StudentODs = JsonConvert.DeserializeObject<ObservableCollection<StudentOD>>(json);
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var item = args.Item as StudentOD;
            if (item == null)
                return;

            await Navigation.PushAsync(new StudentODPage(null,item));

            //// Manually deselect item.
            list.SelectedItem = null;
        }
    }
}