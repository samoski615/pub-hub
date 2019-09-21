using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PubHub.Models;

namespace PubHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BarOwner> BarOwners { get; set; }
        public DbSet<DrinkEnthusiast> DrinkEnthusiasts { get; set; }
        public DbSet<RatingsTable> RatingsTables { get; set; }
        public DbSet<HappyHourSpecials> HappyHourSpecials { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
