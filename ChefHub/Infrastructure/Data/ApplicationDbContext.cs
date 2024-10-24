using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
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
            string? adminPassword = _configuration["AdminUser:Password"];
            return new User
            {
                Id = 1,
                FullName = "Admin",
                Description = "Soy el administrador de esta aplicación",
                Email = "admin@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword(adminPassword),
                TipoRol = Role.Admin,
                UrlPhoto = "string"
            };
        }
    }
}