# MVCKamp Projesi - Detaylı Teknik Dokümantasyon

## Proje Özeti

MVCKamp, ASP.NET MVC Framework kullanarakgeliştirilen, çok katmanlı (N-Tier) mimariye sahip, tam yönetim kontrol panelli bir blog/içerik yönetim sistemidir. Sistem, yazarların içerik yayınlaması, okuyucuların içerikleri görmesi ve adminlerin tüm sistemi yönetmesini sağlar.

---

## Teknoloji Stack

- **Framework**: ASP.NET MVC 5.x
- **.NET Sürümü**: .NET Framework 4.7.2
- **Veritabanı**: SQL Server (SQLEXPRESS)
- **ORM**: Entity Framework 6.x
- **Frontend**: Bootstrap 5, jQuery, HTML5/CSS3
- **Template Engine**: Razor View Engine
- **Kullanıcı Arayüzü**: AdminLTE 3.0.4

---

## Proje Mimarisi

Proje 4 katmanlı N-Tier mimariye göre organize edilmiştir:

### 1. **Presentation Layer (Sunum Katmanı) - MVCProjeKampi**

Kullanıcı arayüzünü ve HTTP isteklerini işleyen katman.

**Konuşu:**
- `Controllers/`: Tüm controller dosyaları
  - `LoginController.cs`: Giriş ve yetkilendirme işlemleri
  - `AdminController.cs`: Admin panel işlemleri
  - `WriterPanelController.cs`: Yazar paneli işlemleri
  - `DefaultController.cs`: Genel sayfa işlemleri
  - `ContentController.cs`: İçerik listeleme işlemleri
  - `HomeController.cs`: Ana sayfa işlemleri

- `Views/`: Razor şablonları (.cshtml)
  - `Shared/`: Ortak layout dosyaları
	- `_Layout.cshtml`: Genel site layout
	- `_AdminLayout.cshtml`: Admin paneli layout
	- `_WriterLayout.cshtml`: Yazar paneli layout
  - `Login/`: Giriş sayfaları
  - `Admin/`: Admin paneli sayfaları
  - `WriterPanel/`: Yazar paneli sayfaları
  - `Content/`: İçerik listeleme sayfaları

- `Models/`: View modelleri (CategoryClass.cs vb.)

- `App_Start/`: Uygulama başlangıç konfigürasyonları
  - `BundleConfig.cs`: CSS/JS bundle ayarları
  - `RouteConfig.cs`: URL routing kuralları
  - `FilterConfig.cs`: Global filter ayarları

- `Roles/`: Rol tabanlı yetkilendirme
  - `AdminRoleProvider.cs`: Özel rol sağlayıcı

---

### 2. **Business Logic Layer (İş Mantığı Katmanı) - BusinessLayer**

Uygulamanın tüm iş kurallarını ve veri doğrulaması işlemleri.

**Yapısı:**

- `Abstract/`: Interface/Sözleşmeler (Repository Pattern)
  - `IAboutService.cs`
  - `IAdminService.cs`
  - `IHeadingService.cs`
  - `IContentService.cs`
  - `IWriterService.cs`
  - `IMessageService.cs`
  - `IContactService.cs`
  - `ICategoryService.cs`
  - `ISkillsService.cs`

- `Concrete/`: Manager sınıfları (Servis uygulamaları)
  - `AboutManager.cs`: Hakkımızda işlemleri
  - `HeadingManager.cs`: Başlık yönetimi
  - `ContentManager.cs`: İçerik yönetimi
  - `WriterManager.cs`: Yazar yönetimi
  - `MessageManager.cs`: Mesaj yönetimi
  - `AdminManager.cs`: Admin yönetimi

- `ValidationRules/`: FluentValidation ile doğrulama kuralları
  - `AdminValidator.cs`
  - `CategoryValidator.cs`
  - `WriterValidator.cs`
  - `MessageValidator.cs`
  - `ContactValidator.cs`

**Tasarım Deseni**: Manager-Service Pattern (Dependency Injection ile)

---

### 3. **Data Access Layer (Veri Erişim Katmanı) - DataAccessLayer**

Veritabanı ile iletişim ve veri işlemleri.

**Yapısı:**

- `Abstract/`: Repository arayüzleri
  - `IRepository.cs`: Genel CRUD işlemleri
  - Özel Dal arayüzleri (IAboutDal, IAdminDal, vb.)

- `EntityFramework/`: Entity Framework uygulamaları
  - `EfAboutDal.cs`
  - `EfAdminDal.cs`
  - `EfHeadingDal.cs`
  - `EfContentDal.cs`
  - `EfWriterDal.cs`
  - `EfCategoryDal.cs`
  - `EfContactDal.cs`
  - `EfMessageDal.cs`

- `Concrete/`:
  - `Context.cs`: Entity Framework DbContext sınıfı
  - `GenericRepository.cs`: Genel repository tabanı
  - Özel repository sınıfları

- `Migrations/`: Veritabanı şema değişiklikleri
  - 20+ migration dosyası
  - Tablo oluşturma, alan ekleme işlemleri

---

### 4. **Entity Layer (Varlık Katmanı) - EntityLayer**

Veritabanı tablolarını temsil eden sınıflar.

**Varlıklar:**

- `About.cs`: Hakkımızda bilgileri
- `Admin.cs`: Admin kullanıcıları
- `Category.cs`: İçerik kategorileri
- `Heading.cs`: Blog başlıkları/Konuları
- `Content.cs`: Blog içerikleri
- `Writer.cs`: Yazar/Kullanıcı bilgileri
- `Contact.cs`: İletişim formları
- `Message.cs`: Yazarlar arası mesajlar
- `Skills.cs`: Yetenek/Beceriler
- `CardAbout.cs`: Kart tabanlı hakkımızda
- `ImageFile.cs`: Dosya yönetimi

---

## Veritabanı Şeması

### Ana Tablolar

| Tablo | Amaç | Anahtar Alanlar |
|-------|------|-----------------|
| Writer | Yazarlar | WriterID, WriterName, WriterEmail, WriterPassword |
| Category | Kategoriler | CategoryID, CategoryName |
| Heading | Blog başlıkları | HeadingID, HeadingName, WriterID, CategoryID |
| Content | Blog içerikleri | ContentID, ContentValue, HeadingID |
| Admin | Admin kullanıcıları | AdminID, AdminName, AdminPassword |
| Message | Yazarlar arası mesaj | MessageID, SenderID, ReceiverID |
| Contact | İletişim mesajları | ContactID, ContactName, ContactEmail |
| About | Hakkımızda | AboutID, AboutContent |
| Skills | Yetenekler | SkillsID, SkillsName, SkillsPercentage |
| ImageFile | Dosya/Görsel | ImageFileID, ImagePath |

### İlişkiler

- **Writer ↔ Heading**: 1-Çok (Bir yazar birçok başlık yazabilir)
- **Category ↔ Heading**: 1-Çok (Bir kategori birçok başlık barındırabilir)
- **Heading ↔ Content**: 1-Çok (Bir başlığın birçok içeriği olabilir)
- **Writer ↔ Message**: 1-Çok (Bir yazar birçok mesaj gönderebilir/alabilir)

---

## Özellikler ve Fonksiyonaliteler

### 1. **Admin Paneli**
- Yazarları yönetme (Ekle, Düzenle, Sil)
- Kategorileri yönetme
- İçerikleri ve başlıkları yönetme
- Mesajları görüntüleme
- İletişim formlarını görüntüleme
- Rol tabanlı erişim kontrolü

### 2. **Yazar Paneli**
- Kendi başlıkları oluşturma
- İçerik ekleme/düzenleme
- Başlık durumunu kontrol etme (Yayınlandı/Taslak)
- Diğer yazarlarla mesajlaşma
- Profil yönetimi

### 3. **Ziyaretçi Görünümü**
- Blog başlıklarını listeleme
- İçerikleri kategorilere göre filtreleme
- İletişim formları
- Hakkımızda sayfası
- Galeri ve istatistikler

### 4. **Yetkilendirme Sistemi**
- Form-based Authentication
- Rol tabanlı yetkilendirme (Admin, Writer)
- Custom Role Provider
- Session yönetimi

---

## Kullanılan Tasarım Desenleri

### 1. **Repository Pattern**
Veri erişim katmanında kullanılır. Her varlık için Repository arayüzü ve somut uygulaması vardır.

```
IRepository<T> → GenericRepository<T>
├── IAboutDal → EfAboutDal
├── IHeadingDal → EfHeadingDal
└── ...
```

### 2. **Service/Manager Pattern**
İş mantığı katmanında. Her varlık için Manager sınıfı vardır.

```
IAboutService → AboutManager
IHeadingService → HeadingManager
```

### 3. **N-Tier Architecture**
Presentation → Business → Data Access → Database

### 4. **Dependency Injection (Örtülü)**
Controller'larda Manager sınıfları örnek oluşturulur.

### 5. **Validation Rules Pattern**
FluentValidation kütüphanesi ile validasyon kuralları tanımlanır.

---

## Güvenlik Özellikleri

1. **Form Tabanlı Kimlik Doğrulama**: Web.config'de tanımlı
2. **Rol Tabanlı Erişim Kontrolü**: AdminRoleProvider sınıfı
3. **Şifre Hashleme**: (Entity'lerde implement edilebilir)
4. **CSRF Koruması**: AntiForgeryToken kullanımı (View'lerde)
5. **Session Yönetimi**: Oturum süresi ve yönetimi

---

## Veritabanı Bağlantı Ayarları

```xml
<connectionStrings>
	<add name="Context" 
		 connectionString="data source=SQLConnection; 
						   initial catalog=KampMVCDBYENİ; 
						   integrated security=true;" 
		 providerName="System.Data.SqlClient" />
</connectionStrings>
```

- **Server**: SQLEXPRESS (Yerel)
- **Veritabanı**: KampMVCDBYENİ
- **Kimlik Doğrulama**: Windows Integrated Security

---

## NuGet Paketleri

Ana paketler:
- **EntityFramework** (6.x): ORM
- **FluentValidation** (Örtülü): Veri doğrulama
- **Bootstrap** (5.x): Frontend framework
- **jQuery** (3.7.0): JavaScript framework
- **AdminLTE** (3.0.4): Admin template

---

## Uygulama Akışı

### Giriş Akışı
1. Kullanıcı `/Login/Index` sayfasına gitme
2. Kimlik bilgileri sunulur (Email/Şifre)
3. `LoginController` istek işler
4. `WriterLoginManager` veritabanında kontrol eder
5. Başarılı ise Session ve Cookie ayarlanır
6. Admin/Yazar paneline yönlendirilir

### İçerik Yayınlama Akışı (Yazar)
1. Yazar `/WriterPanel/NewHeading` → Başlık oluşturur
2. `/WriterPanelContent/AddContent` → İçerik ekler
3. İçerik "Taslak" durumunda kaydedilir
4. Admin panelden onaylanmayı bekler
5. Admin onaylarsa "Yayınlandı" durumu alır
6. Genel siteye görünür hale gelir

### Mesajlaşma Akışı
1. Yazar `/WriterPanel/NewMessage` 
2. Alıcı yazarı seçer ve mesaj gönderir
3. Mesaj `Message` tablosuna kaydedilir
4. Alıcı `/WriterPanel/Inbox` sayfasında görür
5. Yanıt verebilir veya arşivleyebilir

---

## Dosya Yapısı Özeti

```
MVCProjeKampi/
├── Controllers/          # HTTP isteklerini işleyen sınıflar
├── Views/               # Razor şablonları
├── Models/              # View modelleri
├── App_Start/           # Konfigürasyon
├── Roles/               # Yetkilendirme sınıfları
├── AdminLTE-3.0.4/     # Admin template
├── Content/             # CSS dosyaları
├── Scripts/             # JavaScript dosyaları
└── Web.config           # Uygulama konfigürasyonu

BusinessLayer/
├── Abstract/            # Interface/Sözleşmeler
├── Concrete/            # Manager uygulamaları
└── ValidationRules/     # Doğrulama kuralları

DataAccessLayer/
├── Abstract/            # Repository arayüzleri
├── EntityFramework/     # EF uygulamaları
├── Concrete/            # Context ve Repositories
└── Migrations/          # Veritabanı şemaları

EntityLayer/
└── Concrete/            # Varlık sınıfları
```

---

## API/Endpoint Yapısı

### Admin İşlemleri
- `GET /Admin/Index` - Adminleri listele
- `GET /Admin/AddAdmin` - Admin ekle sayfası
- `POST /Admin/AddAdmin` - Admin ekle
- `GET /Admin/EditAdmin/{id}` - Admin düzenle sayfası
- `POST /Admin/EditAdmin` - Admin güncelle

### Yazar İşlemleri
- `GET /Writer/Index` - Yazarları listele
- `GET /WriterPanel/MyHeading` - Yazarın başlıkları
- `GET /WriterPanel/NewHeading` - Yeni başlık ekle

### İçerik İşlemleri
- `GET /Content/GetAllContent` - Tüm içerikleri listele
- `GET /Content/ContentByHeading/{id}` - Başlığa göre içerik

### Mesaj İşlemleri
- `GET /WriterPanelMessage/Inbox` - Gelen mesajlar
- `GET /WriterPanelMessage/Sendbox` - Gönderilen mesajlar
- `POST /WriterPanelMessage/NewMessage` - Mesaj gönder

---

## Kimlik Doğrulama ve Yetkilendirme

### Roles
- **Admin**: Tam sistem yönetim erişimi
- **Writer**: Kendi içerik yönetimi ve mesajlaşma

### Kontrol Mekanizması
```
Web.config → AuthenticationMode=Forms
			→ Custom RoleProvider
			→ [Authorize] attributes
```

---

## Veritabanı Migrations

Proje 15+ migration geçişi içerir:
- Tablo oluşturma
- Alan ekleme (Status, Image, Title vb.)
- İlişki tanımlama

Migration Komutları:
```
Add-Migration MigrationName
Update-Database
```

---

## Konfigürasyon Dosyaları

### Web.config
- Connection String tanımı
- Authentication ve Authorization ayarları
- Entity Framework konfigürasyonu
- Uygulama ayarları (webpages, clientValidation)

### App.config (BusinessLayer, DataAccessLayer)
- Assembly bindings
- Entity Framework provider konfigürasyonu

## Başlangıç Adımları

1. Veritabanını SQL Server'da oluştur
2. Connection String'i kendi ortamına uyarla
3. `Update-Database` komutu ile migration'ları uygula
4. Projeyi Visual Studio'da aç
5. Uygulamayı çalıştır (F5)
6. `/Login/Index` sayfasına git

---

## Sonuç

MVCKamp, profesyonel bir N-Tier mimariye sahip, ölçeklenebilir ve bakım yapılabilir bir ASP.NET MVC uygulamasıdır. Başlık, içerik, yazar, kategori ve mesajlaşma sistemlerinin bütünleşik yönetimini sağlar. Repository Pattern, Manager Pattern ve Role-Based Authorization gibi tasarım desenleri kullanarakuygulama sağlamlığını ve güvenliğini arttırır.
