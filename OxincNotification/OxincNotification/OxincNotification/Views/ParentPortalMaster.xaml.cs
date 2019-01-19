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
    public partial class ParentPortalMaster : ContentPage
    {
        public ListView ListView;

        public ParentPortalMaster()
        {
            InitializeComponent();

            BindingContext = new ParentPortalMasterViewModel();
            ListView = MenuItemsListView;
        }

        class ParentPortalMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<ParentPortalMenuItem> MenuItems { get; set; }
            
            public ParentPortalMasterViewModel()
            {
                MenuItems = new ObservableCollection<ParentPortalMenuItem>(new[]
                {
                    new ParentPortalMenuItem { Id = 0, Title = "Page 1" },
                    new ParentPortalMenuItem { Id = 1, Title = "Page 2" },
                    new ParentPortalMenuItem { Id = 2, Title = "Page 3" },
                    new ParentPortalMenuItem { Id = 3, Title = "Page 4" },
                    new ParentPortalMenuItem { Id = 4, Title = "Page 5" },
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