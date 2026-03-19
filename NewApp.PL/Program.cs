using Microsoft.EntityFrameworkCore;
using NewApp.BLL.Services;
using NewApp.Core.Entities;
using NewApp.DAL;

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NewAppDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

var context = new AppDbContext(optionsBuilder.Options);
var autorService = new AutorService(context);

Console.WriteLine("=== Menu ===");

while (true)
{
    Console.WriteLine("\n1 - Muellifler");
    Console.WriteLine("2 - Kitablar");
    Console.WriteLine("0 - Cixis");
    Console.Write("Secim: ");

    var mainChoice = Console.ReadLine();

    if (mainChoice == "0") break;

    switch (mainChoice)
    {
        case "1":
            await AutorMenu();
            break;

        case "2":
            await BookMenu();
            break;

        default:
            Console.WriteLine("Yanlis secim!");
            break;
    }
}

async Task AutorMenu()
{
    while (true)
    {
        Console.WriteLine("\n--- Muellifler ---");
        Console.WriteLine("1 - Siyahi");
        Console.WriteLine("2 - Elave etmek");
        Console.WriteLine("3 - Yenilemek");
        Console.WriteLine("4 - Silmek");
        Console.WriteLine("0 - Cixis");
        Console.Write("Secim: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var autors = await autorService.GetAllAsync();
                if (!autors.Any())
                {
                    Console.WriteLine("Muellif tapilmadi!");
                    break;
                }
                Console.WriteLine("\nMuellifler");
                foreach (var a in autors)
                    Console.WriteLine($"Id: {a.Id} | Tam adi: {a.FullName} | Yas: {a.Age}");
                break;

            case "2":
                Console.Write("Ad Soyad: ");
                var fullName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(fullName)) { Console.WriteLine("Bos ola bilmez."); break; }

                Console.Write("Yas: ");
                if (!int.TryParse(Console.ReadLine(), out var age)) { Console.WriteLine("Yanlış format."); break; }

                Console.Write("Profil sekli (url): ");
                var profileImage = Console.ReadLine() ?? "";

                Console.Write("Elave melumat: ");
                var otherInfo = Console.ReadLine() ?? "";

                await autorService.CreateAsync(fullName, age, profileImage, otherInfo);
                Console.WriteLine("Muellif elave edildi!");
                break;

            case "3":
                Console.Write("Yenilemek ucun muəllif adi: ");
                var searchName = Console.ReadLine();
                var autorToUpdate = await autorService.GetByNameAsync(searchName ?? "");
                if (autorToUpdate == null) { Console.WriteLine("Tapilmad!"); break; }

                Console.Write($"Yeni ad ({autorToUpdate.FullName}): ");
                var newName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newName)) newName = autorToUpdate.FullName;

                Console.Write($"Yeni yas ({autorToUpdate.Age}): ");
                var ageInput = Console.ReadLine();
                var newAge = int.TryParse(ageInput, out var parsedAge) ? parsedAge : autorToUpdate.Age;

                Console.Write($"Yeni sekil ({autorToUpdate.ProfileImage}): ");
                var newImage = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newImage)) newImage = autorToUpdate.ProfileImage;

                Console.Write($"Yeni melumat ({autorToUpdate.OtherInfo}): ");
                var newInfo = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newInfo)) newInfo = autorToUpdate.OtherInfo;

                await autorService.UpdateAsync(autorToUpdate.Id, newName, newAge, newImage, newInfo);
                Console.WriteLine("Yenilendi.");
                break;

            case "4":
                Console.Write("Silmek ucun muellif adı: ");
                var deleteName = Console.ReadLine();
                var autorToDelete = await autorService.GetByNameAsync(deleteName ?? "");
                if (autorToDelete == null) { Console.WriteLine("Tapilmadi!"); break; }

                await autorService.DeleteAsync(autorToDelete.Id);
                Console.WriteLine("Muellif silindi!");
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Yanliş secim!");
                break;
        }
    }
}

async Task BookMenu()
{
    while (true)
    {
        Console.WriteLine("\n--- KITABLAR ---");
        Console.WriteLine("1 - Kitablari goster");
        Console.WriteLine("2 - Kitab elave et");
        Console.WriteLine("3 - Kitab sil");
        Console.WriteLine("0 - Cixis");
        Console.Write("Secim: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var books = await context.Books.Include(b => b.Autor).OrderBy(b => b.Name).ToListAsync();
                if (!books.Any()) { Console.WriteLine("Kitab tapilmadi!"); break; }
                Console.WriteLine("\nKitablar:");
                foreach (var b in books)
                    Console.WriteLine($"Id: {b.Id} | Kitabin adi: {b.Name} | Qiymeti: {b.Price} | Muellif: {b.Autor?.FullName ?? "Yoxdur"}");
                break;

            case "2":
                var autors = await autorService.GetAllAsync();
                if (!autors.Any())
                {
                    Console.WriteLine("Evvelce muellif elave edin!");
                    break;
                }

                Console.WriteLine("Mevcud Muellifler:");
                foreach (var a in autors)
                    Console.WriteLine($"- {a.Id}: {a.FullName}");

                Console.Write("Muellif Id secin: ");
                var autorIdInput = Console.ReadLine();
                if (!Guid.TryParse(autorIdInput, out var autorId)) { Console.WriteLine("Yanliş format!"); break; }

                var selectedAutor = autors.FirstOrDefault(a => a.Id == autorId);
                if (selectedAutor == null) { Console.WriteLine("Muellif tapilmadi!"); break; }

                Console.Write("Kitab adi: ");
                var bookName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(bookName)) { Console.WriteLine("Boş ola bilməz."); break; }

                Console.Write("Qiymet: ");
                if (!decimal.TryParse(Console.ReadLine(), out var price)) { Console.WriteLine("Yanliş format!"); break; }

                var newBook = new Book { Name = bookName.Trim(), Price = price, AutorId = selectedAutor.Id };
                context.Books.Add(newBook);
                await context.SaveChangesAsync();
                Console.WriteLine($"Kitab elave edildi. Id: {newBook.Id}");
                break;

            case "3":
                var allBooks = context.Books.OrderBy(b => b.Name).ToList();
                if (!allBooks.Any()) { Console.WriteLine("Silinecek kitab yoxdur!"); break; }

                Console.WriteLine("\nKitablar:");
                foreach (var b in allBooks)
                    Console.WriteLine($"Id: {b.Id} | Kitabin adi: {b.Name}");

                Console.Write("Silinecek kitab Id: ");
                if (!int.TryParse(Console.ReadLine(), out var bookId)) { Console.WriteLine("Yanliş Id!"); break; }

                var bookToDelete = context.Books.FirstOrDefault(b => b.Id == bookId);
                if (bookToDelete == null) { Console.WriteLine("Tapilmadi!"); break; }

                context.Books.Remove(bookToDelete);
                await context.SaveChangesAsync();
                Console.WriteLine("Kitab silindi!");
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Yanlis secim!");
                break;
        }
    }
}