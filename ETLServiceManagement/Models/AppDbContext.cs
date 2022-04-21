using Microsoft.EntityFrameworkCore;

namespace ETLServiceManagement.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Models.Service.Service> services { get; set; }
    }
}
