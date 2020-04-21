using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyPortfolioWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
            public DbSet<Setting> Settings { get; set; }
            public DbSet<WelcomePage> WelcomePages { get; set; }
            public DbSet<Skill> Skills { get; set; }
            public DbSet<Experience> Experiences { get; set; }
            public DbSet<Testimonial> Testimonials { get; set; }
        }
}
