using Core.Persistence.Entities;

namespace BasakSehirBurada.Domain.Entities
{
    public class Category : Entity<int>
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; } 
    }
}
