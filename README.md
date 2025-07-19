# SD Burger Restaurant Management System

[![.NET](https://img.shields.io/badge/.NET-9.0-purple.svg)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-blue.svg)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-9.0.7-green.svg)](https://docs.microsoft.com/en-us/ef/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

Çok şubeli burger restoranı zincirleri için tasarlanmış restoran yönetim sistemi.

## ⚠️ Geliştirme Durumu

> **⚠️ Geliştirme Aşamasında**
> 
> Bu proje aktif geliştirme aşamasındadır ve **ÜRETİM ORTAMI İÇİN HAZIR DEĞİLDİR**.
> 
> - 🚧 Bazı özellikler geliştirme aşamasındadır
> - 🐛 Hatalar ve kararlılık sorunları mevcuttur
> - 📝 Dokümantasyon eksiktir
> - 🔒 Güvenlik önlemleri tamamlanmamıştır
> - ⚡ Performans optimizasyonları beklemektedir
> - 🧪 Test kapsamı sınırlıdır
> 
> **Öneriler:**
> - Sadece test ortamlarında kullanın
> - Üretim verilerinizi kullanmayın
> - GitHub Issues üzerinden sorun bildirin

## 🏗️ Proje Yapısı

| Katman | Proje | Açıklama |
|--------|-------|----------|
| **Core** | `SD_Burger.Core` | Varlıklar ve repository arayüzleri |
| **Infrastructure** | `SD_Burger.Infrastructure` | Veri erişimi ve EF Core |
| **Application** | `SD_Burger.Application` | İş mantığı ve servisler |
| **API** | `SD_Burger.API` | REST API endpoint'leri |
| **Web** | `SD_Burger.Web` | MVC web uygulaması |

## 🚀 Özellikler

- **Şube Yönetimi** - Çok lokasyonlu restoran desteği
- **Menü Yönetimi** - Kategori ve ürün yönetimi
- **Stok Yönetimi** - Malzeme takibi ve stok kontrolü
- **Sipariş Yönetimi** - Sipariş alma ve takip
- **Rezervasyon Sistemi** - Masa rezervasyonları
- **Ödeme İşleme** - Ödeme yönetimi
- **Kullanıcı Yönetimi** - Rol tabanlı erişim
- **Raporlama** - Satış ve stok raporları

## 🛠️ Teknolojiler

### Backend
- **.NET 9.0** - Framework
- **ASP.NET Core MVC** - Web framework
- **Entity Framework Core 9.0.7** - ORM
- **Microsoft.EntityFrameworkCore.SqlServer 9.0.7** - SQL Server provider
- **Microsoft.EntityFrameworkCore.Tools 9.0.7** - EF Tools
- **Microsoft.AspNetCore.OpenApi 9.0.6** - OpenAPI support
- **Swashbuckle.AspNetCore 9.0.3** - Swagger/OpenAPI
- **Microsoft.AspNetCore.Mvc.NewtonsoftJson 9.0.7** - JSON serialization

### Frontend
- **Bootstrap 5** - UI framework
- **Razor Views** - Server-side templating
- **JavaScript** - Client-side scripting
- **CSS3** - Styling

### Veritabanı
- **SQL Server** - Relational database

## 📊 Veritabanı

Sistem 12 ana tablo içerir:
- **Branches** - Şube bilgileri
- **Categories** - Menü kategorileri
- **Ingredients** - Malzeme bilgileri
- **Inventory** - Stok takibi
- **MenuItems** - Menü öğeleri
- **MenuItemIngredients** - Menü-malzeme ilişkisi
- **Orders** - Siparişler
- **OrderItems** - Sipariş detayları
- **Payments** - Ödeme bilgileri
- **Reservations** - Rezervasyonlar
- **Tables** - Masa bilgileri
- **Users** - Kullanıcılar

## 📦 Kurulum

### Gereksinimler
- .NET 9.0 SDK
- SQL Server 2019 veya üzeri

### Adımlar

1. **Repository'yi klonlayın**
   ```bash
   git clone https://github.com/SametDulger/SD_Burger.git
   cd SD_Burger
   ```

2. **Veritabanı bağlantısını yapılandırın**
   - `SD_Burger.API/appsettings.json` dosyasında connection string'i güncelleyin
   - `SD_Burger.Web/Services/ApiService.cs` dosyasında API base URL'i güncelleyin

3. **Veritabanını oluşturun**
   ```bash
   cd SD_Burger.API
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Projeyi çalıştırın**
   ```bash
   # API projesini çalıştırın
   cd SD_Burger.API
   dotnet run
   
   # Web projesini çalıştırın (yeni terminal)
   cd SD_Burger.Web
   dotnet run
   ```

5. **Erişim**
   - Web uygulaması: https://localhost:7030 (HTTP: http://localhost:5237)
   - API: https://localhost:7070 (HTTP: http://localhost:5269)
   - Swagger: https://localhost:7070/swagger

## 📚 API Dokümantasyonu

API dokümantasyonu Swagger UI üzerinden erişilebilir:
```
https://localhost:7070/swagger
```

### Mevcut Endpoint'ler
- `/api/branches` - Şube yönetimi
- `/api/categories` - Kategori yönetimi
- `/api/ingredients` - Malzeme yönetimi
- `/api/inventory` - Stok yönetimi
- `/api/menuitems` - Menü öğesi yönetimi
- `/api/orders` - Sipariş yönetimi
- `/api/payments` - Ödeme yönetimi
- `/api/reservations` - Rezervasyon yönetimi
- `/api/tables` - Masa yönetimi
- `/api/users` - Kullanıcı yönetimi
- `/api/reports` - Rapor oluşturma


## 🤝 Katkıda Bulunma

1. Repository'yi fork edin
2. Özellik dalı oluşturun (`git checkout -b feature/yeni-ozellik`)
3. Değişikliklerinizi commit edin (`git commit -m 'Yeni özellik eklendi'`)
4. Dalınıza push edin (`git push origin feature/yeni-ozellik`)
5. Pull Request oluşturun

## 📄 Lisans

Bu proje MIT Lisansı altında lisanslanmıştır.

---

**SD Burger Restaurant Management System** - Restoran zincirleri için modern teknoloji çözümü. 