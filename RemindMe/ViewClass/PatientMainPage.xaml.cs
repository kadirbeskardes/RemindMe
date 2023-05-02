using RemindMe.Classes;
using Plugin.LocalNotification;
using System;
using System.Globalization;
using System.Numerics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientMainPage : FlyoutPage
    {
        public PatientMainPage()
        {
            InitializeComponent();
            PatientFlyout.PatientFlyList.ItemSelected += OnSelectedItem;
        }
        internal PatientMainPage(string tckno)
        {
            Preferences.Set("logged", true);
            Preferences.Set("myid", tckno);
            Preferences.Set("role", "pat");
            InitializeComponent();
            PatientFlyout.PatientFlyList.ItemSelected += OnSelectedItem;
        }
        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is FlyoutPageItems item)
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
                PatientFlyout.PatientFlyList.SelectedItem = item;
                IsPresented = false;
            }
        }
    }
}