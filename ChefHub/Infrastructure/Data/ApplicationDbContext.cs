using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(AdminDataSeed());
        }
        private User AdminDataSeed()
        {
            return new User
            {
                Id = 1,
                FullName = "Admin",
                Description = "Soy el administrador de esta aplicación",
                Email = "admin@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("string"),
                TipoRol = Role.Admin,
                UrlPhoto = "string"
            };
        }
    }
}