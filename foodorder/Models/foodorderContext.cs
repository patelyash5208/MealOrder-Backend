using Microsoft.EntityFrameworkCore;

namespace foodorder.Models
{
    public class foodorderContext : DbContext
    {
        public foodorderContext(DbContextOptions<foodorderContext> options) : base(options) { }
        public DbSet<foodItem> FoodItems { get; set; } = null!;
        public DbSet<User> Users { get; set; }=null!;
    }
}