# 📚 NewApp — Kitab & Müəllif İdarəetmə Sistemi

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Architecture](https://img.shields.io/badge/Architecture-N--Layer-0078D4?style=for-the-badge)

> 🎯 Konsol əsaslı C# tətbiqi — müəllif və kitabların tam idarəetməsi üçün.

---

## 🏗️ Layihə Strukturu

```
NewApp/
├── 🖥️  NewApp.PL       →  Presentation Layer  (Program.cs)
├── ⚙️  NewApp.BLL      →  Business Logic      (AutorService.cs)
├── 🗄️  NewApp.DAL      →  Data Access         (DbContext, Migrations)
└── 🧩  NewApp.Core     →  Entities & Models   (Book, Autors)
```

---

## ✨ Xüsusiyyətlər

- 🔄 **Soft Delete** — müəlliflər `IsDeleted = true` ilə silinir, bazadan getmir
- 🕒 **Auto Audit** — `CreatedDate`, `UpdateDate`, `DeleteDate` avtomatik doldurulur
- 🔍 **Global Query Filter** — silinmiş müəlliflər sorğularda görünmür
- 🔗 **Cascade Delete** — müəllif silinəndə onun kitabları da silinir
- 🛠️ **Fluent API** — `BookConfiguration.cs` ilə EF konfiqurasiyası
- 📦 **EF Core Migrations** — versiyonlanmış schema idarəetməsi

---

## 🗃️ Entities

### 👤 `Autors` — `AuditableEntity`-dən miras alır
- 🔑 `Id` — `Guid` (əsas açar)
- 📝 `FullName` — tam adı
- 🎂 `Age` — yaşı
- 🖼️ `ProfileImage` — profil şəkli (URL)
- 📄 `OtherInfo` — əlavə məlumat
- 📅 `CreatedDate`, `UpdateDate`, `DeleteDate` — audit tarixlər
- 🗑️ `IsDeleted` — soft delete bayrağı

### 📖 `Book` — `BaseEntity`-dən miras alır
- 🔑 `Id` — `int` (əsas açar)
- 📝 `Name` — kitabın adı (max 150 simvol)
- 💰 `Price` — `decimal(18,2)`
- 🔗 `AutorId` — xarici açar → `Autors`
- 📅 `CreatedAt`, `UpdatedAt` — audit tarixlər

---

## 🖥️ Konsol Menyusu

```
=== Menu ===
1 - 👤 Muellifler
2 - 📖 Kitablar
0 - 🚪 Cixis
```

### 👤 Müəllif əməliyyatları
- 📋 Siyahı — bütün müəllifləri göstər
- ➕ Əlavə etmək — yeni müəllif yarat
- ✏️ Yeniləmək — ad ilə tap və yenilə
- 🗑️ Silmək — soft delete ilə sil

### 📖 Kitab əməliyyatları
- 📋 Siyahı — müəllif adı ilə göstər
- ➕ Əlavə et — müəllifə kitab əlavə et
- 🗑️ Sil — kitabı bazadan sil

---

## 🚀 İşə Salma

**1️⃣ Layihəni klonla**
```bash
git clone <repo-url>
cd NewApp
```

**2️⃣ Dependency-ləri yüklə**
```bash
dotnet restore
```

**3️⃣ Verilənlər bazasını yarat**
```bash
dotnet ef database update --project NewApp.DAL --startup-project NewApp.PL
```

**4️⃣ Tətbiqi işə sal**
```bash
dotnet run --project NewApp.PL
```

---

## 🔌 Connection String

```
Server=(localdb)\mssqllocaldb;Database=NewAppDb;
Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

> 📁 `NewApp.PL/Program.cs` və `NewApp.DAL/AppDbContextFactory.cs` fayllarında konfiqurasiya edilib.

---

## 📦 Texnologiyalar

![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![.NET 8](https://img.shields.io/badge/.NET_8-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![LocalDB](https://img.shields.io/badge/LocalDB-0078D4?style=flat-square)

---

<p align="center">Made with ❤️ by Elchin</p>