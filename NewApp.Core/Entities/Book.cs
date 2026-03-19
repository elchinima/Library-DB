using NewApp.Core.Models;

namespace NewApp.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public Guid AutorId { get; set; }
        public Autors Autor { get; set; }
    }
}