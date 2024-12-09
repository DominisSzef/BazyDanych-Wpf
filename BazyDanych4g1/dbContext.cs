using Microsoft.EntityFrameworkCore;

namespace BazyDanych4g1;

public class dbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=wpf4g1;Trusted_Connection=True;");
    }
}