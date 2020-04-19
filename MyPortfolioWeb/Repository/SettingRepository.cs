using Microsoft.EntityFrameworkCore;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Repository
{
    public class SettingRepository : ISettingRepository
    {
        private readonly ApplicationDbContext _db;
        public SettingRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Setting entity)
        {
            await _db.Settings.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Setting entity)
        {
            _db.Settings.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Setting>> FindAll()
        {
            var settings = await _db.Settings.ToListAsync();
            return settings;
        }

        public async Task<Setting> FindById(int id)
        {
            var setting = await _db.Settings.FindAsync(id);
            return setting;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Setting entity)
        {
            _db.Settings.Update(entity);
            return await Save();
        }
    }
}
