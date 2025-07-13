using Microsoft.EntityFrameworkCore;
using Prueba.Domain.Entities.Model;

namespace Prueba.Infraestructure.Contexts.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Rols> Rols { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>()
                .HasKey(u => u.Usr_Id);

            modelBuilder.Entity<Rols>()
                .HasKey(r => r.Rol_Id);

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Usr_DocumentNumber)
                .IsUnique();

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Rols)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.Usr_RolId)
                .HasConstraintName("FK_Users_Rols");
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Users>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    if (entry.Entity.Password != null)
                        entry.Entity.Password = PasswordHelper.EncryptPassword(entry.Entity.Password);
                }
            }

            return base.SaveChanges();
        }
    }
}