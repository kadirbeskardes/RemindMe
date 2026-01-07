# 💊 RemindMe - Doktor-Hasta İlaç Takip ve Hatırlatma Uygulaması

<p align="center">
  <img src="https://img.shields.io/badge/Xamarin.Forms-5.0.0.2515-3498DB?style=for-the-badge&logo=xamarin&logoColor=white" alt="Xamarin.Forms"/>
  <img src="https://img.shields.io/badge/C%23-.NET%20Standard%202.1-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#"/>
  <img src="https://img.shields.io/badge/Android-API%2031-3DDC84?style=for-the-badge&logo=android&logoColor=white" alt="Android"/>
  <img src="https://img.shields.io/badge/Firebase-Realtime%20Database-FFCA28?style=for-the-badge&logo=firebase&logoColor=black" alt="Firebase"/>
</p>

<p align="center">
  <strong>RemindMe</strong>, doktorlar ve hastalar arasında ilaç takibi ve yönetimini kolaylaştıran, 
  Firebase tabanlı bir Xamarin.Forms mobil uygulamasıdır.
</p>

---

## 📋 İçindekiler

- [Proje Hakkında](#-proje-hakkında)
- [Özellikler](#-özellikler)
- [Teknolojiler ve Bağımlılıklar](#-teknolojiler-ve-bağımlılıklar)
- [Proje Yapısı](#-proje-yapısı)
- [Veri Modelleri](#-veri-modelleri)
- [Sayfa ve Ekran Açıklamaları](#-sayfa-ve-ekran-açıklamaları)
- [Firebase Entegrasyonu](#-firebase-entegrasyonu)
- [Güvenlik](#-güvenlik)
- [Kurulum](#-kurulum)
- [Yapılandırma](#-yapılandırma)
- [Katkıda Bulunma](#-katkıda-bulunma)

---

## 🎯 Proje Hakkında

**RemindMe**, sağlık sektörü için tasarlanmış kapsamlı bir ilaç takip uygulamasıdır. Uygulama iki ana kullanıcı rolü üzerine kurulmuştur:

| Rol | Açıklama |
|-----|----------|
| 👨‍⚕️ **Doktor** | Hasta yönetimi, ilaç reçetesi oluşturma, hasta durum takibi ve video izleme |
| 🧑‍🤝‍🧑 **Hasta** | İlaç listesi görüntüleme, ilaç hatırlatmaları alma ve doktora video gönderme |

Uygulama, **Firebase Realtime Database** ve **Firebase Storage** kullanarak gerçek zamanlı veri senkronizasyonu sağlar.

---

## ✨ Özellikler

### 👨‍⚕️ Doktor Özellikleri

| Özellik | Açıklama |
|---------|----------|
| 📝 **Hesap Oluşturma** | TCKNO, e-posta, hastane bilgisi ve profil fotoğrafı ile kayıt |
| 🔐 **Güvenli Giriş** | MD5 şifreleme ile güvenli kimlik doğrulama |
| 👥 **Hasta Yönetimi** | TCKNO ile hasta ekleme, listeleme ve silme |
| 💊 **İlaç Tanımlama** | Hastalar için ilaç adı ve saat bilgisi ekleme |
| 📊 **Durum Takibi** | Hasta durumunu görüntüleme ve güncelleme |
| 🎬 **Video İzleme** | Hastaların gönderdiği ilaç alma videolarını izleme |
| 🔍 **Hasta Arama** | TCKNO ile hızlı hasta arama |
| 👤 **Profil Yönetimi** | Kendi profil bilgilerini görüntüleme |

### 🧑‍🤝‍🧑 Hasta Özellikleri

| Özellik | Açıklama |
|---------|----------|
| 📝 **Hesap Oluşturma** | TCKNO, e-posta, doğum tarihi ve profil fotoğrafı ile kayıt |
| 🔐 **Güvenli Giriş** | MD5 şifreleme ile güvenli kimlik doğrulama |
| 💊 **İlaç Listesi** | Doktor tarafından tanımlanan ilaçları görüntüleme |
| ⏰ **Bildirim Sistemi** | Günlük tekrarlayan ilaç hatırlatmaları |
| 👨‍⚕️ **Doktor Bilgisi** | Kayıtlı olduğu doktorun bilgilerini görüntüleme |
| 🎥 **Video Gönderme** | İlaç alımını kanıtlayan video kaydı ve gönderme |
| 👤 **Profil Yönetimi** | Kendi profil bilgilerini görüntüleme |

### 🔔 Bildirim Sistemi

- **Plugin.LocalNotification** kullanılarak yerel bildirimler
- Günlük tekrarlayan hatırlatmalar (`NotificationRepeat.Daily`)
- İlaç adı ve saatine göre özelleştirilmiş bildirim mesajları
- Badge numarası desteği

---

## 🛠 Teknolojiler ve Bağımlılıklar

### Ana Framework
```
Xamarin.Forms                 5.0.0.2515
.NET Standard                 2.1
```

### NuGet Paketleri

| Paket | Versiyon | Kullanım Amacı |
|-------|----------|----------------|
| `FirebaseDatabase.net` | 4.2.0 | Firebase Realtime Database bağlantısı |
| `FirebaseStorage.net` | 1.0.3 | Firebase Storage (fotoğraf/video depolama) |
| `Plugin.LocalNotification` | 10.0.3 | Yerel bildirim sistemi |
| `Xam.Plugin.Media` | 5.0.1 | Kamera ve galeri erişimi |
| `Xam.Plugins.Forms.ImageCircle` | 3.0.0.5 | Dairesel profil resimleri |
| `Xamarin.CommunityToolkit` | 2.0.5 | Ek UI bileşenleri |
| `Xamarin.Essentials` | 1.7.3 | Platform özellikleri (Preferences, MediaPicker) |

### Android Gereksinimleri
```xml
Minimum SDK: API 20
Target SDK: API 31
```

### Android İzinleri
```xml
ACCESS_NETWORK_STATE    - İnternet bağlantısı kontrolü
CAPTURE_VIDEO_OUTPUT    - Video çekimi
CAPTURE_SECURE_VIDEO_OUTPUT - Güvenli video çekimi
CAMERA                  - Kamera erişimi
```

---

## 📁 Proje Yapısı

```
RemindMe/
│
├── 📄 RemindMe.sln                          # Solution dosyası
│
├── 📂 RemindMe/                             # Shared .NET Standard Projesi
│   │
│   ├── 📄 AssemblyInfo.cs                   # Assembly bilgileri
│   ├── 📄 Client.cs                         # Firebase işlemleri servisi
│   ├── 📄 FlyoutPageItems.cs                # Menü öğesi modeli
│   ├── 📄 RemindMe.csproj                   # Proje dosyası
│   │
│   ├── 📂 Classes/                          # Veri Modelleri
│   │   ├── 📄 DoctorInfo.cs                 # Doktor veri modeli
│   │   └── 📄 PatientInfo.cs                # Hasta veri modeli
│   │
│   ├── 📂 View/                             # XAML Arayüz Dosyaları
│   │   ├── 📄 App.xaml                      # Uygulama kaynakları
│   │   ├── 📄 RootPage.xaml                 # Ana giriş sayfası
│   │   ├── 📄 DoctorLoginPage.xaml          # Doktor giriş sayfası
│   │   ├── 📄 DoctorCreate.xaml             # Doktor kayıt sayfası
│   │   ├── 📄 DoctorMainPage.xaml           # Doktor ana sayfa (FlyoutPage)
│   │   ├── 📄 DoctorFlyoutPageItem.xaml     # Doktor menü sayfası
│   │   ├── 📄 DoctorProfilePage.xaml        # Doktor profil sayfası
│   │   ├── 📄 DoctorMyPatientPage.xaml      # Hasta listesi sayfası
│   │   ├── 📄 DoctorEditPatient.xaml        # Hasta düzenleme sayfası
│   │   ├── 📄 DoctorPatientVideos.xaml      # Hasta videoları sayfası
│   │   ├── 📄 PatientLoginPage.xaml         # Hasta giriş sayfası
│   │   ├── 📄 PatientCreate.xaml            # Hasta kayıt sayfası
│   │   ├── 📄 PatientMainPage.xaml          # Hasta ana sayfa (FlyoutPage)
│   │   ├── 📄 PatientFlyoutPageItem.xaml    # Hasta menü sayfası
│   │   ├── 📄 PatientMyProfilePage.xaml     # Hasta profil sayfası
│   │   ├── 📄 PatientMyDoctorPage.xaml      # Doktorum sayfası
│   │   ├── 📄 PatientMyMedicinePage.xaml    # İlaçlarım sayfası
│   │   └── 📄 PatientMakeVideos.xaml        # Video gönderme sayfası
│   │
│   └── 📂 ViewClass/                        # Code-Behind Dosyaları
│       ├── 📄 App.xaml.cs
│       ├── 📄 RootPage.xaml.cs
│       ├── 📄 DoctorLoginPage.xaml.cs
│       ├── 📄 DoctorCreate.xaml.cs
│       ├── 📄 DoctorMainPage.xaml.cs
│       ├── 📄 DoctorFlyoutPageItem.xaml.cs
│       ├── 📄 DoctorProfilePage.xaml.cs
│       ├── 📄 DoctorMyPatientPage.cs
│       ├── 📄 DoctorEditPatient.xaml.cs
│       ├── 📄 DoctorPatientVideos.xaml.cs
│       ├── 📄 PatientLoginPage.xaml.cs
│       ├── 📄 PatientCreate.xaml.cs
│       ├── 📄 PatientMainPage.xaml.cs
│       ├── 📄 PatientFlyoutPageItem.xaml.cs
│       ├── 📄 PatientMyProfilePage.xaml.cs
│       ├── 📄 PatientMyDoctorPage.xaml.cs
│       ├── 📄 PatientMyMedicinePage.xaml.cs
│       └── 📄 PatientMakeVideos.xaml.cs
│
└── 📂 RemindMe.Android/                     # Android Platform Projesi
    ├── 📄 MainActivity.cs                   # Android giriş noktası
    ├── 📄 RemindMe.Android.csproj           # Android proje dosyası
    │
    ├── 📂 Properties/
    │   ├── 📄 AndroidManifest.xml           # Android manifest
    │   └── 📄 AssemblyInfo.cs
    │
    └── 📂 Resources/
        ├── 📂 drawable/                     # Görsel kaynaklar
        ├── 📂 mipmap-*/                     # Uygulama ikonları
        ├── 📂 values/
        │   ├── 📄 colors.xml                # Renk tanımları
        │   └── 📄 styles.xml                # Stil tanımları
        └── 📂 xml/
            └── 📄 file_paths.xml            # FileProvider yolları
```

---

## 📊 Veri Modelleri

### DoctorInfo (Doktor Bilgileri)

```csharp
public class DoctorInfo
{
    public string TCKNO { get; set; }        // Birincil anahtar (11 haneli)
    public string Name { get; set; }          // Ad Soyad
    public string ImageURL { get; set; }      // Profil fotoğrafı URL
    public string HospitalName { get; set; }  // Hastane adı
    public string TelNo { get; set; }         // Telefon numarası
    public string Mail { get; set; }          // E-posta adresi
    public string CV { get; set; }            // Özgeçmiş bilgisi
    public string password { get; set; }      // Şifre (MD5 hash)
}
```

### PatientInfo (Hasta Bilgileri)

```csharp
internal class PatientInfo
{
    public string Ad_Soyad { get; set; }      // Ad Soyad
    public string TCKNO { get; set; }         // Birincil anahtar (11 haneli)
    public string ImageURL { get; set; }      // Profil fotoğrafı URL
    public string TelNo { get; set; }         // Telefon numarası
    public string Mail { get; set; }          // E-posta adresi
    public string Status { get; set; }        // Hasta durumu
    public string DoctorTCKNO { get; set; }   // Bağlı doktorun TCKNO'su
    public DateTime BirthDate { get; set; }   // Doğum tarihi
    public string password { get; set; }      // Şifre (MD5 hash)
    public string CV { get; set; }            // Özgeçmiş bilgisi
    
    public ObservableCollection<string> Medicine { get; set; }      // İlaç listesi
    public ObservableCollection<string> MedicineTime { get; set; }  // İlaç saatleri
}
```

### FlyoutPageItems (Menü Öğesi)

```csharp
public class FlyoutPageItems
{
    public string Title { get; set; }         // Menü başlığı
    public string IconSource { get; set; }    // İkon kaynağı
    public Type TargetPage { get; set; }      // Hedef sayfa tipi
}
```

---

## 📱 Sayfa ve Ekran Açıklamaları

### 🚀 Uygulama Başlangıç Akışı

```
App.xaml.cs
    │
    ├── Kullanıcı giriş yapmış mı? (Preferences kontrolü)
    │   │
    │   ├── ✅ Evet → Rol kontrolü
    │   │   ├── "doc" → DoctorMainPage
    │   │   └── "pat" → PatientMainPage
    │   │
    │   └── ❌ Hayır → RootPage (Rol seçim ekranı)
```

### 📄 Sayfa Detayları

| Sayfa | Tip | Açıklama |
|-------|-----|----------|
| `RootPage` | ContentPage | Doktor/Hasta seçim ekranı |
| `DoctorLoginPage` | ContentPage | Doktor giriş formu |
| `PatientLoginPage` | ContentPage | Hasta giriş formu |
| `DoctorCreate` | ContentPage | Doktor kayıt formu |
| `PatientCreate` | ContentPage | Hasta kayıt formu |
| `DoctorMainPage` | FlyoutPage | Doktor ana sayfa (menü yapısı) |
| `PatientMainPage` | FlyoutPage | Hasta ana sayfa (menü yapısı) |
| `DoctorProfilePage` | ContentPage | Doktor profil görüntüleme |
| `PatientMyProfilePage` | ContentPage | Hasta profil ve bildirim ayarları |
| `DoctorMyPatientPage` | ContentPage | Hasta listesi ve yönetimi |
| `DoctorEditPatient` | ContentPage | Hasta bilgisi ve ilaç düzenleme |
| `DoctorPatientVideos` | ContentPage | Hasta videolarını izleme |
| `PatientMyMedicinePage` | ContentPage | İlaç listesi görüntüleme |
| `PatientMyDoctorPage` | ContentPage | Doktor bilgisi görüntüleme |
| `PatientMakeVideos` | ContentPage | Video çekme ve gönderme |

### 🎨 Menü Yapısı

#### Doktor Menüsü (DoctorFlyoutPageItem)
```
┌─────────────────────────────┐
│ 🏠 Profilim                 │ → DoctorProfilePage
├─────────────────────────────┤
│ 👨‍⚕️ Hastalarım              │ → DoctorMyPatientPage
├─────────────────────────────┤
│ 🚪 Çıkış Yap                │ → RootPage
└─────────────────────────────┘
```

#### Hasta Menüsü (PatientFlyoutPageItem)
```
┌─────────────────────────────┐
│ 🏠 Profilim                 │ → PatientMyProfilePage
├─────────────────────────────┤
│ 👨‍⚕️ Doktorum                │ → PatientMyDoctorPage
├─────────────────────────────┤
│ 💊 İlaçlarım                │ → PatientMyMedicinePage
├─────────────────────────────┤
│ 🎥 Doktoruna Video Gönder   │ → PatientMakeVideos
├─────────────────────────────┤
│ 🚪 Çıkış Yap                │ → RootPage
└─────────────────────────────┘
```

---

## 🔥 Firebase Entegrasyonu

### Client.cs - Firebase Servis Sınıfı

`Client.cs` dosyası, tüm Firebase işlemlerini yöneten merkezi servis sınıfıdır:

| Metot | Açıklama |
|-------|----------|
| `Save(DoctorInfo)` | Doktor bilgisini kaydet |
| `SavePatient(PatientInfo)` | Hasta bilgisini kaydet |
| `isThereDoctor(tckno, email)` | Doktor var mı kontrolü |
| `isTherePatient(tckno, email)` | Hasta var mı kontrolü |
| `isThereNewPatient(tckno)` | Yeni hasta kontrolü |
| `isLogDoctor(tckno, password)` | Doktor giriş doğrulama |
| `isLogPatient(tckno, password)` | Hasta giriş doğrulama |
| `DoctorGetMe(tckno)` | Doktor bilgisi getir |
| `GetAllPatient(tckno)` | Doktora bağlı hastaları getir |
| `GetPatient(tckno)` | Hasta bilgisi getir |
| `addNewPatient(tckno)` | Yeni hasta ekle |
| `addNewPatient_1(patient, tckno)` | Hastayı doktora bağla |
| `DeletePatient(patient)` | Hasta bağlantısını kaldır |

### Firebase Veritabanı Yapısı

```
Firebase Realtime Database
│
├── DoctorInfo/
│   └── {TCKNO}/
│       ├── TCKNO: "12345678901"
│       ├── Name: "Dr. Ahmet Yılmaz"
│       ├── HospitalName: "Şehir Hastanesi"
│       ├── Mail: "ahmet@hospital.com"
│       ├── TelNo: "05551234567"
│       ├── ImageURL: "https://..."
│       ├── CV: "Kardiyoloji Uzmanı"
│       └── password: "MD5_HASH"
│
└── PatientInfo/
    └── {TCKNO}/
        ├── TCKNO: "98765432109"
        ├── Ad_Soyad: "Mehmet Demir"
        ├── DoctorTCKNO: "12345678901"
        ├── Mail: "mehmet@email.com"
        ├── TelNo: "05559876543"
        ├── BirthDate: "1990-05-15"
        ├── ImageURL: "https://..."
        ├── Status: "İyi"
        ├── CV: "Kalp hastası"
        ├── Medicine: ["Aspirin", "Metoprolol"]
        ├── MedicineTime: ["08:00", "20:00"]
        └── password: "MD5_HASH"
```

### Firebase Storage Yapısı

```
Firebase Storage
│
└── photos/
    ├── doctor/
    │   └── {TCKNO}.jpeg          # Doktor profil fotoğrafları
    │
    └── patient/
        └── {TCKNO}.jpeg          # Hasta profil fotoğrafları

└── videos/
    └── {DoctorTCKNO}/
        └── {PatientTCKNO}/
            └── {IlacAdı}-{Saat}.mp4  # Hasta videoları
```

---

## 🔐 Güvenlik

### Şifre Güvenliği

Uygulama, kullanıcı şifrelerini **MD5 hash** algoritması ile şifreler:

```csharp
private string getHash(string password)
{
    using (MD5 md5 = MD5.Create())
    {
        byte[] yeniByte = Encoding.ASCII.GetBytes(password);
        byte[] hashByte = md5.ComputeHash(yeniByte);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashByte.Length; i++)
        {
            sb.Append(hashByte[i].ToString("X2"));
        }
        return sb.ToString();
    }
}
```

### Oturum Yönetimi

- **Xamarin.Essentials.Preferences** kullanılarak oturum durumu saklanır
- Saklanan bilgiler: `logged`, `myid`, `role`, `mydocid`
- Çıkış yapıldığında tüm oturum bilgileri temizlenir

---

## 🚀 Kurulum

### Gereksinimler

- ✅ Visual Studio 2022 (veya üzeri)
- ✅ Xamarin workload yüklü
- ✅ Android SDK (API 31)
- ✅ .NET Standard 2.1 desteği
- ✅ Firebase projesi (Realtime Database + Storage)

### Adımlar

1. **Repository'yi klonlayın:**
```bash
git clone https://github.com/kullaniciadi/RemindMe.git
cd RemindMe
```

2. **Visual Studio ile açın:**
```
RemindMe.sln dosyasını açın
```

3. **NuGet paketlerini geri yükleyin:**
```
Solution üzerine sağ tık → Restore NuGet Packages
```

4. **Firebase yapılandırması yapın** (Bkz. Yapılandırma bölümü)

5. **Android emülatör veya cihaz seçin**

6. **Uygulamayı derleyip çalıştırın** (F5)

---

## ⚙️ Yapılandırma

### Firebase Bağlantısı

Aşağıdaki dosyalarda Firebase bağlantı adreslerini güncelleyin:

**`Client.cs` dosyasında:**
```csharp
FirebaseClient firebase = new FirebaseClient("FIREBASE_DATABASE_URL");
```

**`DoctorCreate.xaml.cs` dosyasında:**
```csharp
new FirebaseStorage("FIREBASE_STORAGE_URL", ...)
```

**`PatientCreate.xaml.cs` dosyasında:**
```csharp
new FirebaseStorage("FIREBASE_STORAGE_URL", ...)
```

**Diğer dosyalarda da aynı şekilde güncelleyin:**
- `DoctorEditPatient.xaml.cs`
- `DoctorProfilePage.xaml.cs`
- `DoctorPatientVideos.xaml.cs`
- `PatientMyMedicinePage.xaml.cs`
- `PatientMakeVideos.xaml.cs`

### Firebase Konsol Ayarları

1. [Firebase Console](https://console.firebase.google.com/) üzerinde yeni proje oluşturun
2. **Realtime Database** etkinleştirin
3. **Storage** etkinleştirin
4. Güvenlik kurallarını yapılandırın

---

## 🤝 Katkıda Bulunma

Projeye katkıda bulunmak için:

1. Bu repository'yi fork edin
2. Yeni bir branch oluşturun (`git checkout -b feature/yeni-ozellik`)
3. Değişikliklerinizi commit edin (`git commit -m 'Yeni özellik eklendi'`)
4. Branch'inizi push edin (`git push origin feature/yeni-ozellik`)
5. Pull Request oluşturun

---

## 📄 Lisans

Bu proje açık kaynak olarak geliştirilmektedir.

---

<p align="center">
  <strong>💊 RemindMe - Sağlığınız İçin Hatırlatıyoruz! 💊</strong>
</p>

<p align="center">
  Made with ❤️ using Xamarin.Forms
</p>
