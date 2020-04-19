﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb
{
    public static class SeedData
    {
        public static void Seed(UserManager<IdentityUser> userManager)
        {
            SeedUser(userManager);
        }
        private static void SeedUser(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new IdentityUser { 
                    UserName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true
                };
                userManager.CreateAsync(user, "@dm1n1stratoR");
            }
        }
    }
}
