using Microsoft.EntityFrameworkCore;
using Project.Api.Models;

namespace Project.Api.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

     
    }
}