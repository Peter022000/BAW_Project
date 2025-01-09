using BAW_Project_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BAW_Project_API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users {  get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
