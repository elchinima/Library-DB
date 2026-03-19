using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewApp.Core.Entities;

namespace NewApp.DAL.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(b => b.Autor)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.AutorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}