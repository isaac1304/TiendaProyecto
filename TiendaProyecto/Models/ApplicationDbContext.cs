using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TiendaProyecto.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(x => new { x.Id, });
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.LoginProvider, x.ProviderKey });
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasKey(x => new { x.Id });
            //Isaac
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Carrito> Carritos { get; set; }

    }
}
