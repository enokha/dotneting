using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBookApp.Models;

namespace MyBookApp.Data
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BookModel> Books { get; set; }
    }
}
