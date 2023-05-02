using RemindMe.Classes;
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientCreate : ContentPage
    {
        public PatientCreate()
        {
            InitializeComponent();
        }
        Client myCl = new Client();
        static bool photob = false;
        static Xamarin.Essentials.FileResult photo;
        private string getHash(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] yeniByte = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hashByte = md5.ComputeHash(yeniByte);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashByte.Length; i++)
                {
                    sb.Append(hashByte[i].ToString("X2"));
                }
                string conPass = sb.ToString();
                return conPass;
            }
        }
        private async void yeni_kayit(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tckno.Text) || string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(ad.Text)
                        || string.IsNullOrEmpty(soyad.Text) || string.IsNullOrEmpty(birth.Date.ToString()) || string.IsNullOrEmpty(telno.Text)
                        || string.IsNullOrEmpty(sifre.Text) || tckno.Text.Length != 11 || string.IsNullOrEmpty(cv.Text))
            {
                await DisplayAlert("Eksik girdi...", "Eksik veya hatalı giriş yaptınız.", "Yeniden dene");
            }
            else
            {
                try
                {
                    if (await myCl.isTherePatient(tckno.Text, email.Text))
                    {
                        await DisplayAlert("Hatalı giriş", "Bu mail adresi veya TCKNO kullanılmaktadır.", "Yeniden dene");
                        return;
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
                if (photob)
                {
                    var patient = new PatientInfo()
                    {
                        TCKNO = tckno.Text,
                        Ad_Soyad = ad.Text + " " + soyad.Text,
                        Mail = email.Text,
                        TelNo = telno.Text,
                        BirthDate = birth.Date,
                        password = getHash(sifre.Text),
                        Medicine = new ObservableCollection<string>(),
                        MedicineTime = new ObservableCollection<string>(),
                        CV = cv.Text
                    };
                    try
                    {
                        var imageS = new FirebaseStorage("------------your Firebase Storage source link------------",
                            new FirebaseStorageOptions
                            {
                                ThrowOnCancel = true
                            })
                            .Child("photos").Child("doctor").Child("photos").Child("patient").Child(patient.TCKNO + ".jpeg")
                            .PutAsync(await photo.OpenReadAsync());
                        var downloadLink = await imageS;
                        patient.ImageURL = downloadLink;
                        var sonuc = await myCl.SavePatient(patient);

                        if (sonuc)
                        {
                            await DisplayAlert("Başarılı...", "Başarılı giriş...", "Ok");
                            App.Current.MainPage = new PatientMainPage(patient.TCKNO);
                        }
                        else
                        {
                            await DisplayAlert("Başarısız...", "Başarısız giriş...", "Tekrar dene");
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
        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            try
            {
                photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
                if (photo == null)
                {
                    photob = false;
                }
                else
                {
                    foto.Source = photo.FullPath;
                    foto.WidthRequest = 100;
                    photob = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok");
            }
        }
    }
}