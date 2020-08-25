using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

//The DbContext base class provides access to the Entity Framework Core’s underlying functionality, 
//and the Cocktails property will provide access to the Cocktail objects in the database.
namespace BartenderApp.Models
{
        public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
        { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
