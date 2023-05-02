using Firebase.Database;
using System;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientLoginPage : ContentPage
    {
        Client client = new Client();
        public PatientLoginPage()
        {
            InitializeComponent();
        }
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
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (passwordEntry.IsPassword == false)
            {
                passwordEntry.IsPassword = true;
                gizBut.Source = "hide.png";
            }
            else
            {
                passwordEntry.IsPassword = false;
                gizBut.Source = "view.png";
            }
        }
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(idEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
                {
                    await DisplayAlert("Hatalı giriş...", "Kullanıcı adını ve şifrenizi girin.", "Ok");
                }
                else
                {
                    var islog = (await client.isLogPatient(idEntry.Text, getHash(passwordEntry.Text)));
                    if (islog)
                    {
                        App.Current.MainPage = new PatientMainPage(idEntry.Text);
                    }
                    else
                    {
                        await DisplayAlert("Hatalı giriş", "Hatalı giriş yaptınız.", "Tekrar dene");
                    }
                }

            }
            catch (FirebaseException ex)
            {
                await DisplayAlert("Bağlantı sorunu...", "Bir bağlantı hatası oluştu. İnternet bağlantınızı kontrol edin.", "Ok");
            }
            catch (Exception ex) { await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok"); }

        }
         void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
                App.Current.MainPage = new NavigationPage(new PatientCreate());
        }
    }
}