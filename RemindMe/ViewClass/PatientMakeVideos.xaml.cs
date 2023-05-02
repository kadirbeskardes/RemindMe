using RemindMe.Classes;
using Firebase.Database;
using Firebase.Storage;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientMakeVideos : ContentPage
    {
        FirebaseStorage fs = new FirebaseStorage("------------your Firebase Storage source link------------");
        static PatientInfo _patient = new PatientInfo();
        static ObservableCollection<IlacBilgi> ilacBilgi;
        static string mtckno;
        static string selectedIlac;
        static string selectedIlacSaat;
        static bool videob = false;
        static Plugin.Media.Abstractions.MediaFile video;
        static Client cl;
        public PatientMakeVideos()
        {
            InitializeComponent();
            cl = new Client();
            mtckno = Preferences.Get("myid", "11");
        }

        private async Task<string> Gonder(Stream img)
        {
            var videoS = await fs.Child("videos").Child(_patient.DoctorTCKNO).Child(_patient.TCKNO).Child($"{selectedIlac}-{selectedIlacSaat}.mp4").PutAsync(img);
            return videoS;
        }
        private async void video_gonder(object sender, EventArgs e)
        {
            if (videob)
            {
                try
                {
                    if (selectedIlac == null || selectedIlacSaat == null)
                    {
                        await DisplayAlert("İlaç seçiniz", "Videosunu yüklemek istediğiniz ilacı ve saati seçiniz.", "Yeniden dene");
                    }
                    else
                    {
                      string vs=await Gonder(video.GetStream());
                        await DisplayAlert("Ok", "Ok.", "Ok");
                    }
                }
                catch (FirebaseException ex)
                {
                    await DisplayAlert("Bağlantı sorunu...", "Bağlantınızı kontrol edin.", "Ok");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Eksik...", "Lütfen bir video yükleyeniniz.", "Ok");
            }
        }
        private async void video_kaydet(object sender, EventArgs e)
        {

            video = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
            {
                CompressionQuality = 100,
                Quality = VideoQuality.Low

            }); if (video == null)
            {
                videob = false;
            }
            else
            {
                videob = true;
                gonder.IsEnabled = true;
                await DisplayAlert("Başarılı", "Videonuz yüklendi. Lütfen gönder butonuna tıkalayarak gönderme işlemini tamamlayınız.", "Ok");
            }
        }
        protected async override void OnAppearing()
        {
            try
            {
                _patient = await cl.GetPatient(mtckno);
                ilacBilgi = new ObservableCollection<IlacBilgi>();
                if (_patient.Medicine != null)
                {
                    if (_patient.Medicine.Count == 0)
                    {
                        await DisplayAlert("Kayıtlı ilaç yok.", "Bir doktor tarafından sizin adınıza sisteme bir ilaç kaydı bulunmamaktadır. Bir yanlışlık olduğunu düşünüyorsanız lütfen hekminize danışınız.", "Ok");
                        return;
                    }
                    for (int i = 0; i < _patient.Medicine.Count; i++)
                    {
                        ilacBilgi.Add(new IlacBilgi { IlacAd = _patient.Medicine[i], IlacSaat = _patient.MedicineTime[i] });
                    }
                }
                else
                {
                    await DisplayAlert("Kayıtlı ilaç yok.", "Bir doktor tarafından sizin adınıza sisteme bir ilaç kaydı bulunmamaktadır. Bir yanlışlık olduğunu düşünüyorsanız lütfen hekminize danışınız.", "Ok");
                    return;
                }
                Content.BindingContext = _patient;
                lsviewilac.ItemsSource = ilacBilgi;
            }
            catch (FirebaseException ex)
            {
                await DisplayAlert("Bağlantı sorunu...", "Bir bağlantı hatası oluştu. İnternet bağlantınızı kontrol edin.", "Ok");
            }
            catch (Exception ex) { await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok"); }

        }
        class IlacBilgi
        {
            public string IlacAd { get; set; }
            public string IlacSaat { get; set; }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var bilgi = e.SelectedItem as IlacBilgi;
            selectedIlac = bilgi.IlacAd;
            selectedIlacSaat = bilgi.IlacSaat;
        }
        private void LV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var bilgi = e.Item as IlacBilgi;
            selectedIlac = bilgi.IlacAd;
            selectedIlacSaat = bilgi.IlacSaat;
        }
    }
}