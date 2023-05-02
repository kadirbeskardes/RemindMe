using RemindMe.Classes;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorEditPatient : ContentPage
    {
        FirebaseClient firebase =
                    new FirebaseClient("------------your Firebase Database source link------------");
        class IlacBilgi
        {
            public string IlacAd { get; set; }
            public string IlacSaat { get; set; }
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
                    password = item.Object.password,
                    Medicine = item.Object.Medicine,
                    MedicineTime = item.Object.MedicineTime,
                    CV=item.Object.CV
                }).ToList()[0];
        }
        static PatientInfo _patient = new PatientInfo();
        static ObservableCollection<IlacBilgi> ilacBilgi;
        static string mtckno;
        internal DoctorEditPatient(string tckno)
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
                        lsviewilac.IsVisible= false;
                        return;
                    }
                    else
                    {
                        lsviewilac.IsVisible= true;
                        noMedicine.IsVisible = false;
                        for (int i = 0; i < _patient.Medicine.Count; i++)
                        {
                            ilacBilgi.Add(new IlacBilgi { IlacAd = _patient.Medicine[i], IlacSaat = _patient.MedicineTime[i] });
                        }
                    }
                }
                Content.BindingContext = _patient;
                lsviewilac.ItemsSource = ilacBilgi;
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
        private async void degistir(object sender, EventArgs e)
        {
            _patient.Status = Status.Text;
            try
            {
                await firebase.Child(nameof(PatientInfo) + "/" + _patient.TCKNO)
                    .PutAsync(JsonConvert.SerializeObject(_patient));
            }
            catch (FirebaseException ex)
            {
                await DisplayAlert("Bağlantı sorunu...", "Bir bağlantı hatası oluştu. İnternet bağlantınızı kontrol edin.", "Ok");
            }
            await Navigation.PopAsync();
        }
        private async void saatEkle(object sender, EventArgs e)
        {
            var ilac_saat = saat.Time.ToString();
            var ilac_isim = isim.Text;
            if (_patient.MedicineTime == null)
            {
                _patient.MedicineTime = new ObservableCollection<string>();
            }
            if (_patient.Medicine == null)
            {
                _patient.Medicine = new ObservableCollection<string>();
            }
            _patient.MedicineTime.Add(ilac_saat);
            _patient.Medicine.Add(ilac_isim);
            try
            {
                ilacBilgi.Add(new IlacBilgi { IlacAd = ilac_isim, IlacSaat = ilac_saat });
                await firebase.Child(nameof(PatientInfo) + "/" + _patient.TCKNO)
                    .PutAsync(JsonConvert.SerializeObject(_patient));

            }
            catch (FirebaseException ex)
            {
                await DisplayAlert("Bağlantı sorunu...", "Bir bağlantı hatası oluştu. İnternet bağlantınızı kontrol edin.", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok");
            }
            lsviewilac.IsVisible =true;
            noMedicine.IsVisible =false;
        }
    }
}