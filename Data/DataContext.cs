using Microsoft.EntityFrameworkCore;
using ProjectFor7COMm.Models;

namespace ProjectFor7COMm.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> Options) : base(Options) { }

        public DbSet<User> Users { get; set; }
    }
}
