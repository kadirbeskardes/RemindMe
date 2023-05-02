using RemindMe.Classes;
using System;
using System.Data;
using System.Numerics;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace RemindMe
{
    public partial class DoctorMainPage : FlyoutPage
    {
        public DoctorMainPage()
        {
            InitializeComponent();
            DoctorFlyout.DoctorFlyList.ItemSelected += OnSelectedItem;
        }
        public DoctorMainPage(string tckno)
        {
            Preferences.Set("logged", true);
            Preferences.Set("myid", tckno);
            Preferences.Set("role", "doc");
            InitializeComponent();
            DoctorFlyout.DoctorFlyList.ItemSelected += OnSelectedItem;
        }
        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutPageItems;
            if (item != null)
            {
                if (item.Title == "Çıkış Yap")
                {
                    Preferences.Set("logged", false);
                    Preferences.Remove("myid");
                    Preferences.Remove("role", "doc");
                    App.Current.MainPage = new RootPage();
                    return;
                }
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
                DoctorFlyout.DoctorFlyList.SelectedItem = item;
                IsPresented = false;
            }
        }
    }
}
