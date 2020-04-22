using Microsoft.EntityFrameworkCore;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _db;
        public PortfolioRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Portfolio entity)
        {
            await _db.Portfolios.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Portfolio entity)
        {
            _db.Portfolios.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Portfolio>> FindAll()
        {
            var ToList = await _db.Portfolios.ToListAsync();
            return ToList;
        }

        public async Task<Portfolio> FindById(int id)
        {
            var ToFind = await _db.Portfolios.FindAsync(id);
            return ToFind;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Portfolio entity)
        {
            _db.Portfolios.Update(entity);
            return await Save();
        }
    }
}
