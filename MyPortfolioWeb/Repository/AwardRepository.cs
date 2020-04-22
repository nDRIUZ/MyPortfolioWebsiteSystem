using Microsoft.EntityFrameworkCore;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Repository
{
    public class AwardRepository : IAwardRepository
    {
        private readonly ApplicationDbContext _db;
        public AwardRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Award entity)
        {
            await _db.Awards.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Award entity)
        {
            _db.Awards.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Award>> FindAll()
        {
            var ToList = await _db.Awards.ToListAsync();
            return ToList;
        }

        public async Task<Award> FindById(int id)
        {
            var ToFind = await _db.Awards.FindAsync(id);
            return ToFind;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Award entity)
        {
            _db.Awards.Update(entity);
            return await Save();
        }
    }
}
