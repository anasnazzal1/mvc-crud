using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;

using WebApplication2.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Prodacts> prodacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer(
                    "Server=db31245.public.databaseasp.net; Database=db31245; User Id=db31245; Password=iW-3!9StN+p7; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;"
                );
            }
        }
    }
}
