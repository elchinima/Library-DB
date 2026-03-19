using Microsoft.EntityFrameworkCore;
using NewApp.Core.Models;
using NewApp.DAL;

namespace NewApp.BLL.Services
{
    public class AutorService
    {
        private readonly AppDbContext _context;

        public AutorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Autors>> GetAllAsync()
        {
            return await _context.Autors.ToListAsync();
        }

        public async Task<Autors?> GetByNameAsync(string fullName)
        {
            return await _context.Autors
                .FirstOrDefaultAsync(a => a.FullName.ToLower() == fullName.Trim().ToLower());
        }

        public async Task CreateAsync(string fullName, int age, string profileImage, string otherInfo)
        {
            var autor = new Autors
            {
                Id = Guid.NewGuid(),
                FullName = fullName,
                Age = age,
                ProfileImage = profileImage,
                OtherInfo = otherInfo
            };

            await _context.Autors.AddAsync(autor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, string fullName, int age, string profileImage, string otherInfo)
        {
            var autor = await _context.Autors.FirstOrDefaultAsync(a => a.Id == id);
            if (autor == null) return;

            autor.FullName = fullName;
            autor.Age = age;
            autor.ProfileImage = profileImage;
            autor.OtherInfo = otherInfo;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var autor = await _context.Autors.FirstOrDefaultAsync(a => a.Id == id);
            if (autor == null) return;

            _context.Autors.Remove(autor); 
            await _context.SaveChangesAsync();
        }
    }
}