# 📚 NewApp — Kitab və Müəllif İdarəetmə Sistemi

Konsol əsaslı C# tətbiqi. **Entity Framework Core** və **SQL Server** istifadə edərək müəllif və kitabların idarə edilməsini təmin edir.

---

## 🏗️ Layihə Strukturu

```
NewApp/
├── NewApp.PL/          → Presentation Layer (Program.cs — konsol menyusu)
├── NewApp.BLL/         → Business Logic Layer (AutorService.cs)
├── NewApp.DAL/         → Data Access Layer (AppDbContext, Migrations, Configurations)
└── NewApp.Core/        → Entities və Models (Book, Autors, BaseEntity)
```

### Layihələr

| Layihə | Məqsəd |
|--------|--------|
| `NewApp.Core` | Entity-lər və modellər (`Book`, `Autors`, `BaseEntity`, `AuditableEntity`) |
| `NewApp.DAL` | EF Core DbContext, Migrations, Fluent API konfiqurasiyaları |
| `NewApp.BLL` | Biznes məntiqi — `AutorService` |
| `NewApp.PL` | Konsol interfeysi — `Program.cs` |

---

## 🗃️ Verilənlər Bazası Strukturu

### `Autors` cədvəli (`AuditableEntity`-dən miras alır)

| Sütun | Tip | Açıqlama |
|-------|-----|----------|
| `Id` | `Guid` | Əsas açar |
| `FullName` | `string` | Müəllifin tam adı |
| `Age` | `int` | Yaşı |
| `ProfileImage` | `string` | Profil şəkli (URL) |
| `OtherInfo` | `string` | Əlavə məlumat |
| `CreatedDate` | `DateTime` | Yaradılma tarixi (avtomatik) |
| `UpdateDate` | `DateTime` | Yenilənmə tarixi (avtomatik) |
| `IsDeleted` | `bool` | Soft delete bayrağı |

### `Books` cədvəli (`BaseEntity`-dən miras alır)

| Sütun | Tip | Açıqlama |
|-------|-----|----------|
| `Id` | `int` | Əsas açar |
| `Name` | `string` | Kitabın adı (max 150 simvol) |
| `Price` | `decimal(18,2)` | Qiyməti |
| `AutorId` | `Guid` | Xarici açar → `Autors` |
| `CreatedAt` | `DateTime` | Yaradılma tarixi (avtomatik) |

> **Əlaqə:** Bir müəllifin bir neçə kitabı ola bilər. Müəllif silinərsə, onun kitabları da silinir (`Cascade Delete`).

---

## ⚙️ Xüsusiyyətlər

- ✅ **Soft Delete** — `Autors` cədvəlində `IsDeleted = true` ilə silinmə
- ✅ **Audit Fields** — `CreatedDate`, `UpdateDate`, `DeleteDate` avtomatik doldurulur
- ✅ **Global Query Filter** — Silinmiş müəlliflər sorğularda avtomatik gizlədilir
- ✅ **Fluent API** — `BookConfiguration.cs` ilə konfiqurasiya
- ✅ **EF Core Migrations** — Versiyonlanmış schema idarəetməsi

---

## 🖥️ Konsol Menyusu

```
=== Menu ===
1 - Muellifler
2 - Kitablar
0 - Cixis
```

### Müəllif Əməliyyatları
- Siyahı — bütün müəllifləri göstərir
- Əlavə etmək — yeni müəllif yaradır
- Yeniləmək — ad ilə müəllif tapıb məlumatları yeniləyir
- Silmək — müəllifi soft delete ilə silir

### Kitab Əməliyyatları
- Siyahı — müəllif adı ilə birlikdə bütün kitabları göstərir
- Əlavə et — mövcud müəllifə kitab əlavə edir
- Sil — kitabı bazadan silir

---

## 🚀 İşə Salma

### Tələblər
- [.NET 8 SDK](https://dotnet.microsoft.com/)
- SQL Server / LocalDB
- EF Core CLI

### Addımlar

```bash
# 1. Layihəni klonlayın
git clone <repo-url>
cd NewApp

# 2. Dependency-ləri yükləyin
dotnet restore

# 3. Migration-ları tətbiq edin
dotnet ef database update --project NewApp.DAL --startup-project NewApp.PL

# 4. Tətbiqi işə salın
dotnet run --project NewApp.PL
```

---

## 🔌 Connection String

`NewApp.PL/Program.cs` və `NewApp.DAL/AppDbContextFactory.cs` fayllarında:

```
Server=(localdb)\mssqllocaldb;Database=NewAppDb;Trusted_Connection=True;
MultipleActiveResultSets=true;TrustServerCertificate=True
```

---

## 📦 İstifadə Edilən Texnologiyalar

| Texnologiya | Versiya |
|-------------|---------|
| C# / .NET | 8+ |
| Entity Framework Core | 8+ |
| SQL Server / LocalDB | — |
| ADO.NET (EF üzərindən) | — |