using Microsoft.EntityFrameworkCore;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Repository
{
    public class WelcomePageRepository : IWelcomePageRepository
    {
        private readonly ApplicationDbContext _db;
        public WelcomePageRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(WelcomePage entity)
        {
            await _db.WelcomePages.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(WelcomePage entity)
        {
            _db.WelcomePages.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<WelcomePage>> FindAll()
        {
            var welcomePages = await _db.WelcomePages.ToListAsync();
            return welcomePages;
        }

        public async Task<WelcomePage> FindById(int id)
        {
            var welcomePage = await _db.WelcomePages.FindAsync(id);
            return welcomePage;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(WelcomePage entity)
        {
            _db.WelcomePages.Update(entity);
            return await Save();
        }
    }
}
