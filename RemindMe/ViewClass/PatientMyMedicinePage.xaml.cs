using RemindMe.Classes;
using Firebase.Database;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientMyMedicinePage : ContentPage
    {
        FirebaseClient firebase =
                    new FirebaseClient("------------your Firebase Database source link------------");
        public PatientMyMedicinePage()
        {
            InitializeComponent();
            mtckno = Preferences.Get("myid", "11");
        }
        class IlacBilgi
        {
            public string IlacAd { get; set; }
            public string IlacSaat { get; set; }
        }
        static PatientInfo _patient = new PatientInfo();
        static ObservableCollection<IlacBilgi> ilacBilgi;
        static string mtckno;
        protected async override void OnAppearing()
        {
            try
            {
                _patient = await GetPatient(mtckno);
                ilacBilgi = new ObservableCollection<IlacBilgi>();
                if (_patient.Medicine != null)
                {
                    if(_patient.Medicine.Count==0)
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
    }
}