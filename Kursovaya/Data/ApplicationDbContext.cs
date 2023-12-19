using Kursovaya.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kursovaya.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<UrlModel> UrlModels { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
            Database.EnsureCreated();
        }
    }
}
