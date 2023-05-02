using RemindMe.Classes;
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorPatientVideos : ContentPage
    {
        FirebaseClient firebase =
                    new FirebaseClient("------------your Firebase Database source link------------");
        FirebaseStorage fs = new FirebaseStorage("------------your Firebase Storage source link------------");
        static PatientInfo _patient = new PatientInfo();
        static ObservableCollection<IlacBilgi> ilacBilgi;
        static string mtckno;
        static string selectedIlac;
        static string selectedIlacSaat;
        public DoctorPatientVideos()
        {
            InitializeComponent();
        }
        public DoctorPatientVideos(string tckno)
        {
            InitializeComponent();
            mtckno = tckno;
        }
        protected async override void OnAppearing()
        {
            try
            {
                _patient = await GetPatient(mtckno);
                ilacBilgi = new ObservableCollection<IlacBilgi>();
                if (_patient.Medicine != null)
                {
                    if (_patient.Medicine.Count == 0)
                    {
                        await DisplayAlert("Kayıtlı ilaç yok.", "Bir doktor tarafından bu hasta adına sisteme bir ilaç kaydı bulunmamaktadır.", "Ok");
                        return;
                    }
                    for (int i = 0; i < _patient.Medicine.Count; i++)
                    {
                        ilacBilgi.Add(new IlacBilgi { IlacAd = _patient.Medicine[i], IlacSaat = _patient.MedicineTime[i] });
                    }
                }
                Content.BindingContext = _patient;
                lsviewilac.ItemsSource = ilacBilgi;
            }
            catch (FirebaseException ex)
            {
                await DisplayAlert("Bağlantı sorunu...",
                    "Bir bağlantı hatası oluştu. İnternet bağlantınızı kontrol edin.", "Ok");
            }
            catch (Exception ex) { 
                await DisplayAlert("Bir hata meydana geldi", 
                    "Belirlenemeyen bir sorun oluştu..", "Ok"); }

        }
        internal async Task<PatientInfo> GetPatient(string mytckno)
        {
            return (await firebase.Child(nameof(PatientInfo)).OnceAsync<PatientInfo>())
                .Where(x => x.Object.TCKNO == mytckno).Select(item => new PatientInfo
                {
                    Ad_Soyad = item.Object.Ad_Soyad,
                    DoctorTCKNO = item.Object.DoctorTCKNO,
                    TCKNO = item.Object.TCKNO,
                    ImageURL = item.Object.ImageURL,
                    TelNo = item.Object.TelNo,
                    Mail = item.Object.Mail,
                    Medicine = item.Object.Medicine,
                    MedicineTime = item.Object.MedicineTime,
                }).ToList()[0];
        }
        class IlacBilgi
        {
            public string IlacAd { get; set; }
            public string IlacSaat { get; set; }
        }
        
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var bilgi = e.SelectedItem as IlacBilgi;
                selectedIlac = bilgi.IlacAd;
                selectedIlacSaat = bilgi.IlacSaat;
                    FirebaseStorageReference videoRef = fs.Child("videos")
                    .Child(_patient.DoctorTCKNO).Child(_patient.TCKNO)
                    .Child($"{selectedIlac}-{selectedIlacSaat}.mp4");
                    var video = await videoRef.GetDownloadUrlAsync();
                    media.Source = video;                
            }
            catch (Exception ex) 
            { 
                await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok");
            }
        }
    }
}