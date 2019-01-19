using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OxincNotification.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherPortalMainPageMaster : ContentPage
    {
        public ListView ListView;

        public TeacherPortalMainPageMaster()
        {
            InitializeComponent();

            BindingContext = new TeacherPortalMainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class TeacherPortalMainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<TeacherPortalMainPageMenuItem> MenuItems { get; set; }
            
            public TeacherPortalMainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<TeacherPortalMainPageMenuItem>(new[]
                {
                    new TeacherPortalMainPageMenuItem { Id = 0, Title = "Page 1" },
                    new TeacherPortalMainPageMenuItem { Id = 1, Title = "Page 2" },
                    new TeacherPortalMainPageMenuItem { Id = 2, Title = "Page 3" },
                    new TeacherPortalMainPageMenuItem { Id = 3, Title = "Page 4" },
                    new TeacherPortalMainPageMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}