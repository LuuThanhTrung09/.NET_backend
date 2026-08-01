using Game106_API.Models;
using Microsoft.EntityFrameworkCore;


namespace Game106_API.AppDataContext
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<Account> accounts  {get; set;}
        public DbSet<Character> characters {get; set;}
    }
}