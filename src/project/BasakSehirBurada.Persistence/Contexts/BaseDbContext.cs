using BasakSehirBurada.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BasakSehirBurada.Persistence.Contexts;

public class BaseDbContext : IdentityDbContext<User,IdentityRole,string>
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"server = (localdb)\MSSQLLocalDB; Database=Stok_takip_db; Trusted_Connection=true");
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
