using RemindMe;
using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace RemindMe
{

    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();
            if (Preferences.Get("logged", false) == true)
            {
                if (Preferences.Get("role", "no") == "doc")
                {
                    RS["Style"] = Color.FromHex("0080FF");
                    App.Current.MainPage = new DoctorMainPage();
                }
                else if (Preferences.Get("role", "no") == "pat")
                {
                    RS["Style"] = Color.FromHex("00FF80");
                    App.Current.MainPage = new PatientMainPage();
                }
            }
            else
            {
                MainPage = new RootPage();
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
