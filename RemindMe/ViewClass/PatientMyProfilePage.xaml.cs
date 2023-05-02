using RemindMe.Classes;
using Firebase.Database;
using Plugin.LocalNotification;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientMyProfilePage : ContentPage
    {
        static string mytckno;
        static PatientInfo myInfo;
        Client client = new Client();
        public PatientMyProfilePage()
        {
            InitializeComponent();
            mytckno = Preferences.Get("myid", "11");
        }
        protected override async void OnAppearing()
        {
            try
            {
                myInfo = await client.GetPatient(mytckno);
                if (!string.IsNullOrEmpty(myInfo.DoctorTCKNO)) Preferences.Set("mydocid", myInfo.DoctorTCKNO);
                else Preferences.Set("mydocid", null);

                Content.BindingContext = myInfo;
                if (myInfo.Medicine != null)
                {
                    var ilacAd = myInfo.Medicine;
                    var ilacSaat = myInfo.MedicineTime;
                    for (int i = 0; i < ilacAd.Count; i++)
                    {
                        var notification = new NotificationRequest
                        {
                            BadgeNumber = 1,
                            Description = $"{ilacAd[i]} isimli ilacın saati geldi. Lütfen yarım saati içinde ilacı alınız.",
                            ReturningData = "Dummy Data",
                            NotificationId = (i + 58) * i,
                            Title = "İlaçın saati geldi",
                            Subtitle = "Hatırlatma!!!!!",
                            Schedule =
                            {
                                NotifyTime =(DateTime)(Convert.ToDateTime(ilacSaat[i])), RepeatType=NotificationRepeat.Daily
                            }
                        };
                        await LocalNotificationCenter.Current.Show(notification);
                    }

                }
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
    }
}
