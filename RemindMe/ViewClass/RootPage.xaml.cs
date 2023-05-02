using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.EventArgs;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : ContentPage
    {
        public RootPage()
        {
            InitializeComponent();
        }
        private void Doctor_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new DoctorLoginPage();
        }
        private void Patient_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new PatientLoginPage();
        }
    }
}