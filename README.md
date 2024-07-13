Bu projede, doktor-hasta etkileşimli bir ilaç hatırlatıcısı yapmak hedeflenmiştir. Alzheimer hastası kişiye, doktorun sisteme girdiği ilacı sisteme girilen saatte içmesine yardım edilmesi için yapılan bir uygulamadır. Alınması gereken ilaç için doktorun istediği saatte hastaya bildirim gidecektir. Hasta, bu ilacı içtiğine dair video kaydını doktora gönderebilecektir. 


Bu proje, hasta kişinin kullanımında kişi ilaç saatlerinde hatırlatma bildirimi almasını sağlar. Bu ilacı aldığına dair video kaydını doktoruna gönderebilir. İlaçlarını ve bu ilaçları alması gereken saati görebilir. Doktoruna ait gerekli bilgileri görebilir. Doktor kişinin kullanımında ise doktor, sistemde başka bir doktor ile ilişiği bulunmayan hastayı kendi sistemine bağlayabilir. Kendi sistemine bağlı tüm hastaları listeleyebilir. Bu hastaların kullanması gereken ilaçları ve ilacı alması gereken saati hasta üzerinden sisteme girebilir. Doktor, hastalarının kendine gönderdiği video kayıtlarını izleyebilir.

Dil: C# , XAML 

Kullanılan Derleyici: Visual Studio 

Kütüphaneler: Xamarin Forms, .NET 

Teknolojiler: Android Studio, Firebase Real-Time Database, Firebase Storage

Minimum Android Version: Android 4.4.87 (API Level 21)

Target Android Verison: Android 12.0 (API Level 31)



## Kurulum

RemindMe'i kurmak ve çalıştırmak için aşağıdaki adımları izleyin:

1. Bu depoyu klonlayın:
    ```bash
    git clone https://github.com/kadirbeskardes/RemindMe.git
    ```
2. Projeyi Visual Studio'da açın.
3. Gerekli bağımlılıkları yükleyin(Bağımlılıklar zaten yüklüyse bu adımı kesinlikle atlamanız tavsiye edilir):
    - NuGet Paket Yöneticisi Konsolu'nu açın ve aşağıdaki komutları çalıştırın:
      ```powershell
      Install-Package FirebaseDatabase.net -Version 4.2.0
      Install-Package FirebaseStorage.net -Version 1.0.3
      Install-Package Plugin.LocalNotification -Version 10.0.3
      Install-Package Xam.Plugin.Media -Version 5.0.1
      Install-Package Xam.Plugins.Forms.ImageCircle -Version 3.0.0.5
      Install-Package Xamarin.CommunityToolkit -Version 2.0.5
      Install-Package Xamarin.Essentials -Version 1.7.3
      Install-Package Xamarin.Forms -Version 5.0.0.2515
      ```
4. Google Firebase üzerinden oluşturduğunuz Firebase Realtime Database bağlantısını ve Firebase Storage bağlantısını projede gereken her yere ekleyin.
5. Projeyi derleyin ve çalıştırın.
