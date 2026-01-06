# 💊 RemindMe - İlaç Hatırlatıcı Uygulaması

<p align="center">
  <img src="https://img.shields.io/badge/Xamarin.Forms-3498DB?style=for-the-badge&logo=xamarin&logoColor=white" alt="Xamarin.Forms"/>
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#"/>
  <img src="https://img.shields.io/badge/Android-3DDC84?style=for-the-badge&logo=android&logoColor=white" alt="Android"/>
  <img src="https://img.shields.io/badge/Healthcare-E91E63?style=for-the-badge" alt="Healthcare"/>
</p>

**RemindMe**, doktor-hasta etkileşimli bir ilaç hatırlatıcısı uygulamasıdır. Hastaların ilaçlarını zamanında almasını sağlamak ve doktorların hasta takibini kolaylaştırmak için tasarlanmıştır. 

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Kullanıcı Rolleri](#-kullanıcı-rolleri)
- [Teknolojiler](#-teknolojiler)
- [Kurulum](#-kurulum)
- [Kullanım](#-kullanım)
- [Katkıda Bulunma](#-katkıda-bulunma)

## ✨ Özellikler

### 👨‍⚕️ Doktor Özellikleri
- 📋 Hasta listesi görüntüleme ve yönetimi
- 💊 İlaç reçetesi oluşturma ve düzenleme
- ⏰ İlaç alma saatlerini belirleme
- 📊 Hasta uyum raporlarını görüntüleme
- 🔔 Kritik durumlar için bildirim alma

### 👤 Hasta Özellikleri
- 💊 Günlük ilaç listesini görüntüleme
- ⏰ İlaç alma hatırlatıcıları
- ✅ İlaç alındı olarak işaretleme
- 📅 İlaç geçmişini görüntüleme
- 👨‍⚕️ Doktoruyla iletişim

### 🔔 Bildirim Sistemi
- Push notification ile hatırlatma
- Tekrarlayan alarm desteği
- Kaçırılan doz bildirimi

## 👥 Kullanıcı Rolleri

```
┌─────────────────────────────────────────────────────────┐
│                      RemindMe                           │
├─────────────────────────┬───────────────────────────────┤
│       DOKTOR            │           HASTA               │
├─────────────────────────┼───────────────────────────────┤
│ • Hasta kaydı           │ • İlaç listesi görüntüleme    │
│ • Reçete yazma          │ • Hatırlatıcı alma            │
│ • İlaç tanımlama        │ • İlaç onaylama               │
│ • Rapor görüntüleme     │ • Geçmiş görüntüleme          │
│ • Bildirim gönderme     │ • Doktora mesaj               │
└─────────────────────────┴───────────────────────────────┘
```

## 🛠 Teknolojiler

- **Xamarin.Forms** - Cross-platform mobil geliştirme
- **C#** - Programlama dili
- **SQLite** - Yerel veritabanı
- **Local Notifications** - Bildirim sistemi
- **MVVM Pattern** - Mimari pattern

## 🚀 Kurulum

### Gereksinimler
- Visual Studio 2022 (Xamarin workload)
- Android SDK 29+
- . NET Standard 2.0

### Adımlar

```bash
# Repository'yi klonlayın
git clone https://github.com/kadirbeskardes/RemindMe.git
cd RemindMe

# Visual Studio ile açın ve derleyin
# RemindMe.sln dosyasını açın
```

## 📁 Proje Yapısı

```
RemindMe/
├── RemindMe/                      # Shared Xamarin. Forms projesi
│   ├── Models/                    # Veri modelleri
│   │   ├── User.cs               # Kullanıcı (Doktor/Hasta)
│   │   ├── Medicine.cs           # İlaç modeli
│   │   ├── Prescription.cs       # Reçete modeli
│   │   └── Reminder.cs           # Hatırlatıcı modeli
│   ├── Views/                     # XAML sayfaları
│   │   ├── LoginPage.xaml
│   │   ├── DoctorDashboard.xaml
│   │   ├── PatientDashboard.xaml
│   │   ├── MedicineListPage.xaml
│   │   └── AddMedicinePage.xaml
│   ├── ViewModels/                # ViewModel sınıfları
│   └── Services/                  # Servis katmanı
│       ├── NotificationService.cs
│       └── DatabaseService.cs
├── RemindMe.Android/              # Android platforma özgü kod
│   ├── MainActivity.cs
│   └── NotificationHelper.cs
└── RemindMe.sln
```

## 📱 Kullanım Akışı

### Doktor Akışı
```
1. Giriş Yap (Doktor hesabı)
         │
         ▼
2. Dashboard görüntüle
         │
         ▼
3. Hasta seç veya ekle
         │
         ▼
4. Reçete oluştur
   • İlaç adı
   • Doz miktarı
   • Alma saatleri
   • Süre
         │
         ▼
5. Hasta takibi
```

### Hasta Akışı
```
1. Giriş Yap (Hasta hesabı)
         │
         ▼
2. Günlük ilaç listesi
         │
         ▼
3. Hatırlatıcı al ⏰
         │
         ▼
4. İlaç aldım ✅
         │
         ▼
5. Geçmiş görüntüle
```

## 🔔 Bildirim Yapılandırması

```csharp
// Örnek bildirim ayarı
var notification = new LocalNotification
{
    Title = "İlaç Zamanı! ",
    Body = "Aspirin - 1 tablet almanız gerekiyor",
    NotifyTime = DateTime.Now.AddHours(8),
    RepeatInterval = TimeSpan.FromHours(12)
};
```

## 🤝 Katkıda Bulunma

1. Fork edin
2. Feature branch oluşturun (`git checkout -b feature/NewFeature`)
3. Commit edin (`git commit -m 'Add NewFeature'`)
4. Push edin (`git push origin feature/NewFeature`)
5. Pull Request açın

## ⚠️ Sorumluluk Reddi

Bu uygulama yalnızca hatırlatma amaçlıdır ve tıbbi tavsiye niteliği taşımaz. İlaç kullanımı konusunda her zaman doktorunuza danışın.

## 📄 Lisans

MIT License

---

<p align="center">
  💊 <strong>RemindMe</strong> - Sağlığınız için doğru zamanda, doğru ilaç! 
</p>
