using RemindMe.Classes;
using Firebase.Database;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientMyDoctorPage : ContentPage
    {
        Client cl = new Client();
        public PatientMyDoctorPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            var doctckno = Preferences.Get("mydocid", "null");
            if (doctckno != "null")
            {
                try
                {
                    DoctorInfo doctor = await cl.DoctorGetMe(doctckno);
                    Content.BindingContext = doctor;
                }
                catch (FirebaseException ex)
                {
                    await DisplayAlert("Bağlantı sorunu...", "Bir bağlantı hatası oluştu. İnternet bağlantınızı kontrol edin.", "Ok");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Doktor bulunamadı.", "Sistemine kayıtlı olduğunuz bir doktor yok. Eğer bir yanlışlıkl olduğunu düşünüyorsanız hekiminize danışınız...", "Tamam");
            }
        }
    }
}