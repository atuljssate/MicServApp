using Microsoft.EntityFrameworkCore;
using MSA.Services.OrderAPI.Models;

namespace MSA.Services.OrderAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<OrderHeader> OrderHeaders { get; set; }  
        public DbSet<OrderDetails> OrderDetails { get; set; }               
    }
}
