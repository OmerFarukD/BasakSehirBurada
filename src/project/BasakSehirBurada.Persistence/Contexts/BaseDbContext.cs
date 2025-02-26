using BasakSehirBurada.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasakSehirBurada.Persistence.Contexts;

public class     : DbContext
{



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"server = (localdb)\MSSQLLocalDB; Database=Stok_takip_db; Trusted_Connection=true");
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
