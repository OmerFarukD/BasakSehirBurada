using BasakSehirBurada.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BasakSehirBurada.Persistence.Contexts;

public class BaseDbContext : IdentityDbContext<User,IdentityRole,string>
{


    public BaseDbContext(DbContextOptions options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


  
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
