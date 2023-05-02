using RemindMe.Classes;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;


namespace RemindMe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class DoctorMyPatientPage : ContentPage
    {
        static string mytckno;
        static List<PatientInfo> myList;
        static DoctorInfo myInfo;
        Client cl = new Client();
        public DoctorMyPatientPage()
        {
            InitializeComponent();
            mytckno = Preferences.Get("myid", "11");
        }
        protected async override void OnAppearing()
        {
            try
            {
                var timeinfo = DateTime.Now.Year;
                Content.BindingContext= timeinfo;
                myInfo = (await cl.DoctorGetMe(mytckno));
                myList = (await cl.GetAllPatient(myInfo.TCKNO));
                lsview.ItemsSource = myList;
            }
            catch (FirebaseException ex) { await DisplayAlert("Bağlantı sorunu...", "Bağlantınızı kontrol edin.", "Ok"); }
            catch (Exception ex) { await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok"); }
        }
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var patient = e.SelectedItem as PatientInfo;
            await Navigation.PushAsync(new DoctorPatientVideos(patient.TCKNO));
        }
        private async void LV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var patient = e.Item as PatientInfo;
            await Navigation.PushAsync(new DoctorPatientVideos(patient.TCKNO));
        }
        async void edit_Button_Clicked(object sender, EventArgs e)
        {
            var butItem = sender as ImageButton;
            var patient = butItem.CommandParameter as PatientInfo;
            await Navigation.PushAsync(new DoctorEditPatient(patient.TCKNO));
        }
        private async void refresh_list(object sender, EventArgs e)
        {
            try
            {
                lsview.ItemsSource = null;
                myList = (await cl.GetAllPatient(myInfo.TCKNO));
                lsview.ItemsSource = myList;
                lsview.EndRefresh();
            }
            catch (FirebaseException ex) { await DisplayAlert("Bağlantı sorunu...", "Bağlantınızı kontrol edin.", "Ok"); }
            catch (Exception ex) { await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok"); }
        }
        private void AddPatientBut(object sender, EventArgs e)
        {
            miniWindow.IsVisible = !miniWindow.IsVisible;
        }
        private async void New_Patient(object sender, EventArgs e)
        {
            if (myList.Any(x => x.TCKNO == YeniTC.Text))
            {
                await DisplayAlert("Opps...", "Bu hastanız eklenmiş bulunmakta.", "Ok");
                YeniTC.Text = String.Empty;
                return;
            }
            try
            {
                var isThere = await cl.isThereNewPatient(YeniTC.Text);
                if (!isThere)
                {
                    await DisplayAlert("Opps...", "Sistemde kayıtlı böyle bir hasta yok veya başka bir doktora kayıt olmuş durumda.", "Ok");return;
                }
                var newPa = await cl.addNewPatient(YeniTC.Text);
                await cl.addNewPatient_1(newPa, mytckno);
                myList.Add(newPa);
                refresh_list(sender,e);
                YeniTC.Text=string.Empty;
                AddPatientBut(sender, e);
            }
            catch (FirebaseException ex) { await DisplayAlert("Bağlantı sorunu...", "Bağlantınızı kontrol edin.", "Ok"); }
            catch (Exception ex) { await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok"); }
        }
        private async void delete_Button_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as ImageButton;
            var patient = menuItem.CommandParameter as PatientInfo;
            myList.Remove(patient);
            try
            {
                var response = await DisplayAlert("Emin...", "Seçili hastayı silmek istediğinizden emin misiniz?", "Evet", "Hayır");
                if (response)
                {
                    var sonuc = await (cl.DeletePatient(patient));
                    if (sonuc)
                    {
                        await DisplayAlert("Ok", "Silindi", "tamam");
                    }
                }
                refresh_list(sender, e);

            }
            catch (FirebaseException ex) { await DisplayAlert("Bağlantı sorunu...", "Bağlantınızı kontrol edin.", "Ok"); }
            catch (Exception ex) { await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok"); }
        }
        private void Search_Patient(object sender, TextChangedEventArgs e)
        {
            lsview.ItemsSource = myList.Where(p => p.TCKNO.StartsWith(e.NewTextValue));
        }
    }
}