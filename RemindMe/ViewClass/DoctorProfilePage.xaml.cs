using RemindMe.Classes;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorProfilePage : ContentPage
    {
        static string mytckno;
        static DoctorInfo myInfo;
        FirebaseClient client = new FirebaseClient("------------your Firebase Database source link------------");
        public DoctorProfilePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                mytckno = Preferences.Get("myid", "11");
                myInfo = (await GetDoctor())[0];
                Content.BindingContext = myInfo;
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
        internal async Task<List<DoctorInfo>> GetDoctor()
        {
            return (await client.Child(nameof(DoctorInfo))
                .OnceAsync<DoctorInfo>()).Where(x => x.Object.TCKNO == mytckno).Select(item => new DoctorInfo
                {
                    Name = item.Object.Name,
                    HospitalName = item.Object.HospitalName,
                    TCKNO = item.Object.TCKNO,
                    ImageURL = item.Object.ImageURL,
                    TelNo = item.Object.TelNo,
                    Mail = item.Object.Mail,
                    CV=item.Object.CV,
                    password=item.Object.password
                }).ToList();
        }
    }
}