using E_Commerce.core.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.infrastructure.RepositoryLayer
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext(DbContextOptions dboptions) : base(dboptions)
        {

        }
        public DbSet<LoginModel> Login { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<BrandModel> Brand { get; set; }
        

    }
}