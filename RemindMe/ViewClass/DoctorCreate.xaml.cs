using RemindMe.Classes;
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorCreate : ContentPage
    {
        public DoctorCreate()
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
                {sb.Append(hashByte[i].ToString("X2"));}
                string conPass = sb.ToString();
                return conPass;
            }
        }
        private async void yeni_kayit(object sender, EventArgs e)
        {
            if (photob)
            {
                try
                {
                    if (string.IsNullOrEmpty(tckno.Text) || string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(ad.Text)
                        || string.IsNullOrEmpty(soyad.Text) || string.IsNullOrEmpty(hastane.Text) || string.IsNullOrEmpty(telno.Text)
                        || string.IsNullOrEmpty(sifre.Text) || tckno.Text.Length != 11 || string.IsNullOrEmpty(cv.Text))
                    {
                        await DisplayAlert("Eksik girdi...", "Eksik veya hatalı giriş yaptınız.", "Yeniden dene");
                    }
                    else
                    {
                        if (await myCl.isThereDoctor(tckno.Text, email.Text))
                        {
                            await DisplayAlert("Hatalı giriş", "Bu mail adresi veya TCKNO kullanılmaktadır.", "Yeniden dene");
                        }
                        else
                        {
                            var doctor = new DoctorInfo()
                            {
                                TCKNO = tckno.Text,
                                Name = ad.Text + " " + soyad.Text,
                                HospitalName = hastane.Text,
                                Mail = email.Text,
                                TelNo = telno.Text,
                                password = getHash(sifre.Text),
                                CV = cv.Text
                            };
                            var imageS = new FirebaseStorage("------------your Firebase Storage source link------------",new FirebaseStorageOptions
                                {ThrowOnCancel = true}).Child("photos").Child("doctor").Child(doctor.TCKNO + ".jpeg").PutAsync(await photo.OpenReadAsync());
                            var downloadLink = await imageS;
                            doctor.ImageURL = downloadLink;
                            var sonuc = await myCl.Save(doctor);
                            if (sonuc)
                            {
                                App.Current.MainPage = new DoctorMainPage(doctor.TCKNO);
                            }
                            else
                            {
                                await DisplayAlert("Başarısız...", "Başarısız giriş...", "Tekrar dene");
                            }
                        }
                    }
                }
                catch (FirebaseException ex){await DisplayAlert("Bağlantı sorunu...", "Bağlantınızı kontrol edin.", "Ok");}
                catch (Exception ex){await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok");}
            }
            else{await DisplayAlert("Eksik...", "Lütfen bir fotoğraf yükleyeniniz.", "Ok");}
        }
        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            try
            {
                photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
                if (photo == null) { photob = false; }
                else
                { foto.Source = photo.FullPath; foto.WidthRequest = 100; photob = true; }
            }
            catch (Exception ex)
            { await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok"); }
        }
    }
}